using System;
using System.Collections.Generic;
using System.Text;

namespace MyOrm.Common
{
    /// <summary>
    /// ���ݿ���ڲ�ѯʱ�Ĺ�����ϵ
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Interface, AllowMultiple = true, Inherited = true)]
    public class TableJoinAttribute : System.Attribute
    {
        private Type targetType;
        private string foreignKey;
        private string aliasName;
        private TableJoinType joinType = TableJoinType.Left;
        private object sourceTable;

        /// <summary>
        /// ָ��Դ�������Ķ������ͺ�������ɹ�����Ϣ
        /// </summary>
        /// <param name="sourceTable">������Դ��</param>
        /// <param name="targetType">�����Ķ�������</param>
        /// <param name="foreignKeys">���</param>
        public TableJoinAttribute(string sourceTable, Type targetType, string foreignKey)
        {
            this.sourceTable = sourceTable;
            this.targetType = targetType;
            this.foreignKey = foreignKey;
        }


        /// <summary>
        /// ָ��Դ�������Ķ������ͺ�������ɹ�����Ϣ
        /// </summary>
        /// <param name="sourceTable">������Դ��</param>
        /// <param name="targetType">�����Ķ�������</param>
        /// <param name="foreignKeys">���</param>
        public TableJoinAttribute(Type sourceTable, Type targetType, string foreignKey)
        {
            this.sourceTable = sourceTable;
            this.targetType = targetType;
            this.foreignKey = foreignKey;
        }

        /// <summary>
        /// ָ�������Ķ������ͺ�������ɹ�����Ϣ
        /// </summary>
        /// <param name="targetType">�����Ķ�������</param>
        /// <param name="foreignKey">���</param>
        public TableJoinAttribute(Type targetType, string foreignKey)
        {
            this.targetType = targetType;
            this.foreignKey = foreignKey;
        }

        /// <summary>
        /// Դ���������ַ�����Ҳ�����Ƕ�Ӧ�Ķ�������
        /// </summary>
        public object Source
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
        /// �������ͣ�Ĭ��ΪTableJoinType.Left
        /// </summary>
        public TableJoinType JoinType
        {
            get { return joinType; }
            set { joinType = value; }
        }

        /// <summary>
        /// ���
        /// </summary>
        public string ForeignKey
        {
            get { return foreignKey; }
        }
    }

    
}
