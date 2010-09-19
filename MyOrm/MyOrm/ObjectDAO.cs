using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MyOrm.Common;
using System.Data.SqlClient;

namespace MyOrm
{
    /// <summary>
    /// ʵ�������ɾ�Ĳ���
    /// </summary>
    /// <typeparam name="T">ʵ������</typeparam>
    public class ObjectDAO<T> : ObjectDAOBase, IObjectDAO<T>, IObjectDAO
    {
        #region ˽�б���
        private IDbCommand insertCommand;
        private IDbCommand updateCommand;
        private IDbCommand deleteCommand;
        private IDbCommand updateOrInsertCommand;
        #endregion

        public override Type ObjectType
        {
            get { return typeof(T); }
        }

        protected override Table Table
        {
            get { return Provider.GetTableDefinition(typeof(T)); }
        }

        protected ColumnDefinition IdentityColumn
        {
            get
            {
                foreach (ColumnDefinition column in TableDefinition.Columns) if (column.IsIdentity) return column;
                return null;
            }
        }

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
            foreach (ColumnDefinition column in TableDefinition.Columns)
            {
                if (!column.IsIdentity && (column.Mode & ColumnMode.Insert) != ColumnMode.None)
                {
                    if (strColumns.Length != 0) strColumns.Append(",");
                    if (strValues.Length != 0) strValues.Append(",");

                    strColumns.Append(ToSqlName(column.Name));
                    strValues.Append(ToSqlParam(column.PropertyName));
                    IDbDataParameter param = command.CreateParameter();
                    param.Size = column.Length;
                    param.DbType = column.DbType;
                    param.ParameterName = ToParamName(column.PropertyName);
                    command.Parameters.Add(param);
                }
            }
            command.CommandText = String.Format("insert into {0} ({1}) values ({2}); {3}", ToSqlName(TableName), strColumns, strValues, IdentityColumn == null ? null : "select @@IDENTITY as [ID];");
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
            foreach (ColumnDefinition column in TableDefinition.Columns)
            {
                if ((column.Mode & ColumnMode.Update) != ColumnMode.None && !column.IsPrimaryKey)
                {
                    if (strColumns.Length != 0) strColumns.Append(",");
                    strColumns.AppendFormat("{0} = {1}", ToSqlName(column.Name), ToSqlParam(column.PropertyName));
                    IDbDataParameter param = command.CreateParameter();
                    param.Size = column.Length;
                    param.DbType = column.DbType;
                    param.ParameterName = ToParamName(column.PropertyName);
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

        /// <summary>
        /// ʵ�ָ��»���Ӳ�����IDbCommand
        /// </summary>
        protected IDbCommand UpdateOrInsertCommand
        {
            get
            {
                if (updateOrInsertCommand == null) updateOrInsertCommand = MakeUpdateOrInsertCommand();
                return updateOrInsertCommand;
            }
        }

        private IDbCommand MakeUpdateOrInsertCommand()
        {
            IDbCommand command = NewCommand();
            StringBuilder strColumns = new StringBuilder();
            StringBuilder strValues = new StringBuilder();
            StringBuilder strUpdateColumns = new StringBuilder();
            foreach (ColumnDefinition column in TableDefinition.Columns)
            {
                bool columnAdded = false;
                if (!column.IsIdentity && (column.Mode & ColumnMode.Insert) != ColumnMode.None)
                {
                    if (strColumns.Length != 0) strColumns.Append(",");
                    if (strValues.Length != 0) strValues.Append(",");

                    strColumns.Append(ToSqlName(column.Name));
                    strValues.Append(ToSqlParam(column.PropertyName));
                    columnAdded = true;
                }

                if ((column.Mode & ColumnMode.Update) != ColumnMode.None && !column.IsPrimaryKey)
                {
                    if (strUpdateColumns.Length != 0) strUpdateColumns.Append(",");
                    strUpdateColumns.AppendFormat("{0} = {1}", ToSqlName(column.Name), ToSqlParam(column.PropertyName));
                    columnAdded = true;
                }

                if (columnAdded)
                {
                    IDbDataParameter param = command.CreateParameter();
                    param.DbType = column.DbType;
                    param.Size = column.Length;
                    param.ParameterName = ToParamName(column.PropertyName);
                    command.Parameters.Add(param);
                }
            }
            string insertCommandText = String.Format("insert into {0} ({1}) values ({2}); {3}", ToSqlName(TableName), strColumns, strValues, IdentityColumn == null ? null : "select @@IDENTITY as [ID];");
            string updateCommandText = String.Format("update {0} set {1} where {2};", ToSqlName(TableName), strUpdateColumns, MakeIsKeyCondition(command));

            command.CommandText = String.Format("if exists(select 1 from {0} where {1}) begin {2} select -1; end else begin {3} end", ToSqlName(TableName), MakeIsKeyCondition(command), updateCommandText, insertCommandText);
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
            if (t == null) throw new ArgumentNullException("t");
            foreach (IDataParameter param in InsertCommand.Parameters)
            {
                ColumnDefinition column = TableDefinition.GetColumn(ToNativeName(param.ParameterName));
                param.Value = ConvertToDBValue(column.GetValue(t), column);
            }
            if (IdentityColumn == null)
                return InsertCommand.ExecuteNonQuery() > 0;
            else
            {
                IdentityColumn.SetValue(t, Convert.ChangeType(InsertCommand.ExecuteScalar(), IdentityColumn.PropertyType));
                return true;
            }
        }

        /// <summary>
        /// ��������µ����ݿ�
        /// </summary>
        /// <param name="t">�����µĶ���</param>
        /// <returns>�Ƿ�ɹ�����</returns>
        public virtual bool Update(T t)
        {
            if (t == null) throw new ArgumentNullException("t");
            foreach (IDataParameter param in UpdateCommand.Parameters)
            {
                ColumnDefinition column = TableDefinition.GetColumn(ToNativeName(param.ParameterName));
                param.Value = ConvertToDBValue(column.GetValue(t), column);
            }
            return UpdateCommand.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// ��������µ����ݿ⣬������ݿ��ͻ
        /// </summary>
        /// <param name="current">�����µĶ���</param>
        /// <param name="original">ԭʼ�Ķ���</param>
        /// <returns>�Ƿ�ɹ�����</returns>
        public bool Update(T current, T original)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// ���»���Ӷ�������������£��������������
        /// </summary>
        /// <param name="t">�����»���ӵĶ���</param>
        /// <returns>ָʾ���»������</returns>
        public UpdateOrInsertResult UpdateOrInsert(T t)
        {
            if (t == null) throw new ArgumentNullException("t");
            foreach (IDataParameter param in UpdateOrInsertCommand.Parameters)
            {
                ColumnDefinition column = TableDefinition.GetColumn(ToNativeName(param.ParameterName));
                param.Value = ConvertToDBValue(column.GetValue(t), column);
            }
            int ret = Convert.ToInt32(UpdateOrInsertCommand.ExecuteScalar());
            if (ret >= 0)
            {
                if (IdentityColumn != null) IdentityColumn.SetValue(t, ret);
                return UpdateOrInsertResult.Inserted;
            }
            else
            {
                return UpdateOrInsertResult.Updated;
            }
        }

        /// <summary>
        /// ����������ݿ�ɾ��
        /// </summary>
        /// <param name="t">��ɾ���Ķ���</param>
        /// <returns>�Ƿ�ɹ�ɾ��</returns>
        public virtual bool Delete(T t)
        {
            if (t == null) throw new ArgumentNullException("t");
            return DeleteByKeys(GetKeyValues(t));
        }

        /// <summary>
        /// ��������ɾ������
        /// </summary>
        /// <param name="condition">����</param>
        /// <returns>ɾ����������</returns>
        public virtual int Delete(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("delete from @Table" + (condition == null ? null : " where @Condition"), condition))
            {
                return command.ExecuteNonQuery();
            }
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
                param.Value = ConvertToDBValue(keys[i], TableDefinition.Keys[i]);
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

        bool IObjectDAO.Update(object current, object original)
        {
            return Update((T)current, (T)original);
        }

        UpdateOrInsertResult IObjectDAO.UpdateOrInsert(object o)
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
