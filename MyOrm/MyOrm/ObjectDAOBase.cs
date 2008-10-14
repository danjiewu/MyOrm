using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using MyOrm.Metadata;
using MyOrm.Common;

namespace MyOrm
{
    /// <summary>
    /// 提供常用操作
    /// </summary>
    public abstract class ObjectDAOBase
    {
        #region 预定义变量
        protected const char LikeEscapeChar = '\\';
        protected const string ParamAllFields = "@AllFields";
        protected const string ParamTable = "@Table";
        protected const string ParamFromTable = "@FromTable";
        protected const string ParamCondition = "@Condition";

        protected static Regex sqlLike = new Regex(@"([%_\^\[\]\*\\])");
        #endregion

        #region 私有变量
        private IDbConnection connection;
        private string fromTable = null;
        private string allFieldsSql = null;
        private List<ColumnInfo> selectColumns;

        private Exception ExceptionWrongKeys = null;
        private Exception ExceptionNoKeys = null;
        #endregion

        #region 属性
        /// <summary>
        /// 数据库连接
        /// </summary>
        public virtual IDbConnection Connection
        {
            get
            {
                if (connection == null) connection = Configuration.DefaultConnection;
                return connection;
            }
        }

        /// <summary>
        /// 表示数据库null值
        /// </summary>
        public virtual object DBNullValue
        {
            get { return DBNull.Value; }
        }

        /// <summary>
        /// 表信息
        /// </summary>
        protected abstract TableInfo Table
        {
            get;
        }

        /// <summary>
        /// 表名
        /// </summary>
        protected virtual string TableName
        {
            get { return Table.TableName; }
        }

        /// <summary>
        /// 查询时使用的相关联的多个表
        /// </summary>
        protected virtual string FromTable
        {
            get
            {
                if (fromTable == null)
                {
                    StringBuilder strFromTable = new StringBuilder(ToSqlName(TableName));
                    foreach (TableJoinInfo tableJoin in Table.JoinTables)
                    {
                        if (tableJoin.ForeignKeys.Count != tableJoin.TargetTable.Keys.Count) throw new Exception(String.Format("Different number between foreign keys of table \"{0}\" and primary keys of table \"{1}\".  ", TableName, tableJoin.TargetTable.TableName));
                        StringBuilder strConditions = new StringBuilder();
                        int index = 0;
                        foreach (ColumnInfo key in tableJoin.TargetTable.Keys)
                        {
                            if (index != 0) strConditions.Append(" and ");
                            strConditions.AppendFormat("{0}.{1} = {2}.{3}", ToSqlName(String.IsNullOrEmpty(tableJoin.SourceTable) ? TableName : tableJoin.SourceTable), ToSqlName(tableJoin.ForeignKeys[index]), ToSqlName(string.IsNullOrEmpty(tableJoin.AliasName) ? tableJoin.TargetTable.TableName : tableJoin.AliasName), ToSqlName(key.ColumnName));
                        }
                        strFromTable.AppendFormat(" {0} join {1} {2} on {3}", tableJoin.JoinType, ToSqlName(tableJoin.TargetTable.TableName), string.IsNullOrEmpty(tableJoin.AliasName) ? null : ToSqlName(tableJoin.AliasName), strConditions);
                    }
                    fromTable = strFromTable.ToString();
                }
                return fromTable;
            }
        }

        /// <summary>
        /// 查询时需要获取的所有列
        /// </summary>
        protected virtual List<ColumnInfo> SelectColumns
        {
            get
            {
                if (selectColumns == null) selectColumns = Table.Columns.FindAll(delegate(ColumnInfo column) { return (column.Mode & ColumnMode.Read) != ColumnMode.Ignore; });
                return selectColumns;
            }
        }

        /// <summary>
        /// 查询时需要获取的所有字段的Sql
        /// </summary>
        protected virtual string AllFieldsSql
        {
            get
            {
                if (allFieldsSql == null)
                {
                    StringBuilder strAllFields = new StringBuilder();
                    foreach (ColumnInfo column in SelectColumns)
                    {
                        if (strAllFields.Length != 0) strAllFields.Append(",");
                        strAllFields.AppendFormat("{0}.{1} as {2}", ToSqlName(String.IsNullOrEmpty(column.ForeignTable) ? TableName : column.ForeignTable), ToSqlName(column.ColumnName), ToSqlName(column.PropertyName));
                    }
                    allFieldsSql = strAllFields.ToString();
                }
                return allFieldsSql;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 创建IDbCommand
        /// </summary>
        /// <returns></returns>
        protected virtual IDbCommand NewCommand()
        {
            return Connection.CreateCommand();
        }

        /// <summary>
        /// 名称转化为数据库合法名称
        /// </summary>
        /// <param name="name">字符串名称</param>
        /// <returns>数据库合法名称</returns>
        protected virtual string ToSqlName(string name)
        {
            return String.Format("[{0}]", name);
        }

        /// <summary>
        /// 参数名称转化为数据库参数
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <returns>数据库参数</returns>
        protected virtual string ToSqlParam(string paramName)
        {
            return String.Format("@{0}", paramName);
        }

        ///<summary>
        /// 根据表达式、条件比较符、值得到SQL字符串
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="op">条件比较符</param>
        /// <param name="value">值</param>
        /// <returns></returns>
        protected string BuildSql(string expression, ConditionOperator op, string value)//TODO
        {
            switch (op)
            {
                case ConditionOperator.Equals: return String.Format("{0} = {1}", expression, value);
                case ConditionOperator.NotEquals: return String.Format("{0} <> {1}", expression, value);
                case ConditionOperator.LargerThan: return String.Format("{0} > {1}", expression, value);
                case ConditionOperator.SmallerThan: return String.Format("{0} < {1}", expression, value);
                case ConditionOperator.NotLargerThan: return String.Format("{0} <= {1}", expression, value);
                case ConditionOperator.NotSmallerThan: return String.Format("{0} >= {1}", expression, value);
                case ConditionOperator.StartsWith: return String.Format("{0} like {1} + '%' escape '{2}'", expression, value, LikeEscapeChar);
                case ConditionOperator.EndsWith: return String.Format("{0} like '%' + {1} escape '{2}'", expression, value, LikeEscapeChar);
                case ConditionOperator.Contains: return String.Format("{0} like '%' + {1} + '%' escape '{2}'", expression, value, LikeEscapeChar);
                case ConditionOperator.NotStartsWith: return String.Format("{0} not like {1} + '%' escape '{2}'", expression, value, LikeEscapeChar);
                case ConditionOperator.NotEndsWith: return String.Format("{0} not like '%' + {1} escape '{2}'", expression, value, LikeEscapeChar);
                case ConditionOperator.NotContains: return String.Format("{0} not like '%' + {1} + '%' escape '{2}'", expression, value, LikeEscapeChar);
                default:
                    return string.Empty;
            }
        }

        /// <summary>
        /// 根据查询条件生成SQL语句与SQL参数
        /// </summary>
        /// <param name="conditon">查询条件，可为查询条件集合或单个条件，为空表示无条件</param>
        /// <param name="outputParams">供输出的参数列表，在该列表中添加SQL参数</param>
        /// <returns>生成的SQL语句，null表示无条件</returns>
        protected string BuildConditionSql(Condition conditon, IList outputParams)
        {
            if (conditon == null)
                return null;
            else if (conditon is SimpleCondition)
                return BuildSimpleConditionSql(conditon as SimpleCondition, outputParams);
            else if (conditon is ConditionSet)
                return BuildConditionSetSql(conditon as ConditionSet, outputParams);
            else
                return BuildCustomConditionSql(conditon, outputParams);
        }

        /// <summary>
        /// 根据查询条件集合生成SQL语句与SQL参数
        /// </summary>
        /// <param name="conditionSet">查询条件的集合</param>
        /// <param name="outputParams">供输出的参数列表，在该列表中添加SQL参数</param>
        /// <returns>生成的SQL语句，null表示无条件</returns>
        protected string BuildConditionSetSql(ConditionSet conditionSet, IList outputParams)
        {
            List<string> conditions = new List<string>();
            foreach (Condition subConditon in conditionSet.SubConditions)
            {
                string str = BuildConditionSql(subConditon, outputParams);
                if (!String.IsNullOrEmpty(str)) conditions.Add(str);
            }
            if (conditions.Count == 0) return null;
            return "(" + String.Join(" " + conditionSet.JoinType + " ", conditions.ToArray()) + ")";
        }

        /// <summary>
        /// 根据简单查询条件生成SQL语句与SQL参数
        /// </summary>
        /// <param name="simpleCondition">简单查询条件</param>
        /// <param name="outputParams">参数列表，在该列表中添加SQL参数</param>
        /// <returns>生成的SQL语句</returns>
        protected string BuildSimpleConditionSql(SimpleCondition simpleCondition, IList outputParams)
        {
            string expression;
            if (simpleCondition.ExpressionType == ExpressionType.Property)
            {
                ColumnInfo column = Table.GetColumnByProperty(simpleCondition.Expression);
                if (column == null)
                    throw new Exception(String.Format("Property \"{0}\" does not exist in type \"{1}\".", simpleCondition.Expression, Table.ObjectType.FullName));
                else
                    expression = string.Format("{0}.{1}", ToSqlName(String.IsNullOrEmpty(column.ForeignTable) ? TableName : column.ForeignTable), ToSqlName(column.ColumnName));
            }
            else
            {
                expression = simpleCondition.Expression;//TODO
            }

            if ((simpleCondition.Value == null || simpleCondition.Value == DBNullValue) && simpleCondition.Operator == ConditionOperator.Equals)
                return string.Format("{0} is null", expression);
            else if ((simpleCondition.Value == null || simpleCondition.Value == DBNullValue) && simpleCondition.Operator == ConditionOperator.NotEquals)
                return string.Format("{0} is not null", expression);
            else
            {
                object value = simpleCondition.Value;
                ConditionOperator positiveOp = simpleCondition.Operator & ConditionOperator.Not;
                if (positiveOp == ConditionOperator.Contains || positiveOp == ConditionOperator.EndsWith || positiveOp == ConditionOperator.StartsWith)
                    value = sqlLike.Replace(Convert.ToString(value), LikeEscapeChar + "$1");
                outputParams.Add(value);
                return BuildSql(expression, simpleCondition.Operator, ToSqlParam(Convert.ToString(outputParams.Count - 1)));
            }
        }

        /// <summary>
        /// 根据自定义条件生成SQL语句与SQL参数
        /// </summary>
        /// <param name="conditon">自定义的查询条件</param>
        /// <param name="outputParams">供输出的参数列表，在该列表中添加SQL参数</param>
        /// <returns>生成的SQL语句，null表示无条件</returns>
        protected virtual string BuildCustomConditionSql(Condition customConditon, IList outputParams)
        {
            throw new Exception(String.Format("Unknown condition type \"{0}\"! Please override the \"BuildCustomConditionSql\" method.", customConditon.GetType().FullName));
        }

        /// <summary>
        /// 根据SQL语句和参数建立IDbCommand
        /// </summary>
        /// <param name="SQL">SQL语句，SQL中可以包含参数信息，参数名为以0开始的递增整数，对应paramValues中值的下标</param>
        /// <param name="paramValues">参数值，需要与SQL中的参数一一对应，为空时表示没有参数</param>
        /// <returns>IDbCommand</returns>
        protected IDbCommand MakeParamCommand(string SQL, IEnumerable paramValues)
        {
            int paramIndex = 0;
            IDbCommand command = NewCommand();
            if (paramValues != null)
                foreach (object paramValue in paramValues)
                {
                    String paramName = Convert.ToString(paramIndex++);
                    IDataParameter param = command.CreateParameter();
                    param.ParameterName = paramName;
                    param.Value = paramValue;
                    command.Parameters.Add(param);
                }
            command.CommandText = SQL;
            return command;
        }

        /// <summary>
        /// 根据SQL语句和条件建立IDbCommand
        /// </summary>
        /// <param name="SQLWithParam">带参数的SQL语句，参数可以为"@AllFields","@Table","@FromTable","@Condition"之一
        /// <example>"select @AllFields from @FromTable where @Condition"表示从表中查询所有符合条件的记录</example>
        /// <example>"select count(*) from @FromTable "表示从表中所有记录的数量，conditions参数需为空</example>
        /// <example>"delete from @Table where @Condition"表示从表中删除所有符合条件的记录</example>
        /// </param>        
        /// <param name="conditions">条件，为null时表示无条件</param>
        /// <returns>IDbCommand</returns>
        protected IDbCommand MakeConditionCommand(string SQLWithParam, Condition condition)
        {
            List<object> paramList = new List<object>();
            string strCondition = BuildConditionSql(condition, paramList);
            if (String.IsNullOrEmpty(strCondition)) strCondition = " 1 = 1 ";
            string strSQL = SQLWithParam.Replace(ParamAllFields, AllFieldsSql).Replace(ParamTable, ToSqlName(TableName)).Replace(ParamFromTable, FromTable).Replace(ParamCondition, strCondition);
            return MakeParamCommand(strSQL, paramList);
        }

        /// <summary>
        /// 为command创建根据主键查询的条件，在command中添加参数并返回where条件的语句
        /// </summary>
        /// <param name="command">要创建条件的数据库命令</param>
        /// <returns>where条件的语句</returns>
        protected string MakeIsKeyCondition(IDbCommand command)
        {
            ThrowExceptionIfNoKeys();
            StringBuilder strConditions = new StringBuilder();
            foreach (ColumnInfo key in Table.Keys)
            {
                if (strConditions.Length == 0) strConditions.Append(" and ");
                strConditions.AppendFormat("{0}.{1} = {2}", ToSqlName(TableName), ToSqlName(key.ColumnName), ToSqlParam(key.ColumnName));
                IDataParameter param = command.CreateParameter();
                param.DbType = key.DbType;
                param.ParameterName = key.ColumnName;
                command.Parameters.Add(param);
            }
            return strConditions.ToString();
        }

        /// <summary>
        /// 获取对象的主键值
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>主键值，多个主键按照属性名称顺序排列</returns>
        protected virtual object[] GetKeyValues(object o)
        {
            List<object> values = new List<object>();
            foreach (ColumnInfo key in Table.Keys)
            {
                values.Add(key.GetValue(o));
            }
            return values.ToArray();
        }

        /// <summary>
        /// 将数据库取得的值转化为对象属性类型所对应的值
        /// </summary>
        /// <param name="value">数据库取得的值</param>
        /// <param name="column">列属性</param>
        /// <returns>对象属性类型所对应的值</returns>
        protected object ConvertToObjectValue(object dbValue, ColumnInfo column)//TODO: 
        {
            if (Equals(dbValue, DBNullValue)) dbValue = null;
            if (column.PropertyType.IsValueType && dbValue == null)
                return Activator.CreateInstance(column.PropertyType);
            else
                return dbValue;
        }

        /// <summary>
        /// 将对象的属性值转化为数据库中的值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="column">列属性</param>
        /// <returns>数据库中的值</returns>
        protected object ConvertToDBValue(object value, ColumnInfo column)//TODO:
        {
            if (value == null) return DBNullValue;
            return value;
        }

        /// <summary>
        /// 检查是否存在主键，若不存在则抛出异常
        /// </summary>
        protected void ThrowExceptionIfNoKeys()
        {
            if (Table.Keys.Count == 0)
            {
                if (ExceptionNoKeys == null) ExceptionNoKeys = new Exception(String.Format("No key definition found in type \"{0}\", please set the value of property \"IsPrimaryKey\" of key column to true.", Table.ObjectType.FullName));
                throw ExceptionNoKeys;
            }
        }

        /// <summary>
        /// 检查主键数目是否正确，否则抛出异常
        /// </summary>
        /// <param name="keys">主键</param>
        protected void ThrowExceptionIfWrongKeys(params object[] keys)
        {
            if (keys.Length != Table.Keys.Count)
            {
                if (ExceptionWrongKeys == null)
                {
                    List<string> strKeys = new List<string>();
                    foreach (ColumnInfo key in Table.Keys) strKeys.Add(key.ColumnName);
                    ExceptionWrongKeys = new ArgumentOutOfRangeException("keys", String.Format("Wrong keys' number. Type \"{0}\" has {1} key(s):'{2}'.", Table.ObjectType.FullName, strKeys.Count, String.Join("','", strKeys.ToArray())));
                }
                throw ExceptionWrongKeys;
            }
        }
        #endregion
    }
}
