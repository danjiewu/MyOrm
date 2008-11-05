using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using MyOrm.Metadata;
using MyOrm.Common;
using System.ComponentModel;

namespace MyOrm
{
    /// <summary>
    /// ʵ����Ĳ�ѯ����
    /// </summary>
    /// <typeparam name="T">ʵ������</typeparam>
    public class ObjectViewDAO<T> : ObjectDAOBase, IObjectViewDAO<T>, IObjectViewDAO where T : new()
    {
        #region ˽�б���
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
            IDbCommand command = NewCommand();
            command.CommandText = String.Format("select 1 from {0} where {1}", ToSqlName(TableName), MakeIsKeyCondition(command));
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
                param.Value = ConvertToDBValue(keys[i], Table.Keys[i]);
                i++;
            }
            return ReadOne(GetObjectCommand.ExecuteReader());
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
                param.Value = ConvertToDBValue(keys[i], Table.Keys[i]);
                i++;
            }
            return (int)ObjectExistsCommand.ExecuteScalar() > 0;
        }

        /// <summary>
        /// ��ȡ���������Ķ������
        /// </summary>
        /// <param name="condition">��������ֵ���б���Ϊnull���ʾû������</param>
        /// <returns>���������Ķ������</returns>
        public virtual int Count(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("select count(*) from @FromTable where @Condition", condition))
            {
                return (int)command.ExecuteScalar();
            }
        }

        /// <summary>
        /// �жϷ��������Ķ����Ƿ����
        /// </summary>
        /// <param name="condition">��������ֵ���б���Ϊnull���ʾû������</param>
        /// <returns>�Ƿ����</returns>
        public virtual bool Exists(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("select 1 from @FromTable where @Condition", condition))
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
            using (IDbCommand command = MakeConditionCommand("select @AllFields from @FromTable where @Condition", condition))
            {
                return ReadAll(command.ExecuteReader());
            }
        }

        /// <summary>
        /// ��ȡ�������������Ķ���
        /// </summary>
        /// <param name="condition">��������ֵ���б���Ϊnull���ʾû������</param>
        /// <returns>��һ�����������Ķ������������򷵻�null</returns>
        public virtual T SearchOne(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("select @AllFields from @FromTable where @Condition", condition))
            {
                return ReadOne(command.ExecuteReader());
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
                if (Table.Keys.Count != 0)
                {
                    StringBuilder strKeys = new StringBuilder();
                    foreach (ColumnInfo key in Table.Keys)
                    {
                        if (strKeys.Length != 0) strKeys.Append(",");
                        strKeys.Append(GetFullName(key));
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
                ColumnInfo column = Table.GetColumnByProperty(orderby);
                if (column != null)
                    orderby = GetFullName(column);
                else
                {
                    //TODO: The orderby is not a safe sql string. Throw exception or not?
                }
            }
            string paramedSQL = String.Format("select * from (select @AllFields, Row_Number() over (Order by {0} {1}) as Row_Number from @FromTable where @Condition) as TempTable where Row_Number > {2} and Row_Number <= {3}", orderby, direction == ListSortDirection.Ascending ? "asc" : "desc", startIndex, startIndex + sectionSize);
            using (IDbCommand command = MakeConditionCommand(paramedSQL, condition))
            {
                return ReadAll(command.ExecuteReader());
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
            reader.Close();
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
            foreach (ColumnInfo column in SelectColumns)
            {
                column.SetValue(t, ConvertToObjectValue(record[i], column));
                i++;
            }
            return t;
        }
        #endregion
    }
}
