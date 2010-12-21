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
    public class DataDAO : ObjectDAOBase
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

        public DataTable Select(Condition condition)
        {
            using (IDbCommand command = MakeConditionCommand("select @AllFields from @FromTable@ where @Condition@", condition))
            {
                return GetAll(command, SelectColumns);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <param name="selectProperties">需要读取数据的属性集合</param>
        /// <returns></returns>
        public DataTable Select(Condition condition, params string[] selectProperties)
        {
            StringBuilder strSelectFields = new StringBuilder();
            List<Column> columns = new List<Column>();
            foreach (string property in selectProperties)
            {
                Column column = Table.GetColumn(property);
                if (column == null) throw new ArgumentException(String.Format("Type \"{0}\" does not have property \"{1}\"", ObjectType.Name, property), "properties");
                if (strSelectFields.Length != 0) strSelectFields.Append(",");
                strSelectFields.Append(column.FormattedExpression + " as " + column.PropertyName);
                columns.Add(column);
            }
            using (IDbCommand command = MakeConditionCommand("select " + strSelectFields + "from @FromTable@ where @Condition@", condition))
            {
                return GetAll(command, columns.ToArray());
            }
        }

        /// <summary>
        /// 读取所选择列的数据生成DataTable
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
