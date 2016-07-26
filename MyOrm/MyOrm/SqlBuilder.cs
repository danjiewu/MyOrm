using System;
using System.Collections.Generic;
using System.Text;
using MyOrm.Common;
using System.Text.RegularExpressions;
using System.Collections;
using System.Data;
using System.Data.Common;

namespace MyOrm
{
    /// <summary>
    /// 生成Sql语句的辅助类
    /// </summary>
    public class SqlBuilder : IConditionSqlBuilder, ISqlBuilder
    {
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

        private Dictionary<Type, IConditionSqlBuilder> extCondtionBuilders = new Dictionary<Type, IConditionSqlBuilder>();
        #endregion

        /// <summary>
        /// 表信息提供者
        /// </summary>
        public TableInfoProvider Provider
        {
            get { return Configuration.DefaultProvider; }
        }

        /// <summary>
        /// 注册自定义条件SQL生成类
        /// </summary>
        /// <param name="conditionType">自定义条件类型</param>
        /// <param name="conditionBuilder">自定义条件SQL生成类</param>
        public void RegisterConditionBuilder(Type conditionType, IConditionSqlBuilder conditionBuilder)
        {
            extCondtionBuilders[conditionType] = conditionBuilder;
        }

        /// <summary>
        /// 根据查询条件生成SQL语句与SQL参数
        /// </summary>
        /// <param name="context">用来生成SQL的上下文</param>
        /// <param name="conditon">查询条件，可为查询条件集合或单个条件，为空表示无条件</param>
        /// <param name="outputParams">供输出的参数列表，在该列表中添加SQL参数</param>
        /// <returns>生成的SQL语句，null表示无条件</returns>
        public virtual string BuildConditionSql(SqlBuildContext context, Condition conditon, IList outputParams)
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
            {
                if (extCondtionBuilders.ContainsKey(conditon.GetType()))
                {
                    return extCondtionBuilders[conditon.GetType()].BuildConditionSql(context, conditon, outputParams);
                }
                else throw new Exception(String.Format("Unsupported condition type \"{0}\"! Please register ConditionBuilder before call BuildConditionSql method.", conditon.GetType().FullName));
            }
        }

        /// <summary>
        /// 根据外部对象查询条件生成SQL语句与SQL参数
        /// </summary>
        /// <param name="context">用来生成SQL的上下文</param>
        /// <param name="condition">外部对象的查询条件</param>
        /// <param name="outputParams">供输出的参数列表，在该列表中添加SQL参数</param>
        /// <returns>生成的SQL语句，null表示无条件</returns>
        protected string BuildForeignConditionSql(SqlBuildContext context, ForeignCondition condition, IList outputParams)
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
                        if (joinedColumn != null || foreignColumn != null) throw new ArgumentException(String.Format("Uncertain relation between Type {0} and Type {1}. Please specify the ForeignCondition.JoinedProperty.", context.Table.DefinitionType.FullName, foreignTable.ObjectType.FullName), "condition");
                        joinedColumn = column;
                    }
                }

                foreach (ColumnDefinition column in foreignTable.Columns)
                {
                    if (column.ForeignType == context.Table.DefinitionType)
                    {
                        if (joinedColumn != null || foreignColumn != null) throw new ArgumentException(String.Format("Uncertain relation between Type {0} and Type {1}. Please specify the ForeignCondition.JoinedProperty.", context.Table.DefinitionType.FullName, foreignTable.ObjectType.FullName), "condition");
                        foreignColumn = column;
                    }
                }
                if (joinedColumn == null && foreignColumn == null) throw new ArgumentException(String.Format("No relation between Type {0} and Type {1}", context.Table.DefinitionType.FullName, foreignTable.ObjectType.FullName), "condition");
            }

            if (foreignColumn == null)
            {
                if (foreignTable.Keys.Count != 1) throw new ArgumentException(String.Format("Type \"{0}\" does not support foreign condition,which only take effect on type with one and only key column.", foreignType.FullName), "condition");
                foreignColumn = foreignTable.Keys[0];
            }
            else if (joinedColumn == null)
            {
                if (context.Table.Definition.Keys.Count != 1) throw new ArgumentException(String.Format("Type \"{0}\" does not support foreign condition,which only take effect on type with one and only key column.", context.Table.DefinitionType.FullName), "condition");
                joinedColumn = context.Table.Definition.Keys[0];
            }

            string tableAlias = context.TableAliasName ?? context.Table.Name;
            string foreignTableAlias = "T" + context.Sequence;

            return String.Format("{0}exists (select 1 from [{1}] [{2}] where [{3}].[{4}] = [{5}].[{6}] and ({7}))",
                condition.Opposite ? "not " : null,
                foreignTable.Name,
                foreignTableAlias,
                tableAlias,
                joinedColumn.PropertyName,
                foreignTableAlias,
                foreignColumn.Name,
                BuildConditionSql(new SqlBuildContext() { TableAliasName = foreignTableAlias, Sequence = context.Sequence + 1, Table = foreignTable }, condition.Condition, outputParams));
        }

        /// <summary>
        /// 根据查询条件集合生成SQL语句与SQL参数
        /// </summary>
        /// <param name="context">用来生成SQL的上下文</param>
        /// <param name="conditionSet">查询条件的集合</param>
        /// <param name="outputParams">供输出的参数列表，在该列表中添加SQL参数</param>
        /// <returns>生成的SQL语句，null表示无条件</returns>
        protected string BuildConditionSetSql(SqlBuildContext context, ConditionSet conditionSet, IList outputParams)
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
        protected string BuildSimpleConditionSql(SqlBuildContext context, SimpleCondition simpleCondition, IList outputParams)
        {
            Column column = context.Table.GetColumn(simpleCondition.Property);
            if (column == null) throw new Exception(String.Format("Property \"{0}\" does not exist in type \"{1}\".", simpleCondition.Property, context.Table.DefinitionType.FullName));

            string tableAlias = context.TableAliasName;
            string columnName = tableAlias == null ? column.FormattedExpression(this) : String.Format("[{0}].[{1}]", tableAlias, column.Name);
            string expression = columnName;
            object value = simpleCondition.Value;

            string strOpposite = simpleCondition.Opposite ? "not" : null;

            if ((simpleCondition.Value == null || simpleCondition.Value == DBNull.Value) && simpleCondition.Operator == ConditionOperator.Equals)
                return string.Format("{0} is {1} null", expression, strOpposite);

            ConditionOperator positiveOp = simpleCondition.Operator;
            if (positiveOp == ConditionOperator.Contains || positiveOp == ConditionOperator.EndsWith || positiveOp == ConditionOperator.StartsWith)
                value = sqlLike.Replace(Convert.ToString(value), LikeEscapeChar + "$1");
            switch (simpleCondition.Operator)
            {
                case ConditionOperator.Equals: return String.Format(simpleCondition.Opposite ? "{0} <> {1}" : "{0} = {1}", expression, ToSqlParam(outputParams.Add(value).ToString()));
                case ConditionOperator.LargerThan: return String.Format(simpleCondition.Opposite ? "{0} <= {1}" : "{0} > {1}", expression, ToSqlParam(outputParams.Add(value).ToString()));
                case ConditionOperator.SmallerThan: return String.Format(simpleCondition.Opposite ? "{0} >= {1}" : "{0} < {1}", expression, ToSqlParam(outputParams.Add(value).ToString()));
                case ConditionOperator.Like: return String.Format(@"{0} {1} like {2}", expression, strOpposite, ToSqlParam(outputParams.Add(value).ToString()));
                case ConditionOperator.StartsWith: return String.Format(@"{0} {1} like {2} escape '{3}'", expression, strOpposite, ConcatSql(ToSqlParam(outputParams.Add(value).ToString()), "'%'"), LikeEscapeChar);
                case ConditionOperator.EndsWith: return String.Format(@"{0} {1} like {2} escape '{3}'", expression, strOpposite, ConcatSql("'%'", ToSqlParam(outputParams.Add(value).ToString())), LikeEscapeChar);
                case ConditionOperator.Contains: return String.Format(@"{0} {1} like {2} escape '{3}'", expression, strOpposite, ConcatSql("'%'", ToSqlParam(outputParams.Add(value).ToString()), "'%'"), LikeEscapeChar);
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
        /// 连接各字符串的SQL语句
        /// </summary>
        /// <param name="strs">需要连接的sql字符串</param>
        /// <returns>SQL语句</returns>
        protected virtual string ConcatSql(params string[] strs)
        {
            return String.Join(" + ", strs);
        }

        /// <summary>
        /// 生成分页查询的SQL语句
        /// </summary>
        /// <param name="select">select内容</param>
        /// <param name="from">from块</param>
        /// <param name="where">where条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="startIndex">起始位置，从0开始</param>
        /// <param name="sectionSize">查询条数</param>
        /// <returns></returns>
        public virtual string GetSelectSectionSql(string select, string from, string where, string orderBy, int startIndex, int sectionSize)
        {
            return String.Format("select * from (select {0}, Row_Number() over (Order by {1}) as Row_Number from {2} where {3}) TempTable where Row_Number > {4} and Row_Number <= {5}", select, orderBy, from, where, startIndex, startIndex + sectionSize);
        }

        /// <summary>
        /// 名称转化为数据库合法名称
        /// </summary>
        /// <param name="name">字符串名称</param>
        /// <returns>数据库合法名称</returns>
        public virtual string ToSqlName(string name)
        {
            if (name == null) throw new ArgumentNullException("name");
            return String.Join(".", Array.ConvertAll(name.Split('.'), n => String.Format("[{0}]", n)));
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
            return nativeName;
        }

        /// <summary>
        /// 参数名称转化为原始名称
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <returns>原始名称</returns>
        public virtual string ToNativeName(string paramName)
        {
            return paramName;
        }

        /// <summary>
        /// 将列名、表名等替换为数据库合法名称
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public virtual string ReplaceSqlName(string sql)
        {
            return sql;
        }

        /// <summary>
        /// 将列名、表名等替换为数据库合法名称
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="left">左定界符</param>
        /// <param name="right">右定界符</param>
        /// <returns></returns>
        protected string ReplaceSqlName(string sql, char left, char right)
        {
            if (sql == null) return null;
            StringBuilder sb = new StringBuilder();
            bool passNext = false;
            Stack<char> stack = new Stack<char>();
            foreach (char ch in sql)
            {
                if (passNext)
                {
                    sb.Append(ch);
                    passNext = false;
                }
                else
                {
                    switch (ch)
                    {
                        case '[': sb.Append(stack.Count == 0 ? left : ch); break;
                        case ']': sb.Append(stack.Count == 0 ? right : ch); break;
                        case '"':
                            if (stack.Count > 0 && stack.Peek() == '"') stack.Pop();
                            else stack.Push('"');
                            sb.Append(ch); break;
                        case '\'':
                            if (stack.Count > 0 && stack.Peek() == '\'') stack.Pop();
                            else stack.Push('\'');
                            sb.Append(ch); break;
                        case '\\': sb.Append(ch); passNext = true; break;
                        default: sb.Append(ch); break;
                    }
                }
            }
            return sb.ToString();
        }
    }

    /// <summary>
    /// 自定义Condition转换为sql语句的接口
    /// </summary>
    public interface IConditionSqlBuilder
    {
        /// <summary>
        /// 生成sql语句
        /// </summary>
        /// <param name="context">生成sql的上下文</param>
        /// <param name="customConditon">自定义Condition</param>
        /// <param name="outputParams">存放参数的集合</param>
        /// <returns>生成的sql字符串</returns>
        string BuildConditionSql(SqlBuildContext context, Condition customConditon, IList outputParams);
    }
}
