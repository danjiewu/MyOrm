using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MyOrm.Metadata;

namespace MyOrm.Attribute
{
    /// <summary>
    /// 数据库列属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColumnAttribute : System.Attribute
    {
        private string columnName;
        private bool isPrimaryKey = false;
        private string foreign;
        private DbType dbType = DbType.Object;
        private int length;
        private bool allowNull;
        private ColumnMode columnMode = ColumnMode.Full;

        public ColumnAttribute()
        {
        }

        public ColumnAttribute(string columnName)
        {
            this.columnName = columnName;
        }

        /// <summary>
        /// 数据库列名
        /// </summary>
        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }

        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
            set { isPrimaryKey = value; }
        }

        /// <summary>
        /// 关联的外部表
        /// </summary>
        public string Foreign
        {
            get { return foreign; }
            set { foreign = value; }
        }

        /// <summary>
        /// 数据库列长度
        /// </summary>
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        /// <summary>
        /// 数据库列数据类型
        /// </summary>
        public DbType DbType
        {
            get { return dbType; }
            set { dbType = value; }
        }

        /// <summary>
        /// 是否允许为空
        /// </summary>
        public bool AllowNull
        {
            get { return allowNull; }
            set { allowNull = value; }
        }

        /// <summary>
        /// 列类型
        /// </summary>
        public ColumnMode ColumnMode
        {
            get { return columnMode; }
            set { columnMode = value; }
        }
    }
}
