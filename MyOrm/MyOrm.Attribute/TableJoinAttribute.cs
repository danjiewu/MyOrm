using System;
using System.Collections.Generic;
using System.Text;
using MyOrm.Metadata;

namespace MyOrm.Attribute
{
    /// <summary>
    /// ���ݿ���ڲ�ѯʱ�Ĺ�����ϵ
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
    public class TableJoinAttribute : System.Attribute
    {
        private Type targetType;
        private string foreignKeys;
        private string aliasName;
        private TableJoinType joinType;
        private string sourceTable;

        /// <summary>
        /// ָ��Դ�������Ķ������ͺ�������ɹ�����Ϣ
        /// </summary>
        /// <param name="sourceTable">������Դ��</param>
        /// <param name="targetType">�����Ķ�������</param>
        /// <param name="foreignKeys">��������������������Ӧ����������˳�����У���","�ָ�</param>
        public TableJoinAttribute(string sourceTable, Type targetType, string foreignKeys)
        {
            this.sourceTable = sourceTable;
            this.targetType = targetType;
            this.foreignKeys = foreignKeys;
        }

        /// <summary>
        /// ָ�������Ķ������ͺ�������ɹ�����Ϣ
        /// </summary>
        /// <param name="targetType">�����Ķ�������</param>
        /// <param name="foreignKeys">��������������������Ӧ����������˳�����У���","�ָ�</param>
        public TableJoinAttribute(Type targetType, string foreignKeys)
        {
            this.targetType = targetType;
            this.foreignKeys = foreignKeys;
        }

        /// <summary>
        /// Դ��
        /// </summary>
        public string SourceTable
        {
            get { return sourceTable; }
        }

        /// <summary>
        /// �����Ķ�������
        /// </summary>
        public Type TargetType
        {
            get { return targetType; }
        }

        /// <summary>
        /// ����
        /// </summary>
        public string AliasName
        {
            get { return aliasName; }
            set { aliasName = value; }
        }

        /// <summary>
        /// �������ͣ�����Ϊ����������������������
        /// </summary>
        public TableJoinType JoinType
        {
            get { return joinType; }
            set { joinType = value; }
        }

        /// <summary>
        /// ��������������������Ӧ����������˳�����У���","�ָ�
        /// </summary>
        public string ForeignKeys
        {
            get { return foreignKeys; }
        }
    }
}
