using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MyOrm
{
    /// <summary>
    /// 事务管理类
    /// </summary>
    public class SessionManager
    {
        private IDbConnection connection;
        private IDbTransaction currentTransaction;
        private readonly object transactionLock = new object();

        public SessionManager(IDbConnection con)
        {
            connection = con;
        }

        /// <summary>
        /// 在指定数据库链接开始事务
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction()
        {
            lock (transactionLock)
            {
                if (currentTransaction == null) currentTransaction = connection.BeginTransaction();
                return currentTransaction;
            }
        }

        /// <summary>
        /// 获取当前事务
        /// </summary>
        /// <returns></returns>
        public IDbTransaction CurrentTransaction
        {
            get { return currentTransaction; }
        }

        /// <summary>
        /// 数据库链接
        /// </summary>
        public IDbConnection Connection
        {
            get { return connection; }
        }

        /// <summary>
        /// 提交数据库链接的事务
        /// </summary>
        public void Commit()
        {
            lock (transactionLock)
            {
                IDbTransaction transaction = CurrentTransaction;
                if (transaction != null)
                {
                    transaction.Commit();
                    currentTransaction = null;
                }
            }
        }

        /// <summary>
        /// 回滚指定数据库链接的事务
        /// </summary>
        /// <param name="connection">数据库链接</param>
        public void Rollback()
        {
            lock (transactionLock)
            {
                IDbTransaction transaction = CurrentTransaction;
                if (transaction != null)
                {
                    transaction.Rollback();
                    currentTransaction = null;
                }
            }
        }
    }
}
