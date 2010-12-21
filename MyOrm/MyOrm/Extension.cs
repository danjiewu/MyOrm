using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace MyOrm
{
    public class AutoCommand : IDbCommand
    {
        public AutoCommand(SqlBuilder sqlBuilder, IDbCommand target)
        {
            this.sqlBuilder = sqlBuilder;
            this.target = target;
        }

        private SqlBuilder sqlBuilder;
        public SqlBuilder SqlBuilder { get { return sqlBuilder; } }

        private IDbCommand target;
        public IDbCommand Target
        {
            get { return target; }
        }

        protected virtual void PreExcuteCommand(ExcuteType excuteType)
        {
            Transaction = DbConnectionManager.CurrentTransaction(Connection);
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
            Transaction = DbConnectionManager.CurrentTransaction(Connection);
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

    public static class DbConnectionManager
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
        /// <param name="connection">数据库链接</param>
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
