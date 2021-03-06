using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MyOrm.Common;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;

namespace MyOrm
{
    /// <summary>
    /// 实体类的增删改操作
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public abstract class ObjectDAO<T> : ObjectViewDAO<T>, IObjectDAO<T>, IObjectDAO where T : new()
    {
        public ObjectDAO() : base() { }

        public ObjectDAO(IDbConnection connection) : base(connection) { }

        public ObjectDAO(IDbConnection connection, SqlBuilder builder) : base(connection, builder) { }
        #region 私有变量
        private IDbCommand insertCommand;
        private IDbCommand updateCommand;
        private IDbCommand deleteCommand;
        private IDbCommand updateOrInsertCommand;
        #endregion

        /// <summary>
        /// 实体类的类型
        /// </summary>
        public override Type ObjectType
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// 表信息
        /// </summary>
        protected override Table Table
        {
            get { return Provider.GetTableDefinition(typeof(T)); }
        }

        /// <summary>
        /// 标识列
        /// </summary>
        protected ColumnDefinition IdentityColumn
        {
            get
            {
                foreach (ColumnDefinition column in TableDefinition.Columns) if (column.IsIdentity) return column;
                return null;
            }
        }

        #region 预定义Command
        /// <summary>
        /// 实现插入操作的IDbCommand
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

            command.CommandText = IdentityColumn == null ?
                String.Format("insert into {0} ({1}) values ({2})", ToSqlName(TableName), strColumns, strValues)
                : SqlBuilder.BuildIdentityInsertSQL(command, IdentityColumn, TableName, strColumns.ToString(), strValues.ToString());
            return command;
        }

        /// <summary>
        /// 实现更新操作的IDbCommand
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
        /// 实现删除操作的IDbCommand
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
        /// 实现更新或添加操作的IDbCommand
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
            string insertCommandText = IdentityColumn == null ? String.Format("insert into {0} ({1}) values ({2})", ToSqlName(TableName), strColumns, strValues)
                : SqlBuilder.BuildIdentityInsertSQL(command, IdentityColumn, ToSqlName(TableName), strColumns.ToString(), strValues.ToString());
            string updateCommandText = String.Format("update {0} set {1} where {2};", ToSqlName(TableName), strUpdateColumns, MakeIsKeyCondition(command));

            command.CommandText = String.Format("BEGIN if exists(select 1 from {0} where {1}) begin {2} select -1; end else begin {3} end END;", ToSqlName(TableName), MakeIsKeyCondition(command), updateCommandText, insertCommandText);
            return command;
        }

        #endregion

        #region 方法
        /// <summary>
        /// 将对象添加到数据库
        /// </summary>
        /// <param name="t">待添加的对象</param>
        /// <returns>是否成功添加</returns>
        public virtual bool Insert(T t)
        {
            if (t == null) throw new ArgumentNullException("t");
            foreach (IDataParameter param in InsertCommand.Parameters)
            {
                ColumnDefinition column = TableDefinition.GetColumn(ToNativeName(param.ParameterName));
                param.Value = ConvertToDBValue(column.GetValue(t), column);
            }
            if (IdentityColumn == null)
            {
                InsertCommand.ExecuteNonQuery();
            }
            else
            {
                IDataParameter param = InsertCommand.Parameters.Contains(ToParamName(IdentityColumn.PropertyName)) ? (IDataParameter)InsertCommand.Parameters[ToParamName(IdentityColumn.PropertyName)] : null;
                if (param != null && param.Direction == ParameterDirection.Output)
                {
                    InsertCommand.ExecuteNonQuery();
                    IdentityColumn.SetValue(t, ConvertValue(param.Value, IdentityColumn.PropertyType));
                }
                else
                {
                    IdentityColumn.SetValue(t, ConvertValue(InsertCommand.ExecuteScalar(), IdentityColumn.PropertyType));
                }
            }
            return true;
        }

        /// <summary>
        /// 将对象更新到数据库
        /// </summary>
        /// <param name="t">待更新的对象</param>
        /// <returns>是否成功更新</returns>
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
        /// 更新或添加对象，若存在则更新，若不存在则添加
        /// </summary>
        /// <param name="t">待更新或添加的对象</param>
        /// <returns>指示更新还是添加</returns>
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
        /// 根据条件更新数据
        /// </summary>
        /// <param name="values">需要更新的属性及数值，key为属性名，value为数值</param>
        /// <param name="condition">更新的条件</param>
        /// <returns>更新的记录数</returns>
        public virtual int UpdateValues(IEnumerable<KeyValuePair<string, object>> values, Condition condition)
        {
            List<string> strSets = new List<string>();
            List<object> paramValues = new List<object>();
            foreach (KeyValuePair<string, object> value in values)
            {
                Column column = Table.GetColumn(value.Key);
                if (column == null) throw new Exception(String.Format("Property \"{0}\" does not exist in type \"{1}\".", value.Key, Table.DefinitionType.FullName));
                paramValues.Add(value.Value);
                strSets.Add(column.FormattedName(SqlBuilder) + "=" + ToSqlParam(paramValues.Count.ToString()));
            }
            string updateSql = "udpate " + TableName + " set " + String.Join(",", strSets.ToArray()) + " where " + SqlBuilder.BuildConditionSql(CreateNewContext(), condition, paramValues);
            using (IDbCommand command = MakeParamCommand(updateSql, paramValues))
            {
                using (IDataReader reader = command.ExecuteReader())
                {
                    return command.ExecuteNonQuery();
                }
            }
        }

        /// <summary>
        /// 根据主键更新数据
        /// </summary>
        /// <param name="values">需要更新的属性及数值，key为属性名，value为数值</param>
        /// <param name="condition">更新的条件</param>
        /// <returns>更新是否成功</returns>
        public virtual bool UpdateValues(IEnumerable<KeyValuePair<string, object>> values, params object[] keys)
        {
            ThrowExceptionIfNoKeys();
            ThrowExceptionIfWrongKeys(keys);
            ConditionSet condition = new ConditionSet();
            int i = 0;
            foreach (ColumnDefinition column in TableDefinition.Keys)
            {
                condition.Add(new SimpleCondition(column.PropertyName, keys[i++]));
            }
            return UpdateValues(values, condition) > 0;
        }

        /// <summary>
        /// 将对象从数据库删除
        /// </summary>
        /// <param name="t">待删除的对象</param>
        /// <returns>是否成功删除</returns>
        public virtual bool Delete(T t)
        {
            if (t == null) throw new ArgumentNullException("t");
            return DeleteByKeys(GetKeyValues(t));
        }

        /// <summary>
        /// 根据条件删除对象
        /// </summary>
        /// <param name="condition">条件</param>
        /// <returns>删除对象数量</returns>
        public virtual int Delete(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("delete from @Table@ where @Condition@", condition))
            {
                return command.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// 将指定主键的对象从数据库删除
        /// </summary>
        /// <param name="keys">待删除的对象的主键</param>
        /// <returns>是否成功删除</returns>
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
