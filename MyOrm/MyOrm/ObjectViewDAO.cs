using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections;
using MyOrm.Common;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.Common;

namespace MyOrm
{
    /// <summary>
    /// 实体类的查询操作
    /// </summary>
    /// <typeparam name="T">实体类型</typeparam>
    public class ObjectViewDAO<T> : ObjectDAOBase, IObjectViewDAO<T>, IObjectViewDAO where T : new()
    {
        #region 私有变量
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
            ThrowExceptionIfNoKeys();
            IDbCommand command = NewCommand();
            StringBuilder strConditions = new StringBuilder();
            foreach (ColumnDefinition key in TableDefinition.Keys)
            {
                if (strConditions.Length != 0) strConditions.Append(" and ");
                strConditions.AppendFormat("{0} = {1}", ToSqlName(key.Name), ToSqlParam(key.PropertyName));
                if (!command.Parameters.Contains(key.PropertyName))
                {
                    IDbDataParameter param = command.CreateParameter();
                    param.Size = key.Length;
                    param.DbType = key.DbType;
                    param.ParameterName = ToParamName(key.PropertyName);
                    command.Parameters.Add(param);
                }
            }
            command.CommandText = String.Format("select count(1) from {0} where {1}", ToSqlName(Table.Definition.Name), strConditions);
            command.Prepare();
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
            command.CommandText = String.Format("select {0} from {1} where {2}", AllFieldsSql, From, MakeIsKeyCondition(command));
            command.Prepare();
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
            using (IDbCommand command = MakeConditionCommand("select count(*) from @FromTable@ where @Condition@", condition))
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
            using (IDbCommand command = MakeConditionCommand("select 1 from @FromTable@ where @Condition@", condition))
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
        /// 根据条件查询，多个条件以逻辑与连接
        /// </summary>
        /// <param name="condition">属性名与值的列表，若为null则表示没有条件</param>
        /// <returns>符合条件的对象列表</returns>
        public virtual List<T> Search(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("select @AllFields@ from @FromTable@" + (condition == null ? null : " where @Condition@"), condition))
            {
                return GetAll(command);
            }
        }

        /// <summary>
        /// 获取单个符合条件的对象
        /// </summary>
        /// <param name="condition">属性名与值的列表，若为null则表示没有条件</param>
        /// <returns>第一个符合条件的对象，若不存在则返回null</returns>
        public virtual T SearchOne(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("select @AllFields@ from @FromTable@ where @Condition@", condition))
            {
                return GetOne(command);
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="startIndex">起始位置</param>
        /// <param name="sectionSize">最大记录数</param>
        /// <param name="orderby">排序字段</param>
        /// <param name="direction">排列顺序</param>
        /// <returns></returns>
        public virtual List<T> SearchSection(Condition condition, int startIndex, int sectionSize, string orderby, ListSortDirection direction)
        {
            SectionSet section = new SectionSet() { StartIndex = startIndex, SectionSize = sectionSize };
            if (!String.IsNullOrEmpty(orderby)) section.Orders = new Sorting[] { new Sorting() { PropertyName = orderby, Direction = direction } };
            return SearchSection(condition, section);
        }
        #endregion

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="section">分页设定</param>
        /// <returns></returns>
        public virtual List<T> SearchSection(Condition condition, SectionSet section)
        {
            string sql = SqlBuilder.GetSelectSectionSql(AllFieldsSql, From, ParamCondition, GetOrderBySQL(section.Orders), section.StartIndex, section.SectionSize);
            using (IDbCommand command = MakeConditionCommand(sql, condition))
            {
                return GetAll(command);
            }
        }

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

        IList IObjectViewDAO.SearchSection(Condition condition, SectionSet section)
        {
            return SearchSection(condition, section);
        }

        #endregion

        #region 常用方法

        /// <summary>
        /// 替换Sql中的标记为实际Sql
        /// </summary>
        /// <param name="SQLWithParam">包含标记的Sql语句，标记可以为ParamAllFields，ParamFromTable</param>
        /// <returns></returns>
        protected override string ReplaceParam(string SQLWithParam)
        {
            return base.ReplaceParam(SQLWithParam).Replace(ParamAllFields, AllFieldsSql);
        }

        /// <summary>
        /// 读取所有记录并转化为对象集合，查询AllFieldsSQL时可用
        /// </summary>
        /// <param name="reader">只读结果集</param>
        /// <returns>对象列表</returns>
        private List<T> ReadAll(IDataReader reader)
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
        private T ReadOne(IDataReader dataReader)
        {
            return dataReader.Read() ? Read(dataReader) : default(T);
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

        /// <summary>
        /// 执行IDbCommand，读取所有记录并转化为对象的集合，查询AllFieldsSQL时可用
        /// </summary>
        /// <param name="command">待执行的IDbCommand</param>
        /// <returns></returns>
        protected List<T> GetAll(IDbCommand command)
        {
            using (IDataReader reader = command.ExecuteReader())
            {
                return ReadAll(reader);
            }
        }

        /// <summary>
        /// 执行IDbCommand，读取一条记录并转化为单个对象，查询AllFieldsSQL时可用
        /// </summary>
        /// <param name="command">待执行的IDbCommand</param>
        /// <returns></returns>
        protected T GetOne(IDbCommand command)
        {
            using (IDataReader reader = command.ExecuteReader())
            {
                return ReadOne(reader);
            }
        }
        #endregion
    }
}
