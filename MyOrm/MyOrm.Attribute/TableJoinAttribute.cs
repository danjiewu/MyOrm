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
        private string targetTableName;
        private string foreignKeys;
        private string aliasName;
        private TableJoinType joinType;
        private string[] conditions;

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
        /// ָ�������͹����������ɹ�����Ϣ
        /// </summary>
        /// <param name="targetTableName">�����ı���</param>
        /// <param name="conditions">��������</param>
        [Obsolete("Use \"TableJoinAttribute(Type targetType, string foreignKeys)\" instead.")]
        public TableJoinAttribute(string targetTableName, string[] conditions)
        {
            this.targetTableName = targetTableName;
            this.conditions = conditions;
        }

        /// <summary>
        /// �����Ķ�������
        /// </summary>
        public Type TargetType
        {
            get { return targetType; }
        }

        /// <summary>
        /// �����ı���
        /// </summary>
        public string TargetTableName
        {
            get { return targetTableName; }
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

        /// <summary>
        /// ��������
        /// </summary>
        public string[] Conditions
        {
            get { return conditions; }
        }
    }
}
