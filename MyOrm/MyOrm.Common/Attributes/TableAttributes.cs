using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MyOrm.Common
{
    /// <summary>
    /// ���ݿ�����ԣ�������ʶ�����Ӧ�����ݿ��
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = false, Inherited = true)]
    public class TableAttribute : System.Attribute
    {
        /// <summary>
        /// Ĭ�Ϲ��캯��
        /// </summary>
        public TableAttribute() { }
        /// <summary>
        /// ָ�������Ĺ��캯��
        /// </summary>
        /// <param name="tableName">����</param>
        public TableAttribute(string tableName) { TableName = tableName; }

        /// <summary>
        /// ���ݿ����
        /// </summary>
        public string TableName { get; set; }
    }   
}
