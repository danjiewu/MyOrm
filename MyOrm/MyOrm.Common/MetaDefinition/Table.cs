﻿using System;
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
        public Type DefinitionType
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
        /// 重写ToString方法
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Name;
        }

        /// <summary>
        /// 格式化的表达式
        /// </summary>
        public override string FormattedExpression(ISqlBuilder sqlBuilder)
        {
            return FormattedName(sqlBuilder);
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
        public Type ObjectType
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
    /// 数据库表的引用
    /// </summary>
    public abstract class TableRef : SqlStatement
    {
        /// <summary>
        /// 创建数据库表的引用
        /// </summary>
        /// <param name="table">引用的数据库表定义</param>
        public TableRef(TableDefinition table)
        {
            this.tableDefinition = table;
            Name = table.Name;
            columns = new List<ColumnDefinition>(table.Columns).ConvertAll(delegate(ColumnDefinition column) { return new ColumnRef(this, column); }).AsReadOnly();
        }

        private TableDefinition tableDefinition;
        private ReadOnlyCollection<ColumnRef> columns;
        private Dictionary<string, ColumnRef> namedColumnCache = new Dictionary<string, ColumnRef>();

        /// <summary>
        /// 对应数据库表的定义
        /// </summary>
        public TableDefinition TableDefinition
        {
            get { return tableDefinition; }
        }

        /// <summary>
        /// 数据库表的列信息
        /// </summary>
        public ReadOnlyCollection<ColumnRef> Columns
        {
            get { return columns; }
        }

        /// <summary>
        /// 属性名对应列的缓存
        /// </summary>
        protected Dictionary<string, ColumnRef> NamedColumnCache
        {
            get
            {
                if (namedColumnCache.Count == 0)
                {
                    foreach (ColumnRef column in Columns)
                        namedColumnCache.Add(column.Column.PropertyName, column);
                }
                return namedColumnCache;
            }
        }

        /// <summary>
        /// 根据属性名获得列定义，忽略大小写
        /// </summary>
        /// <param name="propertyName">属性名</param>
        /// <returns>列定义，列名不存在则返回null</returns>
        public virtual ColumnRef GetColumn(string propertyName)
        {
            if (String.IsNullOrEmpty(propertyName)) return null;
            ColumnRef column;
            NamedColumnCache.TryGetValue(propertyName, out column);
            return column;
        }

        /// <summary>
        /// 格式化的表达式
        /// </summary>
        public override string FormattedExpression(ISqlBuilder sqlBuilder)
        {
            return tableDefinition.FormattedExpression(sqlBuilder);
        }
    }
}
