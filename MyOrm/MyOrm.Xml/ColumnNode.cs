using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;
using MyOrm.Metadata;
using System.Data;

namespace MyOrm.Xml
{
    /// <summary>
    /// ���ݿ�������
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
        /// ���ݿ�����
        /// </summary>
        [ConfigurationProperty("Property", IsRequired = true)]
        public string Property
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        [ConfigurationProperty("IsPrimaryKey")]
        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
            set { isPrimaryKey = value; }
        }

        /// <summary>
        /// �������ⲿ��
        /// </summary>
        [ConfigurationProperty("Foreign")]
        public string Foreign
        {
            get { return foreign; }
            set { foreign = value; }
        }

        /// <summary>
        /// ���ݿ��г���
        /// </summary>
        [ConfigurationProperty("Length")]
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        /// <summary>
        /// ���ݿ�����������
        /// </summary>
        [ConfigurationProperty("DbType")]
        public DbType DbType
        {
            get { return dbType; }
            set { dbType = value; }
        }

        /// <summary>
        /// �Ƿ�����Ϊ��
        /// </summary>
        [ConfigurationProperty("AllowNull", DefaultValue = false)]
        public bool AllowNull
        {
            get { return allowNull; }
            set { allowNull = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        [ConfigurationProperty("ColumnMode")]
        public ColumnMode ColumnMode
        {
            get { return columnMode; }
            set { columnMode = value; }
        }
    }
}
