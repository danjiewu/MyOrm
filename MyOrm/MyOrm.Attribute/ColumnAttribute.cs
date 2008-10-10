using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MyOrm.Metadata;

namespace MyOrm.Attribute
{
    /// <summary>
    /// ���ݿ�������
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
        /// ���ݿ�����
        /// </summary>
        public string ColumnName
        {
            get { return columnName; }
            set { columnName = value; }
        }

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public bool IsPrimaryKey
        {
            get { return isPrimaryKey; }
            set { isPrimaryKey = value; }
        }

        /// <summary>
        /// �������ⲿ��
        /// </summary>
        public string Foreign
        {
            get { return foreign; }
            set { foreign = value; }
        }

        /// <summary>
        /// ���ݿ��г���
        /// </summary>
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        /// <summary>
        /// ���ݿ�����������
        /// </summary>
        public DbType DbType
        {
            get { return dbType; }
            set { dbType = value; }
        }

        /// <summary>
        /// �Ƿ�����Ϊ��
        /// </summary>
        public bool AllowNull
        {
            get { return allowNull; }
            set { allowNull = value; }
        }

        /// <summary>
        /// ������
        /// </summary>
        public ColumnMode ColumnMode
        {
            get { return columnMode; }
            set { columnMode = value; }
        }
    }
}
