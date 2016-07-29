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
        private readonly object transactionLock = new object();

        private Dictionary<IDbConnection, IDbTransaction> transactions = new Dictionary<IDbConnection, IDbTransaction>();

        public SessionManager()
        {
        }

        public void RegisterConnection(IDbConnection connection)
        {
            lock (transactionLock)
            {
                if (!transactions.ContainsKey(connection)) transactions[connection] = null;
            }
        }

        /// <summary>
        /// 在指定数据库链接开始事务
        /// </summary>
        /// <returns></returns>
        public IDbTransaction BeginTransaction(IDbConnection connection)
        {
            lock (transactionLock)
            {
                if (!transactions.ContainsKey(connection) || transactions[connection] == null) transactions[connection] = connection.BeginTransaction();
                return transactions[connection];
            }
        }

        public void BeginTransaction()
        {
            lock (transactionLock)
            {
                foreach (IDbConnection connection in new List<IDbConnection>(transactions.Keys))
                {
                    if (transactions[connection] == null) transactions[connection] = connection.BeginTransaction();
                }
            }
        }

        /// <summary>
        /// 获取当前事务
        /// </summary>
        /// <returns></returns>
        public IDbTransaction GetCurrentTransaction(IDbConnection connection)
        {
            if (transactions.ContainsKey(connection)) return transactions[connection];
            else return null;
        }

        /// <summary>
        /// 提交数据库链接的事务
        /// </summary>
        public void Commit(IDbConnection connection)
        {
            lock (transactionLock)
            {
                if (transactions.ContainsKey(connection) && transactions[connection] != null)
                {
                    transactions[connection].Commit();
                    transactions[connection] = null;
                }
            }
        }

        public void Commit()
        {
            lock (transactionLock)
            {
                foreach (IDbConnection connection in new List<IDbConnection>(transactions.Keys))
                {
                    if (transactions[connection] != null)
                    {
                        transactions[connection].Commit();
                        transactions[connection] = null;
                    }
                }
            }
        }

        /// <summary>
        /// 回滚指定数据库链接的事务
        /// </summary>
        /// <param name="connection">数据库链接</param>
        public void Rollback(IDbConnection connection)
        {
            lock (transactionLock)
            {
                if (transactions.ContainsKey(connection) && transactions[connection] != null)
                {
                    transactions[connection].Rollback();
                    transactions[connection] = null;
                }
            }
        }

        public void Rollback()
        {
            lock (transactionLock)
            {
                foreach (IDbConnection connection in new List<IDbConnection>(transactions.Keys))
                {
                    if (transactions[connection] != null)
                    {
                        transactions[connection].Rollback();
                        transactions[connection] = null;
                    }
                }
            }
        }
    }
}
