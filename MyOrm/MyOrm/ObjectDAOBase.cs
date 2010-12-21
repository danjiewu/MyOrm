using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Text.RegularExpressions;
using MyOrm.Common;
using System.Data.Common;

namespace MyOrm
{
    /// <summary>
    /// 提供常用操作
    /// </summary>
    public abstract class ObjectDAOBase
    {
        #region 预定义变量
        /// <summary>
        /// 表示SQL查询中条件语句的标记
        /// </summary>
        public const string ParamCondition = "@Condition@";
        /// <summary>
        /// 表示SQL查询中表名的标记
        /// </summary>
        public const string ParamTable = "@Table@";
        #endregion

        #region 私有变量
        private string tableName = null;
        private Exception ExceptionWrongKeys = null;
        private Exception ExceptionNoKeys = null;
        #endregion

        #region 属性
        /// <summary>
        /// 实体对象类型
        /// </summary>
        public abstract Type ObjectType
        {
            get;
        }

        /// <summary>
        /// 表信息
        /// </summary>
        protected abstract Table Table
        {
            get;
        }

        /// <summary>
        /// 表定义
        /// </summary>
        protected TableDefinition TableDefinition
        {
            get { return Table.Definition; }
        }

        /// <summary>
        /// 构建SQL语句的SQLBuilder
        /// </summary>
        protected virtual SqlBuilder SqlBuilder
        {
            get { return null; }
        }

        protected SqlBuildContext CurrentContext
        {
            get { return new SqlBuildContext() { Table = Table }; }
        }

        /// <summary>
        /// 表信息提供者
        /// </summary>
        protected TableInfoProvider Provider
        {
            get { return Configuration.DefaultProvider; }
        }

        /// <summary>
        /// 表名
        /// </summary>
        protected string TableName
        {
            get
            {
                if (tableName == null) tableName = Table.Name;
                return tableName;
            }
        }
        #endregion

        #region 方法
        /// <summary>
        /// 数据库连接
        /// </summary>
        public IDbConnection Connection
        {
            get;
            set;
        }

        /// <summary>
        /// 创建IDbCommand
        /// </summary>
        /// <returns></returns>
        public virtual IDbCommand NewCommand()
        {
            return Connection.CreateCommand();
        }

        /// <summary>
        /// 根据SQL语句和参数建立IDbCommand
        /// </summary>
        /// <param name="SQL">SQL语句，SQL中可以包含参数信息，参数名为以0开始的递增整数，对应paramValues中值的下标</param>
        /// <param name="paramValues">参数值，需要与SQL中的参数一一对应，为空时表示没有参数</param>
        /// <returns>IDbCommand</returns>
        public IDbCommand MakeParamCommand(string SQL, IEnumerable paramValues)
        {
            int paramIndex = 0;
            IDbCommand command = NewCommand();
            if (paramValues != null)
                foreach (object paramValue in paramValues)
                {
                    IDbDataParameter param = command.CreateParameter();
                    param.ParameterName = ToParamName(Convert.ToString(paramIndex++));
                    param.Value = paramValue ?? DBNull.Value;
                    command.Parameters.Add(param);
                }
            command.CommandText = SQL;
            return command;
        }

        /// <summary>
        /// 根据SQL语句和条件建立IDbCommand
        /// </summary>
        /// <param name="SQLWithParam">带参数的SQL语句
        /// <example>"select @AllFields@ from @FromTable@ where @Condition@"表示从表中查询所有符合条件的记录</example>
        /// <example>"select count(*) from @FromTable@ "表示从表中所有记录的数量，condition参数需为空</example>
        /// <example>"delete from @Table@ where @Condition@"表示从表中删除所有符合条件的记录</example>
        /// </param>        
        /// <param name="condition">条件，为null时表示无条件</param>
        /// <returns>IDbCommand</returns>
        public IDbCommand MakeConditionCommand(string SQLWithParam, Condition condition)
        {
            List<object> paramList = new List<object>();
            string strCondition = SqlBuilder.BuildConditionSql(CurrentContext, condition, paramList);
            if (String.IsNullOrEmpty(strCondition)) strCondition = " 1 = 1 ";
            return MakeParamCommand(ReplaceParam(SQLWithParam.Replace(ParamCondition, strCondition)), paramList);
        }

        /// <summary>
        /// 替换Sql中的标记为实际Sql
        /// </summary>
        /// <param name="SQLWithParam">包含标记的Sql语句</param>
        /// <returns></returns>
        protected virtual string ReplaceParam(string SQLWithParam)
        {
            return SQLWithParam.Replace(ParamTable, TableName);
        }

        /// <summary>
        /// 为command创建根据主键查询的条件，在command中添加参数并返回where条件的语句
        /// </summary>
        /// <param name="command">要创建条件的数据库命令</param>
        /// <returns>where条件的语句</returns>
        protected string MakeIsKeyCondition(IDbCommand command)
        {
            ThrowExceptionIfNoKeys();
            StringBuilder strConditions = new StringBuilder();
            foreach (ColumnDefinition key in TableDefinition.Keys)
            {
                if (strConditions.Length != 0) strConditions.Append(" and ");
                strConditions.AppendFormat("{0}.{1} = {2}", ToSqlName(TableName), ToSqlName(key.Name), ToSqlParam(key.PropertyName));
                if (!command.Parameters.Contains(key.PropertyName))
                {
                    IDbDataParameter param = command.CreateParameter();
                    param.Size = key.Length;
                    param.DbType = key.DbType;
                    param.ParameterName = ToParamName(key.PropertyName);
                    command.Parameters.Add(param);
                }
            }
            return strConditions.ToString();
        }

        /// <summary>
        /// 获取对象的主键值
        /// </summary>
        /// <param name="o">对象</param>
        /// <returns>主键值，多个主键按照属性名称顺序排列</returns>
        protected virtual object[] GetKeyValues(object o)
        {
            List<object> values = new List<object>();
            foreach (ColumnDefinition key in TableDefinition.Keys)
            {
                values.Add(key.GetValue(o));
            }
            return values.ToArray();
        }

        /// <summary>
        /// 将数据库取得的值转化为对象属性类型所对应的值
        /// </summary>
        /// <param name="dbValue">数据库取得的值</param>
        /// <param name="column">列属性</param>
        /// <returns>对象属性类型所对应的值</returns>
        protected virtual object ConvertValue(object dbValue, Type objectType)
        {
            if (dbValue == null || dbValue == DBNull.Value)
                return null;

            objectType = Nullable.GetUnderlyingType(objectType) ?? objectType;

            if (objectType.IsInstanceOfType(dbValue))
                return dbValue;

            if (objectType.IsEnum && dbValue.GetType().IsPrimitive) return dbValue;

            return Convert.ChangeType(dbValue, objectType);
        }

        /// <summary>
        /// 将对象的属性值转化为数据库中的值
        /// </summary>
        /// <param name="value">值</param>
        /// <param name="column">列属性</param>
        /// <returns>数据库中的值</returns>
        protected virtual object ConvertToDBValue(object value, ColumnDefinition column)//TODO:
        {
            if (value == null) return DBNull.Value;
            return value;
        }

        /// <summary>
        /// 检查是否存在主键，若不存在则抛出异常
        /// </summary>
        protected void ThrowExceptionIfNoKeys()
        {
            if (TableDefinition.Keys.Count == 0)
            {
                if (ExceptionNoKeys == null) ExceptionNoKeys = new Exception(String.Format("No key definition found in type \"{0}\", please set the value of property \"IsPrimaryKey\" of key column to true.", Table.ObjectType.FullName));
                throw ExceptionNoKeys;
            }
        }

        /// <summary>
        /// 检查主键数目是否正确，否则抛出异常
        /// </summary>
        /// <param name="keys">主键</param>
        protected void ThrowExceptionIfWrongKeys(params object[] keys)
        {
            if (keys.Length != TableDefinition.Keys.Count)
            {
                if (ExceptionWrongKeys == null)
                {
                    List<string> strKeys = new List<string>();
                    foreach (ColumnDefinition key in TableDefinition.Keys) strKeys.Add(key.Name);
                    ExceptionWrongKeys = new ArgumentOutOfRangeException("keys", String.Format("Wrong keys' number. Type \"{0}\" has {1} key(s):'{2}'.", Table.ObjectType.FullName, strKeys.Count, String.Join("','", strKeys.ToArray())));
                }
                throw ExceptionWrongKeys;
            }
        }

        /// <summary>
        /// 名称转化为数据库合法名称
        /// </summary>
        /// <param name="name">字符串名称</param>
        /// <returns>数据库合法名称</returns>
        protected string ToSqlName(string name)
        {
            return SqlBuilder.ToSqlName(name);
        }

        /// <summary>
        /// 原始名称转化为数据库参数
        /// </summary>
        /// <param name="nativeName">原始名称</param>
        /// <returns>数据库参数</returns>
        protected string ToSqlParam(string nativeName)
        {
            return SqlBuilder.ToSqlParam(nativeName);
        }

        /// <summary>
        /// 原始名称转化为参数名称
        /// </summary>
        /// <param name="nativeName">原始名称</param>
        /// <returns>参数名称</returns>
        protected string ToParamName(string nativeName)
        {
            return SqlBuilder.ToParamName(nativeName);
        }

        /// <summary>
        /// 参数名称转化为原始名称
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <returns>原始名称</returns>
        protected string ToNativeName(string paramName)
        {
            return SqlBuilder.ToNativeName(paramName);
        }
        #endregion
    }
}
