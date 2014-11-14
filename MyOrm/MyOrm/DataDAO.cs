using System;
using System.Collections.Generic;
using System.Text;
using MyOrm.Common;
using System.Data;

namespace MyOrm
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public abstract class DataDAO : ObjectDAOBase
    {
        /// <summary>
        /// 创建指定对象类型的数据访问类
        /// </summary>
        /// <param name="objectType">对象类型</param>
        public DataDAO(Type objectType) : this(objectType, false) { }

        /// <summary>
        /// 创建指定对象类型的数据访问类
        /// </summary>
        /// <param name="objectType">对象类型</param>
        /// <param name="joinTables">是否关联外表</param>
        public DataDAO(Type objectType, bool joinTables)
        {
            this.objectType = objectType;
            if (joinTables) table = Provider.GetTableView(objectType);
            else table = Provider.GetTableDefinition(objectType);
        }

        private Type objectType;
        /// <summary>
        /// 对象类型
        /// </summary>
        public override Type ObjectType
        {
            get { return objectType; }
        }

        private Table table;
        /// <summary>
        /// 表信息
        /// </summary>
        protected override Table Table
        {
            get { return table; }
        }

        /// <summary>
        /// 查询所有列的数据
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public DataTable Select(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("select @AllFields@ from @FromTable@ where @Condition@", condition))
            {
                return GetAll(command, SelectColumns);
            }
        }

        /// <summary>
        /// 查询指定列的数据
        /// </summary>
        /// <param name="selectProperties">需要得到数据的属性集合</param>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public DataTable Select(string[] selectProperties, Condition condition)
        {
            List<Column> columns = new List<Column>();
            foreach (string property in selectProperties)
            {
                Column column = Table.GetColumn(property);
                if (column == null) throw new ArgumentException(String.Format("Type \"{0}\" does not have property \"{1}\"", ObjectType.Name, property), "selectProperties");
                columns.Add(column);
            }

            using (IDbCommand command = MakeConditionCommand("select " + GetSelectFieldsSQL(columns) + "from @FromTable@ where @Condition@", condition))
            {
                return GetAll(command, columns);
            }
        }

        /// <summary>
        /// 分页查询结果
        /// </summary>
        /// <param name="selectProperties">需要得到数据的属性集合</param>
        /// <param name="condition">查询条件</param>
        /// <param name="section">分页设定</param>
        /// <returns></returns>
        public DataTable SelectSection(string[] selectProperties, Condition condition, SectionSet section)
        {
            List<Column> columns = new List<Column>();
            foreach (string property in selectProperties)
            {
                Column column = Table.GetColumn(property);
                if (column == null) throw new ArgumentException(String.Format("Type \"{0}\" does not have property \"{1}\"", ObjectType.Name, property), "selectProperties");
                columns.Add(column);
            }

            string sql = SqlBuilder.GetSelectSectionSql(GetSelectFieldsSQL(columns), From, ParamCondition, GetOrderBySQL(section.Orders), section.StartIndex, section.SectionSize);
            using (IDbCommand command = MakeConditionCommand(sql, condition))
            {
                return GetAll(command, columns);
            }
        }        

        /// <summary>
        /// 根据主键更新字段值
        /// </summary>
        /// <param name="updateValues">需要更新的属性名称以及值集合</param>
        /// <param name="keys">主键，多个主键需按主键名排序</param>
        /// <returns>更新是否成功</returns>
        public bool Update(IEnumerable<KeyValuePair<string, object>> updateValues, params object[] keys)
        {
            ThrowExceptionIfNoKeys();
            ThrowExceptionIfWrongKeys(keys);
            ConditionSet condition = new ConditionSet();
            for (int i = 0; i < keys.Length; i++)
            {
                condition.Add(new SimpleCondition(TableDefinition.Keys[i].PropertyName, keys[i]));
            }
            return Update(updateValues, condition) > 0;
        }

        /// <summary>
        /// 根据条件更新字段值
        /// </summary>
        /// <param name="updateValues">需要更新的属性名称以及值集合</param>
        /// <param name="condition">条件</param>
        /// <returns>更新的记录个数</returns>
        public int Update(IEnumerable<KeyValuePair<string, object>> updateValues, Condition condition)
        {
            List<object> paramList = new List<object>();
            StringBuilder strColumns = new StringBuilder();
            foreach (KeyValuePair<string, object> updateValue in updateValues)
            {
                ColumnDefinition column = TableDefinition.GetColumn(updateValue.Key);
                if (column == null) throw new ArgumentException(String.Format("Type \"{0}\" does not have property \"{1}\"", ObjectType.Name, updateValue.Key));
                strColumns.AppendFormat("{0} = {1}", ToSqlName(column.Name), ToSqlParam(paramList.Count.ToString()));
                paramList.Add(updateValue.Value);
            }
            string sql = String.Format("update {0} set {1} where {2}", ToSqlName(TableName), strColumns, SqlBuilder.BuildConditionSql(CurrentContext, condition, paramList));
            using (IDbCommand command = MakeParamCommand(sql, paramList))
            {
                return command.ExecuteNonQuery();
            }
        }


        /// <summary>
        /// 读取所选择列的数据生成DataTable，将数据库数据转化为实际类型
        /// </summary>
        /// <param name="command">要执行的IDbCommand</param>
        /// <param name="selectColumns">需要读取的列</param>
        /// <returns></returns>
        protected DataTable GetAll(IDbCommand command, IEnumerable<Column> selectColumns)
        {
            DataTable dt = new DataTable();
            using (IDataReader reader = command.ExecuteReader())
            {
                foreach (Column column in selectColumns)
                {
                    dt.Columns.Add(column.PropertyName);
                }
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    foreach (Column column in selectColumns)
                    {
                        row[column.PropertyName] = ConvertValue(reader[column.PropertyName], column.PropertyType);
                    }
                    dt.Rows.Add(row);
                }
            }
            return dt;
        }

        /// <summary>
        /// 读取所有数据生成DataTable
        /// </summary>
        /// <param name="command">要执行的IDbCommand</param>
        /// <returns></returns>
        protected DataTable GetAll(IDbCommand command)
        {
            DataTable dt = new DataTable();
            using (IDataReader reader = command.ExecuteReader())
            {
                dt.Load(reader);
            }
            return dt;
        }
    }
}
