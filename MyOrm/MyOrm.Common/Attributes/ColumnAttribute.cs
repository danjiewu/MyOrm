using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MyOrm.Common
{
    /// <summary>
    /// ���ݿ��ж���
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public ColumnAttribute()
        {
            ColumnMode = ColumnMode.Full;
            DbType = DbType.Object;
            AllowNull = true;
        }

        /// <summary>
        /// ���캯��
        /// <param name="isColumn">�Ƿ������ݿ���</param>
        /// </summary>
        public ColumnAttribute(bool isColumn)
            : this()
        {
            this.isColumn = isColumn;
        }

        /// <summary>
        /// ָ�������Ĺ��캯��
        /// </summary>
        /// <param name="columnName">����</param>
        public ColumnAttribute(string columnName)
            : this(true)
        {
            ColumnName = columnName;
        }

        private readonly bool isColumn = true;

        /// <summary>
        /// �Ƿ������ݿ���
        /// </summary>
        public bool IsColumn
        {
            get { return isColumn; }
        }

        /// <summary>
        /// ���ݿ�����
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// �Ƿ�����������ʶ
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// �Ƿ�������
        /// </summary>
        public bool IsIndex { get; set; }

        /// <summary>
        /// �Ƿ�Ψһ
        /// </summary>
        public bool IsUnique { get; set; }

        /// <summary>
        /// ���ݿ��г���
        /// </summary>
        public int Length { get; set; }

        /// <summary>
        /// ���ݿ�����������
        /// </summary>
        public DbType DbType { get; set; }

        /// <summary>
        /// ������
        /// </summary>
        public ColumnMode ColumnMode { get; set; }

        /// <summary>
        /// �Ƿ�����Ϊ��
        /// </summary>
        public bool AllowNull { get; set; }
    }

    /// <summary>
    /// ��������Ϣ
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ForeignColumnAttribute : Attribute
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="foreignType">�������ⲿ��������</param>
        public ForeignColumnAttribute(Type foreignType)
        {
            this.Foreign = foreignType;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="foreignName">�������ⲿ������</param>
        public ForeignColumnAttribute(string foreignName)
        {
            this.Foreign = foreignName;
        }

        /// <summary>
        /// �������ⲿ������Ϊ�ⲿ���Ӧ��Type��Ҳ����ΪTableJoin�е�AliasName
        /// </summary>
        public object Foreign { get; private set; }

        ///<summary>
        /// �������ⲿ��������
        /// </summary>
        public string Property { get; set; }
    }
}
