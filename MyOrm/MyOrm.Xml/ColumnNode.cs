using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using MyOrm.Metadata;
using System.Data;

namespace MyOrm.Xml
{
    /// <summary>
    /// 数据库列属性
    /// </summary>
    public class ColumnNode : ConfigurationElement
    {
        private string propertyName;
        private bool isPrimaryKey = false;
        private string foreign;
        private DbType dbType = DbType.Object;
        private int length;
        private bool allowNull;
        private ColumnMode columnMode = ColumnMode.Full;

        /// <summary>
        /// 数据库列名
        /// </summary>
        [ConfigurationProperty("Property", IsRequired = true)]
        public string Property
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        /// <summary>
        /// 是否是主键
        /// </summary>
        [ConfigurationProperty("IsPrimaryKey")]
        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
            set { isPrimaryKey = value; }
        }

        /// <summary>
        /// 关联的外部表
        /// </summary>
        [ConfigurationProperty("Foreign")]
        public string Foreign
        {
            get { return foreign; }
            set { foreign = value; }
        }

        /// <summary>
        /// 数据库列长度
        /// </summary>
        [ConfigurationProperty("Length")]
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        /// <summary>
        /// 数据库列数据类型
        /// </summary>
        [ConfigurationProperty("DbType")]
        public DbType DbType
        {
            get { return dbType; }
            set { dbType = value; }
        }

        /// <summary>
        /// 是否允许为空
        /// </summary>
        [ConfigurationProperty("AllowNull", DefaultValue = false)]
        public bool AllowNull
        {
            get { return allowNull; }
            set { allowNull = value; }
        }

        /// <summary>
        /// 列类型
        /// </summary>
        [ConfigurationProperty("ColumnMode")]
        public ColumnMode ColumnMode
        {
            get { return columnMode; }
            set { columnMode = value; }
        }
    }
}
