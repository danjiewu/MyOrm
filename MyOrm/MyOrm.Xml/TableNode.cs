using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace MyOrm.Xml
{
    /// <summary>
    /// ���ݿ�������
    /// </summary>
    public class TableNode : ConfigurationElement
    {
        private string type = string.Empty;
        private string tableName = string.Empty;
        private ColumnNode[] columns;
        private TableJoinNode[] tableJoins;
        private ColumnDefineMode columnDefineMode = ColumnDefineMode.Configuration;

        /// <summary>
        /// ʵ������
        /// </summary>
        [ConfigurationProperty("Type", IsRequired = true)]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        /// <summary>
        /// ���ݿ����
        /// </summary>
        [ConfigurationProperty("TableName")]
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        /// <summary>
        /// ���ݿ��ж��巽ʽ
        /// </summary>
        [ConfigurationProperty("ColumnDefineMode", DefaultValue = ColumnDefineMode.Configuration)]
        public ColumnDefineMode ColumnDefineMode
        {
            get { return columnDefineMode; }
            set { columnDefineMode = value; }
        }

        /// <summary>
        /// ��������
        /// </summary>
        [ConfigurationCollection(typeof(TableJoinNode))]
        public TableJoinNode[] TableJoins
        {
            get { return tableJoins; }
            set { tableJoins = value; }
        }

        /// <summary>
        /// �ж���
        /// </summary>
        [ConfigurationCollection(typeof(ColumnNode))]
        public ColumnNode[] Columns
        {
            get { return columns; }
            set { columns = value; }
        }
    }

    /// <summary>
    /// ���ݿ��ж��巽ʽ
    /// </summary>
    public enum ColumnDefineMode : byte
    {
        /// <summary>
        /// ���ݶ���Property�õ����ݿ��ж���
        /// </summary>
        Property = 1,
        /// <summary>
        /// ���ݶ���Property�����õõ����ݿ��ж���
        /// </summary>
        Configuration = 2,
        /// <summary>
        /// ����ʹ�ö���Property�����õõ����ݿ��ж��壬������������ݶ���Property�õ����ݿ��ж���
        /// </summary>
        ConfigurationAndProperty = Property | Configuration
    }
}
