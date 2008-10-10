using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MyOrm.Attribute
{
    /// <summary>
    /// ���ݿ�����ԣ�������ʶ�����Ӧ�����ݿ��
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : System.Attribute
    {
        private string tableName = string.Empty;
        private ColumnDefineMode columnDefineMode = ColumnDefineMode.AttributeAndProperty;

        public TableAttribute() { }
        public TableAttribute(string tableName) { TableName = tableName; }

        /// <summary>
        /// ���ݿ����
        /// </summary>
        public string TableName
        {
            get { return tableName; }
            set { tableName = value; }
        }

        /// <summary>
        /// ���ݿ��ж��巽ʽ
        /// </summary>
        public ColumnDefineMode ColumnDefineMode
        {
            get { return columnDefineMode; }
            set { columnDefineMode = value; }
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
        /// ���ݶ���Property��Attribute�õ����ݿ��ж���
        /// </summary>
        Attribute = 2,
        /// <summary>
        /// ����ʹ�ö���Property��Attribute�õ����ݿ��ж��壬��������Attribute����ݶ���Property�õ����ݿ��ж���
        /// </summary>
        AttributeAndProperty = Property | Attribute,
        /// <summary>
        /// ���������ļ��õ����ݿ��ж��壬δʵ��
        /// </summary>
        Configuration = 4
    }   
}
