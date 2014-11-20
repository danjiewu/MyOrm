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
        private ObjectDAOBase objectDAO;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dao"></param>
        public AutoCommand(ObjectDAOBase dao)
        {
            if (dao == null) throw new ArgumentNullException("dao");
            this.objectDAO = dao;
        }

        private IDbCommand target;

        /// <summary>
        /// Ä¿±êCommand
        /// </summary>
        public IDbCommand Target
        {
            get
            {
                if (target == null)
                {
                    if (objectDAO.Connection == null) throw new ArgumentNullException("objectDAO.Connection");
                    target = objectDAO.Connection.CreateCommand();
                }
                return target;
            }
        }

        protected virtual void PreExcuteCommand(ExcuteType excuteType)
        {
            if (objectDAO.SessionManager != null) Transaction = objectDAO.SessionManager.CurrentTransaction;
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            //Console.WriteLine(CommandText);//TODO: Add log here.
        }

        protected virtual void PostExcuteCommand(ExcuteType excuteType)
        {
        }

        #region IDbCommand Members

        public void Cancel()
        {
            Target.Cancel();
        }

        private string commandText;
        public string CommandText
        {
            get { return commandText; }
            set
            {
                commandText = value;
                Target.CommandText = objectDAO.SqlBuilder.ReplaceSqlName(value);
            }
        }

        public int CommandTimeout
        {
            get { return Target.CommandTimeout; }
            set { Target.CommandTimeout = value; }
        }

        public CommandType CommandType
        {
            get { return Target.CommandType; }
            set { Target.CommandType = value; }
        }

        public IDbConnection Connection
        {
            get { return Target.Connection; }
            set { Target.Connection = value; }
        }

        public IDbDataParameter CreateParameter()
        {
            return Target.CreateParameter();
        }

        public int ExecuteNonQuery()
        {
            PreExcuteCommand(ExcuteType.ExecuteNonQuery);
            int ret = Target.ExecuteNonQuery();
            PostExcuteCommand(ExcuteType.ExecuteNonQuery);
            return ret;
        }

        public IDataReader ExecuteReader(CommandBehavior behavior)
        {
            PreExcuteCommand(ExcuteType.ExecuteReader);
            IDataReader ret = Target.ExecuteReader(behavior);
            PostExcuteCommand(ExcuteType.ExecuteReader);
            return ret;
        }

        public IDataReader ExecuteReader()
        {
            PreExcuteCommand(ExcuteType.ExecuteReader);
            IDataReader ret = Target.ExecuteReader();
            PostExcuteCommand(ExcuteType.ExecuteReader);
            return ret;
        }

        public object ExecuteScalar()
        {
            PreExcuteCommand(ExcuteType.ExecuteScalar);
            object ret = Target.ExecuteScalar();
            PostExcuteCommand(ExcuteType.ExecuteScalar);
            return ret;
        }

        public IDataParameterCollection Parameters
        {
            get { return Target.Parameters; }
        }

        public void Prepare()
        {
            if (objectDAO.SessionManager != null) Transaction = objectDAO.SessionManager.CurrentTransaction;
            if (Connection.State == ConnectionState.Closed) Connection.Open();
            Target.Prepare();
        }

        public IDbTransaction Transaction
        {
            get { return Target.Transaction; }
            set { Target.Transaction = value; }
        }

        public UpdateRowSource UpdatedRowSource
        {
            get { return Target.UpdatedRowSource; }
            set { Target.UpdatedRowSource = value; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            Target.Dispose();
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
