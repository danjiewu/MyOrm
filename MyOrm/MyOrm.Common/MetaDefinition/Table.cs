using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace MyOrm.Common
{
    /// <summary>
    /// 数据库表定义
    /// </summary>
    public abstract class Table : SqlStatement
    {
        internal Table(ICollection<Column> columns)
        {
            this.columns = new List<Column>(columns).AsReadOnly();
            foreach (Column column in columns) column.Table = this;
        }

        #region 私有变量
        private ReadOnlyCollection<Column> columns;
        private Dictionary<string, Column> namedColumnCache = new Dictionary<string, Column>(StringComparer.OrdinalIgnoreCase);
        #endregion
        
        /// <summary>
        /// 对应的数据库表的定义
        /// </summary>
        public abstract TableDefinition Definition
        {
            get;
        }

        /// <summary>
        /// 对象类型
        /// </summary>
        public Type ObjectType
        {
            get { return Definition.ObjectType; }
        }

        /// <summary>
        /// 数据库表的列信息，包括关联的外部列
        /// </summary>
        public ReadOnlyCollection<Column> Columns
        {
            get { return columns; }
        }

        /// <summary>
        /// 属性名对应列的缓存
        /// </summary>
        protected Dictionary<string, Column> NamedColumnCache
        {
            get
            {
                if (namedColumnCache.Count == 0)
                {
                    foreach (Column column in Columns)
                        namedColumnCache.Add(column.PropertyName, column);
                }
                return namedColumnCache;
            }
        }

        /// <summary>
        /// 根据属性名获得列定义，忽略大小写
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns>列定义，列名不存在则返回null</returns>
        public virtual Column GetColumn(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName)) return null;
            Column column;
            NamedColumnCache.TryGetValue(propertyName, out column);
            return column;
        }

        /// <summary>
        /// 清空缓存
        /// </summary>
        public void ClearCache()
        {
            namedColumnCache.Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// 格式化的表达式
        /// </summary>
        public override string FormattedExpression
        {
            get
            {
                return FormattedName;
            }
        }
    }

    /// <summary>
    /// 数据库表的定义
    /// </summary>
    public class TableDefinition : Table
    {
        internal TableDefinition(Type objectType, ICollection<ColumnDefinition> columns) :
            base(new List<ColumnDefinition>(columns).ConvertAll<Column>(delegate(ColumnDefinition column) { return column; }))
        {
            this.objectType = objectType;
            columnDefinitions = new List<ColumnDefinition>(columns).AsReadOnly();
        }

        private Type objectType;
        private ReadOnlyCollection<ColumnDefinition> columnDefinitions;
        private ReadOnlyCollection<ColumnDefinition> keys = null;

        /// <summary>
        /// 对应数据库表的定义
        /// </summary>
        public override TableDefinition Definition
        {
            get { return this; }
        }

        /// <summary>
        /// 对象类型
        /// </summary>
        public new Type ObjectType
        {
            get { return objectType; }
        }

        /// <summary>
        /// 数据库表的列定义
        /// </summary>
        public new ReadOnlyCollection<ColumnDefinition> Columns
        {
            get { return columnDefinitions; }
        }

        /// <summary>
        /// 主键列，按属性名称的顺序排列
        /// </summary>
        public ReadOnlyCollection<ColumnDefinition> Keys
        {
            get
            {
                if (keys == null)
                {
                    List<ColumnDefinition> keyList = new List<ColumnDefinition>();
                    foreach (ColumnDefinition column in Columns)
                    {
                        if (column.IsPrimaryKey) keyList.Add(column);
                    }
                    keyList.Sort(delegate(ColumnDefinition column1, ColumnDefinition column2) { return String.Compare(column1.PropertyName, column2.PropertyName); });
                    keys = keyList.AsReadOnly();
                }
                return keys;
            }
        }

        /// <summary>
        /// 根据属性名获得列定义，忽略大小写
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns>列定义，列名不存在则返回null</returns>
        public new ColumnDefinition GetColumn(string propertyName)
        {
            return base.GetColumn(propertyName) as ColumnDefinition;
        }
    }

    /// <summary>
    /// 数据库表的信息
    /// </summary>
    public class TableInfo : SqlStatement
    {
        public TableInfo(TableDefinition table)
        {
            this.table = table;
            Name = table.Name;
            columns = new List<ColumnDefinition>(table.Columns).ConvertAll(delegate(ColumnDefinition column) { return new ColumnInfo(this, column); }).AsReadOnly();
        }

        private TableDefinition table;
        public ReadOnlyCollection<ColumnInfo> columns;
        private Dictionary<string, ColumnInfo> namedColumnCache = new Dictionary<string, ColumnInfo>();

        /// <summary>
        /// 对应数据库表的定义
        /// </summary>
        public TableDefinition Table
        {
            get { return table; }
        }

        /// <summary>
        /// 数据库表的列信息
        /// </summary>
        public ReadOnlyCollection<ColumnInfo> Columns
        {
            get { return columns; }
        }

        /// <summary>
        /// 属性名对应列的缓存
        /// </summary>
        protected Dictionary<string, ColumnInfo> NamedColumnCache
        {
            get
            {
                if (namedColumnCache.Count == 0)
                {
                    foreach (ColumnInfo column in Columns)
                        namedColumnCache.Add(column.Name, column);
                }
                return namedColumnCache;
            }
        }

        /// <summary>
        /// 根据属性名获得列定义，忽略大小写
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns>列定义，列名不存在则返回null</returns>
        public virtual ColumnInfo GetColumn(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName)) return null;
            ColumnInfo column;
            NamedColumnCache.TryGetValue(propertyName, out column);
            return column;
        }

        public override string FormattedExpression
        {
            get
            {
                return String.Format("{0} {1}", table.FormattedExpression, FormattedName);
            }
        }
    }
}
