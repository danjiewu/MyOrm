using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MyOrm.Attribute
{
    /// <summary>
    /// 数据库表属性，用来标识对象对应的数据库表
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : System.Attribute
    {
        private string tableName = string.Empty;
        private ColumnDefineMode columnDefineMode = ColumnDefineMode.AttributeAndProperty;

        public TableAttribute() { }
        public TableAttribute(string tableName) { TableName = tableName; }

        /// <summary>
        /// 数据库表名
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        /// <summary>
        /// 数据库列定义方式
        /// </summary>
        public ColumnDefineMode ColumnDefineMode
        {
            get { return columnDefineMode; }
            set { columnDefineMode = value; }
        }
    }

    /// <summary>
    /// 数据库列定义方式
    /// </summary>
    public enum ColumnDefineMode : byte
    {
        /// <summary>
        /// 根据对象Property得到数据库列定义
        /// </summary>
        Property = 1,
        /// <summary>
        /// 根据对象Property的Attribute得到数据库列定义
        /// </summary>
        Attribute = 2,
        /// <summary>
        /// 优先使用对象Property的Attribute得到数据库列定义，若不存在Attribute则根据对象Property得到数据库列定义
        /// </summary>
        AttributeAndProperty = Property | Attribute,
        /// <summary>
        /// 根据配置文件得到数据库列定义，未实现
        /// </summary>
        Configuration = 4
    }   
}
