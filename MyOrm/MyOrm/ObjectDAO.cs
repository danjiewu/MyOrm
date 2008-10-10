using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MyOrm.Metadata;

namespace MyOrm
{
    /// <summary>
    /// ʵ�������ɾ�Ĳ���
    /// </summary>
    /// <typeparam name="T">ʵ������</typeparam>
    public class ObjectDAO<T> : ObjectViewDAO<T>, IObjectDAO<T>, IObjectDAO where T : new()
    {
        #region ˽�б���
        private IDbCommand insertCommand;
        private IDbCommand updateCommand;
        private IDbCommand deleteCommand;
        #endregion

        #region Ԥ����Command
        /// <summary>
        /// ʵ�ֲ��������IDbCommand
        /// </summary>
        protected IDbCommand InsertCommand
        {
            get
            {
                if (insertCommand == null)
                    insertCommand = MakeInsertCommand();
                return insertCommand;
            }
        }

        private IDbCommand MakeInsertCommand()
        {
            IDbCommand command = NewCommand();
            StringBuilder strColumns = new StringBuilder();
            StringBuilder strValues = new StringBuilder();
            foreach (ColumnInfo column in Table.Columns)
            {
                if ((column.Mode & ColumnMode.Insert) != ColumnMode.Ignore)
                {
                    if (strColumns.Length != 0) strColumns.Append(",");
                    if (strValues.Length != 0) strValues.Append(",");

                    strColumns.Append(ToSqlName(column.ColumnName));
                    strValues.Append(ToSqlParam(column.ColumnName));
                    IDataParameter param = command.CreateParameter();
                    param.DbType = column.DbType;
                    param.ParameterName = column.ColumnName;
                    command.Parameters.Add(param);
                }
            }
            command.CommandText = String.Format("insert into {0} ({1}) values ({2});", ToSqlName(TableName), strColumns, strValues);
            return command;
        }

        /// <summary>
        /// ʵ�ָ��²�����IDbCommand
        /// </summary>
        protected IDbCommand UpdateCommand
        {
            get
            {
                if (updateCommand == null)
                    updateCommand = MakeUpdateCommand();
                return updateCommand;
            }
        }

        private IDbCommand MakeUpdateCommand()
        {
            IDbCommand command = NewCommand();
            StringBuilder strColumns = new StringBuilder();
            foreach (ColumnInfo column in Table.Columns)
            {
                if ((column.Mode & ColumnMode.Update) != ColumnMode.Ignore && !column.IsPrimaryKey)
                {
                    if (strColumns.Length != 0) strColumns.Append(",");
                    strColumns.AppendFormat("{0} = {1}", ToSqlName(column.ColumnName), ToSqlParam(column.ColumnName));
                    IDataParameter param = command.CreateParameter();
                    param.DbType = column.DbType;
                    param.ParameterName = column.ColumnName;
                    command.Parameters.Add(param);
                }
            }
            command.CommandText = String.Format("update {0} set {1} where {2}", ToSqlName(TableName), strColumns, MakeIsKeyCondition(command));
            return command;
        }

        /// <summary>
        /// ʵ��ɾ��������IDbCommand
        /// </summary>
        protected IDbCommand DeleteCommand
        {
            get
            {
                if (deleteCommand == null)
                    deleteCommand = MakeDeleteCommand();
                return deleteCommand;
            }
        }

        private IDbCommand MakeDeleteCommand()
        {
            IDbCommand command = NewCommand();
            command.CommandText = String.Format("delete from {0} where {1}", ToSqlName(TableName), MakeIsKeyCondition(command));
            return command;
        }
        #endregion


        #region ����
        /// <summary>
        /// ��������ӵ����ݿ�
        /// </summary>
        /// <param name="t">����ӵĶ���</param>
        /// <returns>�Ƿ�ɹ����</returns>
        public virtual bool Insert(T t)
        {
            foreach (IDataParameter param in InsertCommand.Parameters)
            {
                ColumnInfo column = Table.GetColumn(param.ParameterName);
                param.Value = ConvertToDBValue(column.GetValue(t), column);
            }
            return InsertCommand.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// ��������µ����ݿ�
        /// </summary>
        /// <param name="t">�����µĶ���</param>
        /// <returns>�Ƿ�ɹ�����</returns>
        public virtual bool Update(T t)
        {
            foreach (IDataParameter param in UpdateCommand.Parameters)
            {
                ColumnInfo column = Table.GetColumn(param.ParameterName);
                param.Value = ConvertToDBValue(column.GetValue(t), column);
            }
            return UpdateCommand.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// ���»���Ӷ�������������£��������������
        /// </summary>
        /// <param name="o">�����»���ӵĶ���</param>
        /// <returns>�Ƿ�ɹ����»����</returns>
        public bool UpdateOrInsert(T o)
        {
            return Update(o) ? true : Insert(o);
        }

        /// <summary>
        /// ����������ݿ�ɾ��
        /// </summary>
        /// <param name="t">��ɾ���Ķ���</param>
        /// <returns>�Ƿ�ɹ�ɾ��</returns>
        public virtual bool Delete(T t)
        {
            return DeleteByKeys(GetKeyValues(t));
        }

        /// <summary>
        /// ��ָ�������Ķ�������ݿ�ɾ��
        /// </summary>
        /// <param name="keys">��ɾ���Ķ��������</param>
        /// <returns>�Ƿ�ɹ�ɾ��</returns>
        public virtual bool DeleteByKeys(params object[] keys)
        {
            ThrowExceptionIfWrongKeys(keys);
            int i = 0;
            foreach (IDataParameter param in DeleteCommand.Parameters)
            {
                param.Value = ConvertToDBValue(keys[i], Table.Keys[i]);
                i++;
            }
            return DeleteCommand.ExecuteNonQuery() > 0;
        }
        #endregion

        #region IObjectDAO Members

        bool IObjectDAO.Insert(object o)
        {
            return Insert((T)o);
        }

        bool IObjectDAO.Update(object o)
        {
            return Update((T)o);
        }

        bool IObjectDAO.UpdateOrInsert(object o)
        {
            return UpdateOrInsert((T)o);
        }

        bool IObjectDAO.Delete(object o)
        {
            return Delete((T)o);
        }

        #endregion
    }
}
