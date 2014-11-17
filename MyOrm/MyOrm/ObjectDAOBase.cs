using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using MyOrm.Common;
using System.Data.Common;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace MyOrm
{
    /// <summary>
    /// �ṩ���ò���
    /// </summary>
    public abstract class ObjectDAOBase
    {
        #region Ԥ�������
        /// <summary>
        /// ��ʾSQL��ѯ���������ı��
        /// </summary>
        public const string ParamCondition = "@Condition@";
        /// <summary>
        /// ��ʾSQL��ѯ�б����ı��
        /// </summary>
        public const string ParamTable = "@Table@";
        /// <summary>
        /// ��ʾSQL��ѯ�ж�����ӵı��
        /// </summary>
        public const string ParamFromTable = "@FromTable@";
        /// <summary>
        /// ��ʾSQL��ѯ�������ֶεı��
        /// </summary>
        public const string ParamAllFields = "@AllFields@";
        #endregion

        #region ˽�б���
        private ReadOnlyCollection<Column> selectColumns;
        private string allFieldsSql = null;
        private string tableName = null;
        private string fromTable = null;
        private ArgumentOutOfRangeException ExceptionWrongKeys;
        #endregion

        #region ����
        /// <summary>
        /// ʵ���������
        /// </summary>
        public abstract Type ObjectType
        {
            get;
        }

        /// <summary>
        /// ����Ϣ
        /// </summary>
        protected abstract Table Table
        {
            get;
        }

        /// <summary>
        /// ����
        /// </summary>
        protected TableDefinition TableDefinition
        {
            get { return Table.Definition; }
        }

        /// <summary>
        /// ����SQL����SQLBuilder
        /// </summary>
        protected internal virtual SqlBuilder SqlBuilder
        {
            get { return Configuration.DefaultSqlBuilder; }
        }

        /// <summary>
        /// 
        /// </summary>
        public SessionManager SessionManager { get; set; }

        /// <summary>
        /// ��ǰ����sql��������
        /// </summary>
        protected SqlBuildContext CurrentContext
        {
            get { return new SqlBuildContext() { Table = Table }; }
        }

        /// <summary>
        /// ����Ϣ�ṩ��
        /// </summary>
        protected virtual TableInfoProvider Provider
        {
            get { return Configuration.DefaultProvider; }
        }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public IDbConnection Connection
        {
            get
            {
                if (SessionManager == null) return null;
                return SessionManager.Connection;
            }
        }

        /// <summary>
        /// ����
        /// </summary>
        protected string TableName
        {
            get
            {
                if (tableName == null) tableName = Table.Name;
                return tableName;
            }
        }

        /// <summary>
        /// ��ѯʱʹ�õ�������Ķ����
        /// </summary>
        protected virtual string From
        {
            get
            {
                if (fromTable == null)
                {
                    fromTable = Table.FormattedExpression;
                }
                return fromTable;
            }
        }

        /// <summary>
        /// ��ѯʱ��Ҫ��ȡ��������
        /// </summary>
        protected virtual ReadOnlyCollection<Column> SelectColumns
        {
            get
            {
                if (selectColumns == null)
                {
                    selectColumns = new List<Column>(Table.Columns).FindAll(column => !(column is ColumnDefinition && (((ColumnDefinition)column).Mode & ColumnMode.Read) != ColumnMode.Read)).AsReadOnly();
                }
                return selectColumns;
            }
        }

        /// <summary>
        /// ��ѯʱ��Ҫ��ȡ�������ֶε�Sql
        /// </summary>
        protected string AllFieldsSql
        {
            get
            {
                if (allFieldsSql == null)
                {
                    allFieldsSql = GetSelectFieldsSQL(SelectColumns);
                }
                return allFieldsSql;
            }
        }
        #endregion

        #region ����
        /// <summary>
        /// Ԥ����Command�Ƿ�ʹ��Prepare����
        /// </summary>
        protected virtual bool PrepareCommand
        {
            get { return true; }
        }

        /// <summary>
        /// ����IDbCommand
        /// </summary>
        /// <returns></returns>
        public virtual IDbCommand NewCommand()
        {
            return Configuration.UseAutoCommand ? new AutoCommand(this) : Connection.CreateCommand();
        }

        /// <summary>
        /// ����select���ֵ�sql
        /// </summary>
        /// <param name="selectColumns">��Ҫselect���м���</param>
        /// <returns>���ɵ�sql</returns>
        protected string GetSelectFieldsSQL(IEnumerable<Column> selectColumns)
        {
            StringBuilder strAllFields = new StringBuilder();
            foreach (Column column in selectColumns)
            {
                if (strAllFields.Length != 0) strAllFields.Append(",");
                strAllFields.Append(column.FormattedExpression);
                if (!String.Equals(column.Name, column.PropertyName, StringComparison.OrdinalIgnoreCase)) strAllFields.Append(" as " + column.FormattedPropertyName);
            }
            return strAllFields.ToString();
        }

        /// <summary>
        /// ����orderby���ֵ�sql
        /// </summary>
        /// <param name="orders">������ļ��ϣ������ȼ�˳������</param>
        /// <returns></returns>
        protected string GetOrderBySQL(Sorting[] orders)
        {
            StringBuilder orderBy = new StringBuilder();
            if (orders == null || orders.Length == 0)
            {
                if (TableDefinition.Keys.Count != 0)
                {
                    foreach (ColumnDefinition key in TableDefinition.Keys)
                    {
                        if (orderBy.Length != 0) orderBy.Append(",");
                        orderBy.AppendFormat("{0}.{1}", Table.FormattedName, key.FormattedName);
                    }
                }
                else
                {
                    //TODO: OrderBy one column or all columns?
                    throw new Exception("No columns or keys to sort by.");
                }
            }
            else
            {
                foreach (Sorting sorting in orders)
                {
                    Column column = Table.GetColumn(sorting.PropertyName);
                    if (column == null) throw new ArgumentException(String.Format("Type \"{0}\" does not have property \"{1}\"", ObjectType.Name, sorting.PropertyName), "section");
                    if (orderBy.Length > 0) orderBy.Append(",");
                    orderBy.Append(column.FormattedExpression);
                    orderBy.Append(sorting.Direction == ListSortDirection.Ascending ? " asc" : " desc");
                }
            }
            return orderBy.ToString();
        }

        /// <summary>
        /// ����SQL���Ͳ�������IDbCommand
        /// </summary>
        /// <param name="SQL">SQL��䣬SQL�п��԰���������Ϣ��������Ϊ��0��ʼ�ĵ�����������ӦparamValues��ֵ���±�</param>
        /// <param name="paramValues">����ֵ����Ҫ��SQL�еĲ���һһ��Ӧ��Ϊ��ʱ��ʾû�в���</param>
        /// <returns>IDbCommand</returns>
        public IDbCommand MakeParamCommand(string SQL, IEnumerable paramValues)
        {
            int paramIndex = 0;
            SortedList<string, object> paramList = new SortedList<string, object>();
            if (paramValues != null)
                foreach (object paramValue in paramValues)
                {
                    paramList.Add(Convert.ToString(paramIndex++), paramValue);

                }
            return MakeNamedParamCommand(SQL, paramList);
        }

        /// <summary>
        /// ����SQL���Ͳ�������IDbCommand
        /// </summary>
        /// <param name="SQL">SQL��䣬SQL�п��԰���������Ϣ��������Ϊ��0��ʼ�ĵ�����������ӦparamValues��ֵ���±�</param>
        /// <param name="paramValues">����ֵ����Ҫ��SQL�еĲ���һһ��Ӧ��Ϊ��ʱ��ʾû�в���</param>
        /// <returns>IDbCommand</returns>
        public IDbCommand MakeParamCommand(string SQL, params object[] paramValues)
        {
            return MakeParamCommand(SQL, (IEnumerable)paramValues);
        }

        /// <summary>
        /// ����SQL���������Ĳ�������IDbCommand
        /// </summary>
        /// <param name="SQL">SQL��䣬SQL�п��԰����������Ĳ���</param>
        /// <param name="paramValues">�����б�Ϊ��ʱ��ʾû�в�����Key��Ҫ��SQL�еĲ������ƶ�Ӧ</param>
        /// <returns>IDbCommand</returns>
        public IDbCommand MakeNamedParamCommand(string SQL, IEnumerable<KeyValuePair<string, object>> paramValues)
        {
            IDbCommand command = NewCommand();
            command.CommandText = SQL;
            AddParamsToCommand(command, paramValues);
            return command;
        }

        /// <summary>
        /// ��������ӵ�IDbCommand��
        /// </summary>
        /// <param name="command">��Ҫ��Ӳ�����IDbCommand</param>
        /// <param name="paramValues">�����б������������ƺ�ֵ��Ϊ��ʱ��ʾû�в���</param>
        public void AddParamsToCommand(IDbCommand command, IEnumerable<KeyValuePair<string, object>> paramValues)
        {
            if (paramValues != null)
                foreach (KeyValuePair<string, object> paramSet in paramValues)
                {
                    IDbDataParameter param = command.CreateParameter();
                    param.ParameterName = ToParamName(paramSet.Key);
                    param.Value = paramSet.Value ?? DBNull.Value;
                    command.Parameters.Add(param);
                }
        }

        /// <summary>
        /// ����SQL������������IDbCommand
        /// </summary>
        /// <param name="SQLWithParam">��������SQL���
        /// <example>"select @AllFields@ from @FromTable@ where @Condition@"��ʾ�ӱ��в�ѯ���з��������ļ�¼</example>
        /// <example>"select count(*) from @FromTable@ "��ʾ�ӱ������м�¼��������condition������Ϊ��</example>
        /// <example>"delete from @Table@ where @Condition@"��ʾ�ӱ���ɾ�����з��������ļ�¼</example>
        /// </param>        
        /// <param name="condition">������Ϊnullʱ��ʾ������</param>
        /// <returns>IDbCommand</returns>
        public IDbCommand MakeConditionCommand(string SQLWithParam, Condition condition)
        {
            List<object> paramList = new List<object>();
            string strCondition = SqlBuilder.BuildConditionSql(CurrentContext, condition, paramList);
            if (String.IsNullOrEmpty(strCondition)) strCondition = " 1 = 1 ";
            return MakeParamCommand(ReplaceParam(SQLWithParam.Replace(ParamCondition, strCondition)), paramList);
        }

        /// <summary>
        /// �滻Sql�еı��Ϊʵ��Sql
        /// </summary>
        /// <param name="SQLWithParam">������ǵ�Sql���</param>
        /// <returns></returns>
        protected virtual string ReplaceParam(string SQLWithParam)
        {
            return SQLWithParam.Replace(ParamTable, TableName).Replace(ParamFromTable, From);
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
            foreach (ColumnDefinition key in TableDefinition.Keys)
            {
                if (strConditions.Length != 0) strConditions.Append(" and ");
                strConditions.AppendFormat("{0}.{1} = {2}", ToSqlName(TableName), ToSqlName(key.Name), ToSqlParam(key.PropertyName));
                if (!command.Parameters.Contains(key.PropertyName))
                {
                    IDbDataParameter param = command.CreateParameter();
                    param.Size = key.Length;
                    param.DbType = key.DbType;
                    param.ParameterName = ToParamName(key.PropertyName);
                    command.Parameters.Add(param);
                }
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
            foreach (ColumnDefinition key in TableDefinition.Keys)
            {
                values.Add(key.GetValue(o));
            }
            return values.ToArray();
        }

        /// <summary>
        /// �����ݿ�ȡ�õ�ֵת��Ϊ����������������Ӧ��ֵ
        /// </summary>
        /// <param name="dbValue">���ݿ�ȡ�õ�ֵ</param>
        /// <param name="objectType">�������Ե�����</param>
        /// <returns>����������������Ӧ��ֵ</returns>
        protected virtual object ConvertValue(object dbValue, Type objectType)
        {
            if (dbValue == null || dbValue == DBNull.Value)
                return null;

            objectType = Nullable.GetUnderlyingType(objectType) ?? objectType;

            if (objectType.IsInstanceOfType(dbValue))
                return dbValue;

            if (objectType.IsEnum && dbValue.GetType().IsPrimitive) return Enum.ToObject(objectType, dbValue);

            return Convert.ChangeType(dbValue, objectType);
        }

        /// <summary>
        /// �����������ֵת��Ϊ���ݿ��е�ֵ
        /// </summary>
        /// <param name="value">ֵ</param>
        /// <param name="column">�ж���</param>
        /// <returns>���ݿ��е�ֵ</returns>
        protected virtual object ConvertToDBValue(object value, ColumnDefinition column)//TODO:
        {
            if (value == null) return DBNull.Value;
            return value;
        }

        /// <summary>
        /// ����Ƿ���������������������׳��쳣
        /// </summary>
        protected void ThrowExceptionIfNoKeys()
        {
            if (TableDefinition.Keys.Count == 0)
            {
                throw new Exception(String.Format("No key definition found in type \"{0}\", please set the value of property \"IsPrimaryKey\" of key column to true.", Table.DefinitionType.FullName));
            }
        }

        /// <summary>
        /// ����Ƿ���������Ƿ�ƥ��
        /// </summary>
        protected void ThrowExceptionIfTypeNotMatch(Type type)
        {
            if (!ObjectType.IsAssignableFrom(type))
            {
                throw new Exception(String.Format("Type {0} not match object type {2}.", type.FullName, ObjectType.FullName));
            }
        }

        /// <summary>
        /// ���������Ŀ�Ƿ���ȷ�������׳��쳣
        /// </summary>
        /// <param name="keys">����</param>
        protected void ThrowExceptionIfWrongKeys(params object[] keys)
        {
            if (keys.Length != TableDefinition.Keys.Count)
            {
                if (ExceptionWrongKeys == null)
                {
                    List<string> strKeys = new List<string>();
                    foreach (ColumnDefinition key in TableDefinition.Keys) strKeys.Add(key.Name);
                    ExceptionWrongKeys = new ArgumentOutOfRangeException("keys", String.Format("Wrong keys' number. Type \"{0}\" has {1} key(s):'{2}'.", Table.DefinitionType.FullName, strKeys.Count, String.Join("','", strKeys.ToArray())));
                }
                throw ExceptionWrongKeys;
            }
        }

        /// <summary>
        /// ����ת��Ϊ���ݿ�Ϸ�����
        /// </summary>
        /// <param name="name">�ַ�������</param>
        /// <returns>���ݿ�Ϸ�����</returns>
        protected string ToSqlName(string name)
        {
            return SqlBuilder.ToSqlName(name);
        }

        /// <summary>
        /// ԭʼ����ת��Ϊ���ݿ����
        /// </summary>
        /// <param name="nativeName">ԭʼ����</param>
        /// <returns>���ݿ����</returns>
        protected string ToSqlParam(string nativeName)
        {
            return SqlBuilder.ToSqlParam(nativeName);
        }

        /// <summary>
        /// ԭʼ����ת��Ϊ��������
        /// </summary>
        /// <param name="nativeName">ԭʼ����</param>
        /// <returns>��������</returns>
        protected string ToParamName(string nativeName)
        {
            return SqlBuilder.ToParamName(nativeName);
        }

        /// <summary>
        /// ��������ת��Ϊԭʼ����
        /// </summary>
        /// <param name="paramName">��������</param>
        /// <returns>ԭʼ����</returns>
        protected string ToNativeName(string paramName)
        {
            return SqlBuilder.ToNativeName(paramName);
        }
        #endregion
    }
}