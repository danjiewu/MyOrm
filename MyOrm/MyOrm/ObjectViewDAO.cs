using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using MyOrm.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace MyOrm
{
    /// <summary>
    /// 实体类的查询操作
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class ObjectViewDAO<T> : ObjectDAOBase, IObjectViewDAO<T>, IObjectViewDAO where T : new()
    {
        #region 私有变量
        private string fromTable = null;
        private string allFieldsSql = null;

        private ReadOnlyCollection<Column> selectColumns;

        private IDbCommand getObjectCommand = null;
        private IDbCommand objectExistsCommand = null;
        #endregion

        #region 属性
        /// <summary>
        /// 实体对象类型
        /// </summary>
        public override Type ObjectType
        {
            get { return typeof(T); }
        }

        /// <summary>
        /// 查询关联表
        /// </summary>
        protected override Table Table
        {
            get { return Provider.GetTableView(ObjectType); }
        }

        /// <summary>
        /// 查询时使用的相关联的多个表
        /// </summary>
        protected string FromTable
        {
            get
            {
                if (fromTable == null)
                {
                    fromTable = Table.FormattedExpression;
                }
                return fromTable;
            }
        }

        /// <summary>
        /// 查询时需要获取的所有列
        /// </summary>
        protected ReadOnlyCollection<Column> SelectColumns
        {
            get
            {
                if (selectColumns == null)
                {
                    selectColumns = new List<Column>(Table.Columns).FindAll(column => !(column is ColumnDefinition && (((ColumnDefinition)column).Mode & ColumnMode.Read) != ColumnMode.Read)).AsReadOnly();
                }
                return selectColumns;
            }
        }

        /// <summary>
        /// 查询时需要获取的所有字段的Sql
        /// </summary>
        protected string AllFieldsSql
        {
            get
            {
                if (allFieldsSql == null)
                {
                    StringBuilder strAllFields = new StringBuilder();
                    foreach (Column column in SelectColumns)
                    {
                        if (strAllFields.Length != 0) strAllFields.Append(",");
                        strAllFields.Append(column.SelectExpression);
                    }
                    allFieldsSql = strAllFields.ToString();
                }
                return allFieldsSql;
            }
        }

        #endregion

        #region 预定义Command
        /// <summary>
        /// 实现检查对象是否存在操作的IDbCommand
        /// </summary>
        protected IDbCommand ObjectExistsCommand
        {
            get
            {
                if (objectExistsCommand == null)
                    objectExistsCommand = MakeObjectExistsCommand();
                return objectExistsCommand;
            }
        }

        private IDbCommand MakeObjectExistsCommand()
        {
            IDbCommand command = NewCommand();
            command.CommandText = String.Format("select count(1) from {0} where {1}", ToSqlName(TableName), MakeIsKeyCondition(command));
            return command;
        }

        /// <summary>
        /// 实现获取对象操作的IDbCommand
        /// </summary>
        protected IDbCommand GetObjectCommand
        {
            get
            {
                if (getObjectCommand == null)
                    getObjectCommand = MakeGetObjectCommand();
                return getObjectCommand;
            }
        }

        private IDbCommand MakeGetObjectCommand()
        {
            IDbCommand command = NewCommand();
            command.CommandText = String.Format("select {0} from {1} where {2}", AllFieldsSql, FromTable, MakeIsKeyCondition(command));
            return command;
        }
        #endregion

        #region 方法
        /// <summary>
        /// 根据主键获取对象
        /// </summary>
        /// <param name="keys">主键，多个主键按照主键名顺序排列</param>
        /// <returns>对象，若不存在则返回null</returns>
        public virtual T GetObject(params object[] keys)
        {
            ThrowExceptionIfWrongKeys(keys);
            int i = 0;
            foreach (IDataParameter param in GetObjectCommand.Parameters)
            {
                param.Value = ConvertToDBValue(keys[i], Table.Definition.Keys[i]);
                i++;
            }
            using (IDataReader reader = GetObjectCommand.ExecuteReader())
            {
                return ReadOne(reader);
            }
        }

        /// <summary>
        /// 判断主键对应的对象是否存在
        /// </summary>
        /// <param name="keys">主键，多个主键按照名称顺序排列</param>
        /// <returns>是否存在</returns>
        public virtual bool Exists(params object[] keys)
        {
            ThrowExceptionIfWrongKeys(keys);
            int i = 0;
            foreach (IDataParameter param in ObjectExistsCommand.Parameters)
            {
                param.Value = ConvertToDBValue(keys[i], Table.Definition.Keys[i]);
                i++;
            }
            return Convert.ToInt32(ObjectExistsCommand.ExecuteScalar()) > 0;
        }

        /// <summary>
        /// 获取符合条件的对象个数
        /// </summary>
        /// <param name="condition">属性名与值的列表，若为null则表示没有条件</param>
        /// <returns>符合条件的对象个数</returns>
        public virtual int Count(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("select count(*) from @FromTable where @Condition", condition))
            {
                return Convert.ToInt32(command.ExecuteScalar());
            }
        }

        /// <summary>
        /// 判断符合条件的对象是否存在
        /// </summary>
        /// <param name="condition">属性名与值的列表，若为null则表示没有条件</param>
        /// <returns>是否存在</returns>
        public virtual bool Exists(Condition condition)
        {
#if MYSQL
            using (IDbCommand command = MakeConditionCommand("select 1 from @FromTable where @Condition limit 1", condition))
#elif ORACLE
            using (IDbCommand command = MakeConditionCommand("select 1 from @FromTable where rownumber = 1 and @Condition", condition))
#else
            using (IDbCommand command = MakeConditionCommand("select top 1 1 from @FromTable where @Condition", condition))
#endif
            {
                return command.ExecuteScalar() != null;
            }
        }

        /// <summary>
        /// 根据单个条件查询
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="value">值</param>
        /// <returns>符合条件的对象列表</returns>
        public List<T> Search(string name, object value)
        {
            return Search(new SimpleCondition(name, value));
        }

        /// <summary>
        /// 根据单个条件查询
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="op">条件判断操作符</param>
        /// <param name="value">值</param>
        /// <returns>符合条件的对象列表</returns>
        public List<T> Search(string name, ConditionOperator op, object value)
        {
            return Search(new SimpleCondition(name, op, value));
        }

        /// <summary>
        /// 根据条件查询，多个条件以逻辑与连接
        /// </summary>
        /// <param name="condition">属性名与值的列表，若为null则表示没有条件</param>
        /// <returns>符合条件的对象列表</returns>
        public virtual List<T> Search(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("select @AllFields from @FromTable" + (condition == null ? null : " where @Condition"), condition))
            {
                using (IDataReader reader = command.ExecuteReader())
                {
                    return ReadAll(reader);
                }
            }
        }

        /// <summary>
        /// 获取单个符合条件的对象
        /// </summary>
        /// <param name="condition">属性名与值的列表，若为null则表示没有条件</param>
        /// <returns>第一个符合条件的对象，若不存在则返回null</returns>
        public virtual T SearchOne(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("select @AllFields from @FromTable where @Condition", condition))
            {
                using (IDataReader reader = command.ExecuteReader())
                {
                    return ReadOne(reader);
                }
            }
        }

        /// <summary>
        /// 按默认排序分页查询
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="sectionSize">最大记录数</param>
        /// <returns>符合条件的分页对象列表</returns>
        public virtual List<T> SearchSection(Condition condition, int startIndex, int sectionSize)
        {
            return SearchSection(condition, startIndex, sectionSize, null);
        }

        /// <summary>
        /// 以升序进行分页查询
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="sectionSize">最大记录数</param>
        /// <param name="orderby">排序字段</param>
        /// <returns>符合条件的分页对象列表</returns>
        public virtual List<T> SearchSection(Condition condition, int startIndex, int sectionSize, string orderby)
        {
            return SearchSection(condition, startIndex, sectionSize, orderby, ListSortDirection.Ascending);
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="sectionSize">最大记录数</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="direction">排列顺序</param>
        /// <returns>符合条件的分页对象列表</returns>
        public virtual List<T> SearchSection(Condition condition, int startIndex, int sectionSize, string orderby, ListSortDirection direction)
        {
            if (string.IsNullOrEmpty(orderby))
            {
                if (Table.Definition.Keys.Count != 0)
                {
                    StringBuilder strKeys = new StringBuilder();
                    foreach (ColumnDefinition key in Table.Definition.Keys)
                    {
                        if (strKeys.Length != 0) strKeys.Append(",");
                        strKeys.Append(String.Format("{0}.{1}", Table.FormattedName, key.FormattedName));
                    }
                    orderby = strKeys.ToString();
                }
                else
                {
                    //TODO: Add one column or all columns?
                    throw new Exception("No columns or keys to sort by.");
                }
            }
            else
            {
                Column column = Table.GetColumn(orderby);
                if (column != null)
                    orderby = column.FormattedExpression;
                else
                {
                    //TODO: The orderby is not a safe sql string. Throw exception or not?
                }
            }
#if MYSQL
            string paramedSQL = String.Format("select @AllFieldsfrom @FromTable where @Condition Order by {0} {1} limit {2},{3} ", orderby, direction == ListSortDirection.Ascending ? "asc" : "desc", startIndex, sectionSize);
#else
            string paramedSQL = String.Format("select * from (select @AllFields, Row_Number() over (Order by {0} {1}) as Row_Number from @FromTable where @Condition) as TempTable where Row_Number > {2} and Row_Number <= {3}", orderby, direction == ListSortDirection.Ascending ? "asc" : "desc", startIndex, startIndex + sectionSize);
#endif
            using (IDbCommand command = MakeConditionCommand(paramedSQL, condition))
            {
                using (IDataReader reader = command.ExecuteReader())
                {
                    return ReadAll(reader);
                }
            }
        }
        #endregion

        #region IObjectViewDAO Members

        object IObjectViewDAO.GetObject(params object[] keys)
        {
            return GetObject(keys);
        }

        object IObjectViewDAO.SearchOne(Condition condition)
        {
            return SearchOne(condition);
        }

        IList IObjectViewDAO.Search(Condition condition)
        {
            return Search(condition);
        }

        IList IObjectViewDAO.SearchSection(Condition condition, int startIndex, int sectionSize, string orderby, ListSortDirection direction)
        {
            return SearchSection(condition, startIndex, sectionSize, orderby, direction);
        }

        #endregion

        #region 常用方法

        protected override IDbCommand MakeConditionCommand(string SQLWithParam, Condition condition)
        {
            SQLWithParam = SQLWithParam.Replace(ParamAllFields, AllFieldsSql).Replace(ParamFromTable, FromTable);
            return base.MakeConditionCommand(SQLWithParam, condition);
        }

        /// <summary>
        /// 读取所有记录并转化为对象集合，查询得到AllFieldsSQL时可用
        /// </summary>
        /// <param name="reader">只读结果集</param>
        /// <returns>对象列表</returns>
        protected List<T> ReadAll(IDataReader reader)
        {
            List<T> results = new List<T>();
            while (reader.Read())
            {
                results.Add(Read(reader));
            }
            return results;
        }

        /// <summary>
        /// 从IDataReader中读取一条记录转化为对象，若无记录则返回null
        /// </summary>
        /// <param name="dataReader">IDataReader</param>
        /// <returns>对象，若无记录则返回null</returns>
        protected T ReadOne(IDataReader dataReader)
        {
            using (IDataReader reader = dataReader)
            {
                return reader.Read() ? Read(reader) : default(T);
            }
        }

        /// <summary>
        /// 将一行记录转化为对象
        /// </summary>
        /// <param name="record">一行记录</param>
        /// <returns>对象</returns>
        protected virtual T Read(IDataRecord record)
        {
            T t = new T();
            int i = 0;
            foreach (Column column in SelectColumns)
            {
                column.SetValue(t, record.IsDBNull(i) ? null : ConvertValue(record[i], column.PropertyType));
                i++;
            }
            return t;
        }

        protected object ConvertValue(object dbValue, Type objectType)
        {
            if (dbValue == null || dbValue == DBNull.Value)
                return null;

            if (objectType.IsGenericType && objectType.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                objectType = Nullable.GetUnderlyingType(objectType);

            if (objectType.IsInstanceOfType(dbValue))
                return dbValue;

            if (objectType.IsEnum) return dbValue;

            return Convert.ChangeType(dbValue, objectType);
        }
        #endregion
    }
}
