using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace MyOrm
{
    public class AutoCommand : IDbCommand
    {
        public AutoCommand(IDbCommand target)
        {
            this.target = target;
        }

        private IDbCommand target;
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
            if (Connection.State == ConnectionState.Open && excuteType != ExcuteType.ExecuteReader) Connection.Close();
        }

        #region IDbCommand Members

        public void Cancel()
        {
            target.Cancel();
        }

        public string CommandText
        {
            get { return target.CommandText; }
            set { target.CommandText = value; }
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

    public static class TransactionManager
    {
        private static Dictionary<IDbConnection, IDbTransaction> transactionCache = new Dictionary<IDbConnection, IDbTransaction>();

        /// <summary>
        /// 在指定数据库链接开始事务
        /// </summary>
        /// <param name="connection">数据库链接</param>
        /// <returns></returns>
        public static IDbTransaction BeginTransaction(IDbConnection connection)
        {
            lock (transactionCache)
            {
                if (CurrentTransaction(connection) == null)
                {
                    transactionCache[connection] = connection.BeginTransaction();
                }
                return transactionCache[connection];
            }
        }

        /// <summary>
        /// 获取指定数据库链接的当前事务
        /// </summary>
        /// <param name="connection">数据库链接</param>
        /// <returns></returns>
        public static IDbTransaction CurrentTransaction(IDbConnection connection)
        {
            IDbTransaction transaction;
            transactionCache.TryGetValue(connection, out transaction);
            return transaction;
        }

        /// <summary>
        /// 回滚指定数据库链接的事务
        /// </summary>
        /// <param name="connection">数据库链接</param>
        public static void Commit(IDbConnection connection)
        {
            lock (transactionCache)
            {
                IDbTransaction transaction = CurrentTransaction(connection);
                if (transaction != null)
                {
                    transaction.Commit();
                    transactionCache[connection] = null;
                }
            }
        }

        /// <summary>
        /// 提交所有事务
        /// </summary>
        public static void Commit()
        {
            lock (transactionCache)
            {
                foreach (IDbConnection connection in transactionCache.Keys)
                {
                    Commit(connection);
                }
            }
        }

        /// <summary>
        /// 回滚指定数据库链接的事务
        /// </summary>
        /// <param name="connection">数据库链接</param>
        public static void Rollback(IDbConnection connection)
        {
            lock (transactionCache)
            {
                IDbTransaction transaction = CurrentTransaction(connection);
                if (transaction != null)
                {
                    transaction.Rollback();
                    transactionCache[connection] = null;
                }
            }
        }

        /// <summary>
        /// 回滚所有事务
        /// </summary>
        public static void Rollback()
        {
            lock (transactionCache)
            {
                foreach (IDbConnection connection in transactionCache.Keys)
                {
                    Rollback(connection);
                }
            }
        }
    }
}
