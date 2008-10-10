using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace MyOrm.Metadata
{
    /// <summary>
    /// 实体、表定义
    /// </summary>
    public class TableInfo
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="objectType">实体类型</param>
        public TableInfo(string tableName, Type objectType)
        {
            this.tableName = tableName;
            this.objectType = objectType;
        }

        #region 私有变量
        private string tableName;
        private Type objectType;
        private List<ColumnInfo> columns = new List<ColumnInfo>();
        private ReadOnlyCollection<ColumnInfo> keys = null;
        private Dictionary<string, ColumnInfo> columnCache = new Dictionary<string, ColumnInfo>(StringComparer.OrdinalIgnoreCase);
        private Dictionary<string, ColumnInfo> propertyCache = new Dictionary<string, ColumnInfo>(StringComparer.OrdinalIgnoreCase);
        private List<TableJoinInfo> joinTables = new List<TableJoinInfo>();
        #endregion

        /// <summary>
        /// 表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
        }

        /// <summary>
        /// 实体类型
        /// </summary>
        public Type ObjectType
        {
            get { return objectType; }
        }

        /// <summary>
        /// 数据库表的列信息
        /// </summary>
        public List<ColumnInfo> Columns
        {
            get { return columns; }
        }

        /// <summary>
        /// 主键列，按名称顺序排列
        /// </summary>
        public ReadOnlyCollection<ColumnInfo> Keys
        {
            get
            {
                if (keys == null)
                {
                    List<ColumnInfo> keyList = columns.FindAll(delegate(ColumnInfo column) { return column.IsPrimaryKey; });
                    keyList.Sort(delegate(ColumnInfo column1, ColumnInfo column2) { return String.Compare(column1.ColumnName, column2.ColumnName); });
                    keys = keyList.AsReadOnly();
                }
                return keys;
            }
        }

        public List<TableJoinInfo> JoinTables
        {
            get { return joinTables; }
        }

        /// <summary>
        /// 列名对应列缓存
        /// </summary>
        protected Dictionary<string, ColumnInfo> ColumnCache
        {
            get
            {
                if (columnCache.Count == 0)
                {
                    foreach (ColumnInfo column in columns)
                        columnCache.Add(column.ColumnName, column);
                }
                return columnCache;
            }
        }

        /// <summary>
        /// 属性名对应列缓存
        /// </summary>
        protected Dictionary<string, ColumnInfo> PropertyCache
        {
            get
            {
                if (propertyCache.Count == 0)
                {
                    foreach (ColumnInfo column in columns)
                        propertyCache.Add(column.PropertyName, column);
                }
                return propertyCache;
            }
        }

        public override string ToString()
        {
            return TableName;
        }

        /// <summary>
        /// 根据列名获得列定义，忽略大小写
        /// </summary>
        /// <param name="name">列名</param>
        /// <returns>列定义，列名不存在则返回null</returns>
        public virtual ColumnInfo GetColumn(string name)
        {
            if (String.IsNullOrEmpty(name)) return null;
            ColumnInfo column;
            ColumnCache.TryGetValue(name, out column);
            return column;
        }

        /// <summary>
        /// 根据属性名获得列定义，忽略大小写
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns>列定义，列名不存在则返回null</returns>
        public virtual ColumnInfo GetColumnByProperty(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName)) return null;
            ColumnInfo column;
            PropertyCache.TryGetValue(propertyName, out column);
            return column;
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void ResetCache()
        {
            keys = null;
            columnCache.Clear();
            propertyCache.Clear();
        }
    }
}
