using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace MyOrm
{
    /// <summary>
    /// 
    /// </summary>
    public class AutoCommand : IDbCommand
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlBuilder"></param>
        /// <param name="target"></param>
        public AutoCommand(SqlBuilder sqlBuilder, IDbCommand target)
        {
            this.sqlBuilder = sqlBuilder;
            this.target = target;
        }

        private SqlBuilder sqlBuilder;
        /// <summary>
        /// 生成Sql语句的辅助类
        /// </summary>
        public SqlBuilder SqlBuilder { get { return sqlBuilder; } }

        private IDbCommand target;
        /// <summary>
        /// 目标Command
        /// </summary>
        public IDbCommand Target
        {
            get { return target; }
        }

        protected virtual void PreExcuteCommand(ExcuteType excuteType)
        {
            Transaction = TransactionManager.CurrentTransaction(Connection);
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            //Console.WriteLine(CommandText);//TODO: Add log here.
        }

        protected virtual void PostExcuteCommand(ExcuteType excuteType)
        {
        }

        #region IDbCommand Members

        public void Cancel()
        {
            target.Cancel();
        }

        private string commandText;
        public string CommandText
        {
            get { return commandText; }
            set
            {
                commandText = value;
                target.CommandText = SqlBuilder.ReplaceSqlName(value);
            }
        }

        public int CommandTimeout
        {
            get { return target.CommandTimeout; }
            set { target.CommandTimeout = value; }
        }

        public CommandType CommandType
        {
            get { return target.CommandType; }
            set { target.CommandType = value; }
        }

        public IDbConnection Connection
        {
            get { return target.Connection; }
            set { target.Connection = value; }
        }

        public IDbDataParameter CreateParameter()
        {
            return target.CreateParameter();
        }

        public int ExecuteNonQuery()
        {
            PreExcuteCommand(ExcuteType.ExecuteNonQuery);
            int ret = target.ExecuteNonQuery();
            PostExcuteCommand(ExcuteType.ExecuteNonQuery);
            return ret;
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            PreExcuteCommand(ExcuteType.ExecuteReader);
            IDataReader ret = target.ExecuteReader(behavior);
            PostExcuteCommand(ExcuteType.ExecuteReader);
            return ret;
        }

        public IDataReader ExecuteReader()
        {
            PreExcuteCommand(ExcuteType.ExecuteReader);
            IDataReader ret = target.ExecuteReader();
            PostExcuteCommand(ExcuteType.ExecuteReader);
            return ret;
        }

        public object ExecuteScalar()
        {
            PreExcuteCommand(ExcuteType.ExecuteScalar);
            object ret = target.ExecuteScalar();
            PostExcuteCommand(ExcuteType.ExecuteScalar);
            return ret;
        }

        public IDataParameterCollection Parameters
        {
            get { return target.Parameters; }
        }

        public void Prepare()
        {
            Transaction = TransactionManager.CurrentTransaction(Connection);
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            target.Prepare();
        }

        public IDbTransaction Transaction
        {
            get { return target.Transaction; }
            set { target.Transaction = value; }
        }

        public UpdateRowSource UpdatedRowSource
        {
            get { return target.UpdatedRowSource; }
            set { target.UpdatedRowSource = value; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            target.Dispose();
        }
        #endregion
    }

    public enum ExcuteType
    {
        ExecuteNonQuery,
        ExecuteReader,
        ExecuteScalar
    }    
}
