using System;
using System.Collections.Generic;
using System.Text;
using MyOrm.Metadata;
using System.Configuration;

namespace MyOrm.Xml
{
    public class TableJoinNode : ConfigurationElement
    {
        private string targetType;
        private string foreignKeys;
        private string aliasName;
        private TableJoinType joinType = TableJoinType.Left;
        private string sourceTable;

        /// <summary>
        /// Դ��
        /// </summary>
        [ConfigurationProperty("SourceTable")]
        public string SourceTable
        {
            get { return sourceTable; }
            set { sourceTable = value; }
        }

        /// <summary>
        /// �����Ķ�������
        /// </summary>
        [ConfigurationProperty("TargetType", IsRequired = true)]
        public string TargetType
        {
            get { return targetType; }
            set { targetType = value; }
        }

        /// <summary>
        /// ����
        /// </summary>
        [ConfigurationProperty("AliasName")]
        public string AliasName
        {
            get { return aliasName; }
            set { aliasName = value; }
        }

        /// <summary>
        /// �������ͣ�Ĭ��ΪTableJoinType.Left
        /// </summary>
        [ConfigurationProperty("JoinType")]
        public TableJoinType JoinType
        {
            get { return joinType; }
            set { joinType = value; }
        }

        /// <summary>
        /// ��������������������Ӧ����������������˳�����У���","�ָ�
        /// </summary>
        [ConfigurationProperty("ForeignKeys")]
        public string ForeignKeys
        {
            get { return foreignKeys; }
            set { foreignKeys = value; }
        }
    }
}
