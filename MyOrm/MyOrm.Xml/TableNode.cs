using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace MyOrm.Xml
{
    /// <summary>
    /// 数据库列属性
    /// </summary>
    public class TableNode : ConfigurationElement
    {
        private string type = string.Empty;
        private string tableName = string.Empty;
        private ColumnNode[] columns;
        private TableJoinNode[] tableJoins;
        private ColumnDefineMode columnDefineMode = ColumnDefineMode.Configuration;

        /// <summary>
        /// 实体类型
        /// </summary>
        [ConfigurationProperty("Type", IsRequired = true)]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// 数据库表名
        /// </summary>
        [ConfigurationProperty("TableName")]
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        /// <summary>
        /// 数据库列定义方式
        /// </summary>
        [ConfigurationProperty("ColumnDefineMode", DefaultValue = ColumnDefineMode.Configuration)]
        public ColumnDefineMode ColumnDefineMode
        {
            get { return columnDefineMode; }
            set { columnDefineMode = value; }
        }

        /// <summary>
        /// 关联表定义
        /// </summary>
        [ConfigurationCollection(typeof(TableJoinNode))]
        public TableJoinNode[] TableJoins
        {
            get { return tableJoins; }
            set { tableJoins = value; }
        }

        /// <summary>
        /// 列定义
        /// </summary>
        [ConfigurationCollection(typeof(ColumnNode))]
        public ColumnNode[] Columns
        {
            get { return columns; }
            set { columns = value; }
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
        /// 根据对象Property的配置得到数据库列定义
        /// </summary>
        Configuration = 2,
        /// <summary>
        /// 优先使用对象Property的配置得到数据库列定义，若不存在则根据对象Property得到数据库列定义
        /// </summary>
        ConfigurationAndProperty = Property | Configuration
    }
}
