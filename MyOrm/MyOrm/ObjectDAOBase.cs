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
    /// �ṩ���ò���
    /// </summary>
    public abstract class ObjectDAOBase
    {
        #region Ԥ�������
        protected const char LikeEscapeChar = '\\';
        protected const string ParamAllFields = "@AllFields";
        protected const string ParamTable = "@Table";
        protected const string ParamFromTable = "@FromTable";
        protected const string ParamCondition = "@Condition";

        protected static Regex sqlLike = new Regex(@"([%_\^\[\]\*\\])");
        #endregion

        #region ˽�б���
        private IDbConnection connection;
        private string fromTable = null;
        private string allFieldsSql = null;
        private List<ColumnInfo> selectColumns;

        private Exception ExceptionWrongKeys = null;
        private Exception ExceptionNoKeys = null;
        #endregion

        #region ����
        /// <summary>
        /// ���ݿ�����
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
        /// ��ʾ���ݿ�nullֵ
        /// </summary>
        public virtual object DBNullValue
        {
            get { return DBNull.Value; }
        }

        /// <summary>
        /// ����Ϣ
        /// </summary>
        protected abstract TableInfo Table
        {
            get;
        }

        /// <summary>
        /// ����
        /// </summary>
        protected virtual string TableName
        {
            get { return Table.TableName; }
        }

        /// <summary>
        /// ��ѯʱʹ�õ�������Ķ����
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
        /// ��ѯʱ��Ҫ��ȡ��������
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
        /// ��ѯʱ��Ҫ��ȡ�������ֶε�Sql
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

        #region ����
        /// <summary>
        /// ����IDbCommand
        /// </summary>
        /// <returns></returns>
        protected virtual IDbCommand NewCommand()
        {
            return Connection.CreateCommand();
        }

        /// <summary>
        /// ����ת��Ϊ���ݿ�Ϸ�����
        /// </summary>
        /// <param name="name">�ַ�������</param>
        /// <returns>���ݿ�Ϸ�����</returns>
        protected virtual string ToSqlName(string name)
        {
            return String.Format("[{0}]", name);
        }

        /// <summary>
        /// ��������ת��Ϊ���ݿ����
        /// </summary>
        /// <param name="paramName">��������</param>
        /// <returns>���ݿ����</returns>
        protected virtual string ToSqlParam(string paramName)
        {
            return String.Format("@{0}", paramName);
        }

        ///<summary>
        /// ���ݱ��ʽ�������ȽϷ���ֵ�õ�SQL�ַ���
        /// </summary>
        /// <param name="expression">���ʽ</param>
        /// <param name="op">�����ȽϷ�</param>
        /// <param name="value">ֵ</param>
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
        /// ���ݲ�ѯ��������SQL�����SQL����
        /// </summary>
        /// <param name="conditon">��ѯ��������Ϊ��ѯ�������ϻ򵥸�������Ϊ�ձ�ʾ������</param>
        /// <param name="outputParams">������Ĳ����б��ڸ��б������SQL����</param>
        /// <returns>���ɵ�SQL��䣬null��ʾ������</returns>
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
        /// ���ݲ�ѯ������������SQL�����SQL����
        /// </summary>
        /// <param name="conditionSet">��ѯ�����ļ���</param>
        /// <param name="outputParams">������Ĳ����б��ڸ��б������SQL����</param>
        /// <returns>���ɵ�SQL��䣬null��ʾ������</returns>
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
        /// ���ݼ򵥲�ѯ��������SQL�����SQL����
        /// </summary>
        /// <param name="simpleCondition">�򵥲�ѯ����</param>
        /// <param name="outputParams">�����б��ڸ��б������SQL����</param>
        /// <returns>���ɵ�SQL���</returns>
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
        /// �����Զ�����������SQL�����SQL����
        /// </summary>
        /// <param name="conditon">�Զ���Ĳ�ѯ����</param>
        /// <param name="outputParams">������Ĳ����б��ڸ��б������SQL����</param>
        /// <returns>���ɵ�SQL��䣬null��ʾ������</returns>
        protected virtual string BuildCustomConditionSql(Condition customConditon, IList outputParams)
        {
            throw new Exception(String.Format("Unknown condition type \"{0}\"! Please override the \"BuildCustomConditionSql\" method.", customConditon.GetType().FullName));
        }

        /// <summary>
        /// ����SQL���Ͳ�������IDbCommand
        /// </summary>
        /// <param name="SQL">SQL��䣬SQL�п��԰���������Ϣ��������Ϊ��0��ʼ�ĵ�����������ӦparamValues��ֵ���±�</param>
        /// <param name="paramValues">����ֵ����Ҫ��SQL�еĲ���һһ��Ӧ��Ϊ��ʱ��ʾû�в���</param>
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
        /// ����SQL������������IDbCommand
        /// </summary>
        /// <param name="SQLWithParam">��������SQL��䣬��������Ϊ"@AllFields","@Table","@FromTable","@Condition"֮һ
        /// <example>"select @AllFields from @FromTable where @Condition"��ʾ�ӱ��в�ѯ���з��������ļ�¼</example>
        /// <example>"select count(*) from @FromTable "��ʾ�ӱ������м�¼��������conditions������Ϊ��</example>
        /// <example>"delete from @Table where @Condition"��ʾ�ӱ���ɾ�����з��������ļ�¼</example>
        /// </param>        
        /// <param name="conditions">������Ϊnullʱ��ʾ������</param>
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
        /// Ϊcommand��������������ѯ����������command����Ӳ���������where���������
        /// </summary>
        /// <param name="command">Ҫ�������������ݿ�����</param>
        /// <returns>where���������</returns>
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
        /// ��ȡ���������ֵ
        /// </summary>
        /// <param name="o">����</param>
        /// <returns>����ֵ���������������������˳������</returns>
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
        /// �����ݿ�ȡ�õ�ֵת��Ϊ����������������Ӧ��ֵ
        /// </summary>
        /// <param name="value">���ݿ�ȡ�õ�ֵ</param>
        /// <param name="column">������</param>
        /// <returns>����������������Ӧ��ֵ</returns>
        protected object ConvertToObjectValue(object dbValue, ColumnInfo column)//TODO: 
        {
            if (Equals(dbValue, DBNullValue)) dbValue = null;
            if (column.PropertyType.IsValueType && dbValue == null)
                return Activator.CreateInstance(column.PropertyType);
            else
                return dbValue;
        }

        /// <summary>
        /// �����������ֵת��Ϊ���ݿ��е�ֵ
        /// </summary>
        /// <param name="value">ֵ</param>
        /// <param name="column">������</param>
        /// <returns>���ݿ��е�ֵ</returns>
        protected object ConvertToDBValue(object value, ColumnInfo column)//TODO:
        {
            if (value == null) return DBNullValue;
            return value;
        }

        /// <summary>
        /// ����Ƿ���������������������׳��쳣
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
        /// ���������Ŀ�Ƿ���ȷ�������׳��쳣
        /// </summary>
        /// <param name="keys">����</param>
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
