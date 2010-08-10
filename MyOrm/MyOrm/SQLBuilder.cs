using System;
using System.Collections.Generic;
using System.Text;
using MyOrm.Common;
using System.Text.RegularExpressions;
using System.Collections;

namespace MyOrm
{
    public class SQLBuilder
    {
        #region 静态成员
        public readonly static SQLBuilder Default = new SQLBuilder();
        private static Dictionary<Type, SQLBuilder> sqlBuilderCache = new Dictionary<Type, SQLBuilder>();

        public static SQLBuilder GetSqlBuilder(Type objectType)
        {
            if (sqlBuilderCache.ContainsKey(objectType)) return sqlBuilderCache[objectType];
            return Default;
        }

        public static void RegisterSqlBuilder(Type objectType, SQLBuilder sqlBuilder)
        {
            sqlBuilderCache[objectType] = sqlBuilder;
        }
        #endregion

        #region 预定义变量
        /// <summary>
        /// SQL语句中like条件中的转义符
        /// </summary>
        protected const char LikeEscapeChar = '\\';
        /// <summary>
        /// 对like条件的字符串内容中的转义符进行替换的正则表达式
        /// </summary>
        protected static Regex sqlLike = new Regex(@"([%_\^\[\]\*\\])");
        /// <summary>
        /// 查找列名、表名等的正则表达式
        /// </summary>
        protected static Regex sqlNameRegex = new Regex(@"\[([^\]]+)\]");
        #endregion

        /// <summary>
        /// 表信息提供者
        /// </summary>
        public TableInfoProvider Provider
        {
            get { return DefaultConfiguration.TableInfoProvider; }
        }

        /// <summary>
        /// 根据查询条件生成SQL语句与SQL参数
        /// </summary>
        /// <param name="context">用来生成SQL的上下文</param>
        /// <param name="conditon">查询条件，可为查询条件集合或单个条件，为空表示无条件</param>
        /// <param name="outputParams">供输出的参数列表，在该列表中添加SQL参数</param>
        /// <returns>生成的SQL语句，null表示无条件</returns>
        public virtual string BuildConditionSql(SQLBuildContext context, Condition conditon, IList outputParams)
        {
            if (conditon == null)
                return null;
            else if (conditon is SimpleCondition)
                return BuildSimpleConditionSql(context, conditon as SimpleCondition, outputParams);
            else if (conditon is ConditionSet)
                return BuildConditionSetSql(context, conditon as ConditionSet, outputParams);
            else if (conditon is ForeignCondition)
                return BuildForeignConditionSql(context, conditon as ForeignCondition, outputParams);
            else
                return BuildCustomConditionSql(context, conditon, outputParams);
        }

        /// <summary>
        /// 根据外部对象查询条件生成SQL语句与SQL参数
        /// </summary>
        /// <param name="context">用来生成SQL的上下文</param>
        /// <param name="condition">外部对象的查询条件</param>
        /// <param name="outputParams">供输出的参数列表，在该列表中添加SQL参数</param>
        /// <returns>生成的SQL语句，null表示无条件</returns>
        protected string BuildForeignConditionSql(SQLBuildContext context, ForeignCondition condition, IList outputParams)
        {
            TableDefinition tableDefinition = context.Table.Definition;
            ColumnDefinition joinedColumn = tableDefinition.GetColumn(condition.JoinedProperty);
            Type foreignType = condition.ForeignType;
            if (foreignType == null)
            {
                if (joinedColumn == null) throw new ArgumentException(String.Format("Property {0} not exists.", condition.JoinedProperty), condition.JoinedProperty);
                if (joinedColumn.ForeignType == null) throw new ArgumentException(String.Format("Property {0} does not point to a foreign type.", condition.JoinedProperty), condition.JoinedProperty);
                foreignType = joinedColumn.ForeignType;
            }

            TableDefinition foreignTable = Provider.GetTableDefinition(foreignType);
            ColumnDefinition foreignColumn = foreignTable.GetColumn(condition.ForeignProperty);
            if (joinedColumn == null && foreignColumn == null)
            {
                foreach (ColumnDefinition column in tableDefinition.Columns)
                {
                    if (column.ForeignType == foreignType)
                    {
                        if (joinedColumn != null || foreignColumn != null) throw new ArgumentException(String.Format("Uncertain relation between Type {0} and Type {1}. Please specify the ForeignCondition.JoinedProperty.", context.Table.ObjectType.FullName, foreignTable.ObjectType.FullName), "condition");
                        joinedColumn = column;
                    }
                }

                foreach (ColumnDefinition column in foreignTable.Columns)
                {
                    if (column.ForeignType == context.Table.ObjectType)
                    {
                        if (joinedColumn != null || foreignColumn != null) throw new ArgumentException(String.Format("Uncertain relation between Type {0} and Type {1}. Please specify the ForeignCondition.JoinedProperty.", context.Table.ObjectType.FullName, foreignTable.ObjectType.FullName), "condition");
                        foreignColumn = column;
                    }
                }
                if (joinedColumn == null && foreignColumn == null) throw new ArgumentException(String.Format("No relation between Type {0} and Type {1}", context.Table.ObjectType.FullName, foreignTable.ObjectType.FullName), "condition");
            }

            if (foreignColumn == null)
            {
                if (foreignTable.Keys.Count != 1) throw new ArgumentException(String.Format("Type \"{0}\" does not support foreign condition,which only take effect on type with one and only key column.", foreignType.FullName), "condition");
                foreignColumn = foreignTable.Keys[0];
            }
            else if (joinedColumn == null)
            {
                if (context.Table.Definition.Keys.Count != 1) throw new ArgumentException(String.Format("Type \"{0}\" does not support foreign condition,which only take effect on type with one and only key column.", context.Table.ObjectType.FullName), "condition");
                joinedColumn = context.Table.Definition.Keys[0];
            }

            string tableAlias = context.TableAliasName ?? context.Table.Name;
            string foreignTableAlias = "T" + context.JoinIndex;

            return String.Format("{0} exists (select 1 from [{1}] [{2}] where [{3}].[{4}] = [{5}].[{6}] and ({7}))",
                condition.Opposite ? "not" : null,
                foreignTable.Name,
                foreignTableAlias,
                tableAlias,
                joinedColumn.PropertyName,
                foreignTableAlias,
                foreignColumn.Name,
                SQLBuilder.GetSqlBuilder(foreignType).BuildConditionSql(new SQLBuildContext() { TableAliasName = foreignTableAlias, JoinIndex = context.JoinIndex + 1, Table = foreignTable }, condition.Condition, outputParams));
        }

        /// <summary>
        /// 根据查询条件集合生成SQL语句与SQL参数
        /// </summary>
        /// <param name="context">用来生成SQL的上下文</param>
        /// <param name="conditionSet">查询条件的集合</param>
        /// <param name="outputParams">供输出的参数列表，在该列表中添加SQL参数</param>
        /// <returns>生成的SQL语句，null表示无条件</returns>
        protected string BuildConditionSetSql(SQLBuildContext context, ConditionSet conditionSet, IList outputParams)
        {
            List<string> conditions = new List<string>();
            foreach (Condition subConditon in conditionSet.SubConditions)
            {
                string str = BuildConditionSql(context, subConditon, outputParams);
                if (!String.IsNullOrEmpty(str)) conditions.Add(str);
            }
            if (conditions.Count == 0) return null;
            return String.Format("{0} ({1})", conditionSet.Opposite ? "not" : null, String.Join(" " + conditionSet.JoinType + " ", conditions.ToArray()));
        }

        /// <summary>
        /// 根据简单查询条件生成SQL语句与SQL参数
        /// </summary>
        /// <param name="context">用来生成SQL的上下文</param>
        /// <param name="simpleCondition">简单查询条件</param>
        /// <param name="outputParams">参数列表，在该列表中添加SQL参数</param>
        /// <returns>生成的SQL语句</returns>
        protected string BuildSimpleConditionSql(SQLBuildContext context, SimpleCondition simpleCondition, IList outputParams)
        {
            Column column = context.Table.GetColumn(simpleCondition.Property);
            if (column == null) throw new Exception(String.Format("Property \"{0}\" does not exist in type \"{1}\".", simpleCondition.Property, context.Table.ObjectType.FullName));

            string tableAlias = context.TableAliasName;
            string columnName = tableAlias == null ? column.FormattedExpression : String.Format("[{0}].[{1}]", tableAlias, column.Name);
            string expression = simpleCondition.ExpressionFormat == null ? columnName : String.Format(simpleCondition.ExpressionFormat, columnName);
            object value = simpleCondition.Value;

            string strOpposite = simpleCondition.Opposite ? "not" : null;

            if ((simpleCondition.Value == null || simpleCondition.Value == DBNull.Value) && simpleCondition.Operator == ConditionOperator.Equals)
                return string.Format("{0} is {1} null", expression, strOpposite);

            ConditionOperator positiveOp = simpleCondition.Operator;
            if (positiveOp == ConditionOperator.Contains || positiveOp == ConditionOperator.EndsWith || positiveOp == ConditionOperator.StartsWith)
                value = sqlLike.Replace(Convert.ToString(value), LikeEscapeChar + "$1");
            switch (simpleCondition.Operator)
            {
                case ConditionOperator.Constant: return expression;
                case ConditionOperator.Equals: return String.Format(simpleCondition.Opposite ? "{0} <> {1}" : "{0} = {1}", expression, ToSqlParam(outputParams.Add(value).ToString()));
                case ConditionOperator.LargerThan: return String.Format(simpleCondition.Opposite ? "{0} <= {1}" : "{0} > {1}", expression, ToSqlParam(outputParams.Add(value).ToString()));
                case ConditionOperator.SmallerThan: return String.Format(simpleCondition.Opposite ? "{0} >= {1}" : "{0} < {1}", expression, ToSqlParam(outputParams.Add(value).ToString()));
#if MYSQL
                case ConditionOperator.StartsWith: return String.Format(@"{0} {1} like CONCAT({2}, '%') escape '\{3}'", expression, strOpposite, ToSqlParam(outputParams.Add(value).ToString()), LikeEscapeChar);
                case ConditionOperator.EndsWith: return String.Format(@"{0} {1} like CONCAT('%', {2}) escape '\{3}'", expression, strOpposite, ToSqlParam(outputParams.Add(value).ToString()), LikeEscapeChar);
                case ConditionOperator.Contains: return String.Format(@"{0} {1} like CONCAT('%', {2}, '%') escape '\{3}'", expression, strOpposite, ToSqlParam(outputParams.Add(value).ToString()), LikeEscapeChar);
#elif ORACLE
                case ConditionOperator.StartsWith: return String.Format(@"{0} {1} like {2} || '%' escape '\{3}'", expression, strOpposite, ToSqlParam(outputParams.Add(value).ToString()), LikeEscapeChar);
                case ConditionOperator.EndsWith: return String.Format(@"{0} {1} like '%' || {2} escape '\{3}'", expression, strOpposite, ToSqlParam(outputParams.Add(value).ToString()), LikeEscapeChar);
                case ConditionOperator.Contains: return String.Format(@"{0} {1} like '%' || {2} || '%' escape '\{3}'", expression, strOpposite, ToSqlParam(outputParams.Add(value).ToString()), LikeEscapeChar);

#else
                case ConditionOperator.StartsWith: return String.Format(@"{0} {1} like {2} + '%' escape '\{3}'", expression, strOpposite, ToSqlParam(outputParams.Add(value).ToString()), LikeEscapeChar);
                case ConditionOperator.EndsWith: return String.Format(@"{0} {1} like '%' + {2} escape '\{3}'", expression, strOpposite, ToSqlParam(outputParams.Add(value).ToString()), LikeEscapeChar);
                case ConditionOperator.Contains: return String.Format(@"{0} {1} like '%' + {2} + '%' escape '\{3}'", expression, strOpposite, ToSqlParam(outputParams.Add(value).ToString()), LikeEscapeChar);
#endif
                case ConditionOperator.In:
                    List<string> paramNames = new List<string>();
                    foreach (object item in value as IEnumerable)
                    {
                        paramNames.Add(ToSqlParam(outputParams.Add(item).ToString()));
                    }
                    return String.Format("{0} {1} in ({2})", expression, strOpposite, String.Join(",", paramNames.ToArray()));
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 根据自定义条件生成SQL语句与SQL参数
        /// </summary>
        /// <param name="context">用来生成SQL的上下文</param>
        /// <param name="customConditon">自定义的查询条件</param>
        /// <param name="outputParams">供输出的参数列表，在该列表中添加SQL参数</param>
        /// <returns>生成的SQL语句，null表示无条件</returns>
        protected virtual string BuildCustomConditionSql(SQLBuildContext context, Condition customConditon, IList outputParams)
        {
            throw new Exception(String.Format("Unknown condition type \"{0}\"! Please override the \"BuildCustomConditionSql\" method.", customConditon.GetType().FullName));
        }

        /// <summary>
        /// 名称转化为数据库合法名称
        /// </summary>
        /// <param name="name">字符串名称</param>
        /// <returns>数据库合法名称</returns>
        public virtual string ToSqlName(string name)
        {
            return String.Format("[{0}]", name);
        }

        /// <summary>
        /// 原始名称转化为数据库参数
        /// </summary>
        /// <param name="nativeName">原始名称</param>
        /// <returns>数据库参数</returns>
        public virtual string ToSqlParam(string nativeName)
        {
            return String.Format("@{0}", nativeName);
        }

        /// <summary>
        /// 原始名称转化为参数名称
        /// </summary>
        /// <param name="nativeName">原始名称</param>
        /// <returns>参数名称</returns>
        public virtual string ToParamName(string nativeName)
        {
#if MYSQL
            return String.Format("@{0}", nativeName);
#else
            return nativeName;
#endif
        }

        /// <summary>
        /// 参数名称转化为原始名称
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <returns>原始名称</returns>
        public virtual string ToNativeName(string paramName)
        {
#if MYSQL
            return paramName.TrimStart('@');
#else
            return paramName;
#endif
        }

        /// <summary>
        /// 将列名、表名等替换为数据库合法名称
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public string ReplaceSqlName(string sql)
        {
#if MYSQL
            return sqlNameRegex.Replace(sql, "`$1`");
#else
            return sqlNameRegex.Replace(sql, "[$1]");
#endif
        }
    }

    public class SQLBuildContext
    {
        public string TableAliasName { get; set; }
        public Table Table { get; set; }
        public int JoinIndex { get; set; }
    }
}
