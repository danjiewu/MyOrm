using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using MyOrm.Common;
using System.Data.Common;

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
        #endregion

        #region ˽�б���
        private string tableName = null;
        private Exception ExceptionWrongKeys = null;
        private Exception ExceptionNoKeys = null;
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
        protected virtual SqlBuilder SqlBuilder
        {
            get { return null; }
        }

        protected SqlBuildContext CurrentContext
        {
            get { return new SqlBuildContext() { Table = Table }; }
        }

        /// <summary>
        /// ����Ϣ�ṩ��
        /// </summary>
        protected TableInfoProvider Provider
        {
            get { return Configuration.DefaultProvider; }
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
        #endregion

        #region ����
        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public IDbConnection Connection
        {
            get;
            set;
        }

        /// <summary>
        /// ����IDbCommand
        /// </summary>
        /// <returns></returns>
        public virtual IDbCommand NewCommand()
        {
            return Connection.CreateCommand();
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
            IDbCommand command = NewCommand();
            if (paramValues != null)
                foreach (object paramValue in paramValues)
                {
                    IDbDataParameter param = command.CreateParameter();
                    param.ParameterName = ToParamName(Convert.ToString(paramIndex++));
                    param.Value = paramValue ?? DBNull.Value;
                    command.Parameters.Add(param);
                }
            command.CommandText = SQL;
            return command;
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
            return SQLWithParam.Replace(ParamTable, TableName);
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
        /// <param name="column">������</param>
        /// <returns>����������������Ӧ��ֵ</returns>
        protected virtual object ConvertValue(object dbValue, Type objectType)
        {
            if (dbValue == null || dbValue == DBNull.Value)
                return null;

            objectType = Nullable.GetUnderlyingType(objectType) ?? objectType;

            if (objectType.IsInstanceOfType(dbValue))
                return dbValue;

            if (objectType.IsEnum && dbValue.GetType().IsPrimitive) return dbValue;

            return Convert.ChangeType(dbValue, objectType);
        }

        /// <summary>
        /// �����������ֵת��Ϊ���ݿ��е�ֵ
        /// </summary>
        /// <param name="value">ֵ</param>
        /// <param name="column">������</param>
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
            if (keys.Length != TableDefinition.Keys.Count)
            {
                if (ExceptionWrongKeys == null)
                {
                    List<string> strKeys = new List<string>();
                    foreach (ColumnDefinition key in TableDefinition.Keys) strKeys.Add(key.Name);
                    ExceptionWrongKeys = new ArgumentOutOfRangeException("keys", String.Format("Wrong keys' number. Type \"{0}\" has {1} key(s):'{2}'.", Table.ObjectType.FullName, strKeys.Count, String.Join("','", strKeys.ToArray())));
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
