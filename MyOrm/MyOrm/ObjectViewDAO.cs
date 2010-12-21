using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using MyOrm.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.Common;

namespace MyOrm
{
    /// <summary>
    /// ʵ����Ĳ�ѯ����
    /// </summary>
    /// <typeparam name="T">ʵ������</typeparam>
    public class ObjectViewDAO<T> : ObjectDAOBase, IObjectViewDAO<T>, IObjectViewDAO where T : new()
    {
        #region Ԥ�������
        /// <summary>
        /// ��ʾSQL��ѯ�������ֶεı��
        /// </summary>
        public const string ParamAllFields = "@AllFields@";
        /// <summary>
        /// ��ʾSQL��ѯ�ж�����ӵı��
        /// </summary>
        public const string ParamFromTable = "@FromTable@";
        #endregion

        #region ˽�б���
        private string fromTable = null;
        private string allFieldsSql = null;

        private ReadOnlyCollection<Column> selectColumns;

        private IDbCommand getObjectCommand = null;
        private IDbCommand objectExistsCommand = null;
        #endregion

        #region ����
        /// <summary>
        /// ʵ���������
        /// </summary>
        public override Type ObjectType
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// ��ѯ������
        /// </summary>
        protected override Table Table
        {
            get { return Provider.GetTableView(ObjectType); }
        }

        /// <summary>
        /// ��ѯʱʹ�õ�������Ķ����
        /// </summary>
        protected string FromTable
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
        protected ReadOnlyCollection<Column> SelectColumns
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
                    StringBuilder strAllFields = new StringBuilder();
                    foreach (Column column in SelectColumns)
                    {
                        if (strAllFields.Length != 0) strAllFields.Append(",");
                        strAllFields.Append(column.FormattedExpression + " as " + column.PropertyName);
                    }
                    allFieldsSql = strAllFields.ToString();
                }
                return allFieldsSql;
            }
        }

        #endregion

        #region Ԥ����Command
        /// <summary>
        /// ʵ�ּ������Ƿ���ڲ�����IDbCommand
        /// </summary>
        protected IDbCommand ObjectExistsCommand
        {
            get
            {
                if (objectExistsCommand == null)
                    objectExistsCommand = MakeObjectExistsCommand();
                return objectExistsCommand;
            }
        }

        private IDbCommand MakeObjectExistsCommand()
        {
            ThrowExceptionIfNoKeys();
            IDbCommand command = NewCommand();
            StringBuilder strConditions = new StringBuilder();
            foreach (ColumnDefinition key in TableDefinition.Keys)
            {
                if (strConditions.Length != 0) strConditions.Append(" and ");
                strConditions.AppendFormat("{0} = {1}", ToSqlName(key.Name), ToSqlParam(key.PropertyName));
                if (!command.Parameters.Contains(key.PropertyName))
                {
                    IDbDataParameter param = command.CreateParameter();
                    param.Size = key.Length;
                    param.DbType = key.DbType;
                    param.ParameterName = ToParamName(key.PropertyName);
                    command.Parameters.Add(param);
                }
            }
            command.CommandText = String.Format("select count(1) from {0} where {1}", ToSqlName(Table.Definition.Name), strConditions);
            command.Prepare();
            return command;
        }

        /// <summary>
        /// ʵ�ֻ�ȡ���������IDbCommand
        /// </summary>
        protected IDbCommand GetObjectCommand
        {
            get
            {
                if (getObjectCommand == null)
                    getObjectCommand = MakeGetObjectCommand();
                return getObjectCommand;
            }
        }

        private IDbCommand MakeGetObjectCommand()
        {
            IDbCommand command = NewCommand();
            command.CommandText = String.Format("select {0} from {1} where {2}", AllFieldsSql, FromTable, MakeIsKeyCondition(command));
            command.Prepare();
            return command;
        }
        #endregion

        #region ����
        /// <summary>
        /// ����������ȡ����
        /// </summary>
        /// <param name="keys">�����������������������˳������</param>
        /// <returns>�������������򷵻�null</returns>
        public virtual T GetObject(params object[] keys)
        {
            ThrowExceptionIfWrongKeys(keys);
            int i = 0;
            foreach (IDataParameter param in GetObjectCommand.Parameters)
            {
                param.Value = ConvertToDBValue(keys[i], Table.Definition.Keys[i]);
                i++;
            }
            using (IDataReader reader = GetObjectCommand.ExecuteReader())
            {
                return ReadOne(reader);
            }
        }

        /// <summary>
        /// �ж�������Ӧ�Ķ����Ƿ����
        /// </summary>
        /// <param name="keys">���������������������˳������</param>
        /// <returns>�Ƿ����</returns>
        public virtual bool Exists(params object[] keys)
        {
            ThrowExceptionIfWrongKeys(keys);
            int i = 0;
            foreach (IDataParameter param in ObjectExistsCommand.Parameters)
            {
                param.Value = ConvertToDBValue(keys[i], Table.Definition.Keys[i]);
                i++;
            }
            return Convert.ToInt32(ObjectExistsCommand.ExecuteScalar()) > 0;
        }

        /// <summary>
        /// ��ȡ���������Ķ������
        /// </summary>
        /// <param name="condition">��������ֵ���б���Ϊnull���ʾû������</param>
        /// <returns>���������Ķ������</returns>
        public virtual int Count(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("select count(*) from @FromTable@ where @Condition@", condition))
            {
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        /// <summary>
        /// �жϷ��������Ķ����Ƿ����
        /// </summary>
        /// <param name="condition">��������ֵ���б���Ϊnull���ʾû������</param>
        /// <returns>�Ƿ����</returns>
        public virtual bool Exists(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("select 1 from @FromTable@ where @Condition@", condition))
            {
                return command.ExecuteScalar() != null;
            }
        }

        /// <summary>
        /// ���ݵ���������ѯ
        /// </summary>
        /// <param name="name">������</param>
        /// <param name="value">ֵ</param>
        /// <returns>���������Ķ����б�</returns>
        public List<T> Search(string name, object value)
        {
            return Search(new SimpleCondition(name, value));
        }

        /// <summary>
        /// ���ݵ���������ѯ
        /// </summary>
        /// <param name="name">������</param>
        /// <param name="op">�����жϲ�����</param>
        /// <param name="value">ֵ</param>
        /// <returns>���������Ķ����б�</returns>
        public List<T> Search(string name, ConditionOperator op, object value)
        {
            return Search(new SimpleCondition(name, op, value));
        }

        /// <summary>
        /// ����������ѯ������������߼�������
        /// </summary>
        /// <param name="condition">��������ֵ���б���Ϊnull���ʾû������</param>
        /// <returns>���������Ķ����б�</returns>
        public virtual List<T> Search(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("select @AllFields@ from @FromTable@" + (condition == null ? null : " where @Condition@"), condition))
            {
                using (IDataReader reader = command.ExecuteReader())
                {
                    return ReadAll(reader);
                }
            }
        }

        /// <summary>
        /// ��ȡ�������������Ķ���
        /// </summary>
        /// <param name="condition">��������ֵ���б���Ϊnull���ʾû������</param>
        /// <returns>��һ�����������Ķ������������򷵻�null</returns>
        public virtual T SearchOne(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("select @AllFields@ from @FromTable@ where @Condition@", condition))
            {
                using (IDataReader reader = command.ExecuteReader())
                {
                    return ReadOne(reader);
                }
            }
        }

        /// <summary>
        /// ��Ĭ�������ҳ��ѯ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="startIndex">��ʼλ��</param>
        /// <param name="sectionSize">����¼��</param>
        /// <returns>���������ķ�ҳ�����б�</returns>
        public virtual List<T> SearchSection(Condition condition, int startIndex, int sectionSize)
        {
            return SearchSection(condition, startIndex, sectionSize, null);
        }

        /// <summary>
        /// ��������з�ҳ��ѯ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="startIndex">��ʼλ��</param>
        /// <param name="sectionSize">����¼��</param>
        /// <param name="orderby">�����ֶ�</param>
        /// <returns>���������ķ�ҳ�����б�</returns>
        public virtual List<T> SearchSection(Condition condition, int startIndex, int sectionSize, string orderby)
        {
            return SearchSection(condition, startIndex, sectionSize, orderby, ListSortDirection.Ascending);
        }

        /// <summary>
        /// ��ҳ��ѯ
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="startIndex">��ʼλ��</param>
        /// <param name="sectionSize">����¼��</param>
        /// <param name="orderby">�����ֶ�</param>
        /// <param name="direction">����˳��</param>
        /// <returns>���������ķ�ҳ�����б�</returns>
        public virtual List<T> SearchSection(Condition condition, int startIndex, int sectionSize, string orderby, ListSortDirection direction)
        {
            if (string.IsNullOrEmpty(orderby))
            {
                if (Table.Definition.Keys.Count != 0)
                {
                    StringBuilder strKeys = new StringBuilder();
                    foreach (ColumnDefinition key in Table.Definition.Keys)
                    {
                        if (strKeys.Length != 0) strKeys.Append(",");
                        strKeys.Append(String.Format("{0}.{1}", Table.FormattedName, key.FormattedName));
                    }
                    orderby = strKeys.ToString();
                }
                else
                {
                    //TODO: Add one column or all columns?
                    throw new Exception("No columns or keys to sort by.");
                }
            }
            else
            {
                Column column = Table.GetColumn(orderby);
                if (column != null)
                    orderby = column.FormattedExpression;
                else
                {
                    //TODO: The orderby is not a safe sql string. Throw exception or not?
                }
            }
            string paramedSQL = SqlBuilder.GetSelectSectionSql(AllFieldsSql, FromTable, ParamCondition, orderby + (direction == ListSortDirection.Ascending ? " asc" : " desc"), startIndex, sectionSize);
            using (IDbCommand command = MakeConditionCommand(paramedSQL, condition))
            {
                using (IDataReader reader = command.ExecuteReader())
                {
                    return ReadAll(reader);
                }
            }
        }
        #endregion

        #region IObjectViewDAO Members

        object IObjectViewDAO.GetObject(params object[] keys)
        {
            return GetObject(keys);
        }

        object IObjectViewDAO.SearchOne(Condition condition)
        {
            return SearchOne(condition);
        }

        IList IObjectViewDAO.Search(Condition condition)
        {
            return Search(condition);
        }

        IList IObjectViewDAO.SearchSection(Condition condition, int startIndex, int sectionSize, string orderby, ListSortDirection direction)
        {
            return SearchSection(condition, startIndex, sectionSize, orderby, direction);
        }

        #endregion

        #region ���÷���

        /// <summary>
        /// �滻Sql�еı��Ϊʵ��Sql
        /// </summary>
        /// <param name="SQLWithParam">������ǵ�Sql��䣬��ǿ���ΪParamAllFields��ParamFromTable</param>
        /// <returns></returns>
        protected virtual string ReplaceParam(string SQLWithParam)
        {
            return base.ReplaceParam(SQLWithParam).Replace(ParamAllFields, AllFieldsSql).Replace(ParamFromTable, FromTable);
        }

        /// <summary>
        /// ��ȡ���м�¼��ת��Ϊ���󼯺ϣ���ѯ�õ�AllFieldsSQLʱ����
        /// </summary>
        /// <param name="reader">ֻ�������</param>
        /// <returns>�����б�</returns>
        protected List<T> ReadAll(IDataReader reader)
        {
            List<T> results = new List<T>();
            while (reader.Read())
            {
                results.Add(Read(reader));
            }
            return results;
        }

        /// <summary>
        /// ��IDataReader�ж�ȡһ����¼ת��Ϊ�������޼�¼�򷵻�null
        /// </summary>
        /// <param name="dataReader">IDataReader</param>
        /// <returns>�������޼�¼�򷵻�null</returns>
        protected T ReadOne(IDataReader dataReader)
        {
            using (IDataReader reader = dataReader)
            {
                return reader.Read() ? Read(reader) : default(T);
            }
        }

        /// <summary>
        /// ��һ�м�¼ת��Ϊ����
        /// </summary>
        /// <param name="record">һ�м�¼</param>
        /// <returns>����</returns>
        protected virtual T Read(IDataRecord record)
        {
            T t = new T();
            int i = 0;
            foreach (Column column in SelectColumns)
            {
                column.SetValue(t, record.IsDBNull(i) ? null : ConvertValue(record[i], column.PropertyType));
                i++;
            }
            return t;
        }
        #endregion
    }
}
