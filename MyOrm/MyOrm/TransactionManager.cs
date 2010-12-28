using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MyOrm
{
    /// <summary>
    /// 事务管理类
    /// </summary>
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
                if (CurrentTransaction(connection) == null || CurrentTransaction(connection).Connection == null)
                {
                    transactionCache[connection] = connection.BeginTransaction();
                }
                return transactionCache[connection];
            }
        }

        /// <summary>
        /// 在默认数据库链接开始事务
        /// </summary>
        /// <returns></returns>
        public static IDbTransaction BeginDefaultTransaction()
        {
            return BeginTransaction(Configuration.DefaultConnection);
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
        /// 获取默认数据库链接的当前事务
        /// </summary>
        /// <returns></returns>
        public static IDbTransaction DefaultTransaction()
        {
            IDbTransaction transaction;
            transactionCache.TryGetValue(Configuration.DefaultConnection, out transaction);
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
        /// 提交默认数据库连接事务
        /// </summary>
        public static void CommitDefault()
        {
            Commit(Configuration.DefaultConnection);
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
        /// 回滚默认数据库连接事务
        /// </summary>
        public static void RollbackDefault()
        {
            Rollback(Configuration.DefaultConnection);
        }
    }
}
