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
        /// 源表
        /// </summary>
        [ConfigurationProperty("SourceTable")]
        public string SourceTable
        {
            get { return sourceTable; }
            set { sourceTable = value; }
        }

        /// <summary>
        /// 关联的对象类型
        /// </summary>
        [ConfigurationProperty("TargetType", IsRequired = true)]
        public string TargetType
        {
            get { return targetType; }
            set { targetType = value; }
        }

        /// <summary>
        /// 别名
        /// </summary>
        [ConfigurationProperty("AliasName")]
        public string AliasName
        {
            get { return aliasName; }
            set { aliasName = value; }
        }

        /// <summary>
        /// 关联类型，默认为TableJoinType.Left
        /// </summary>
        [ConfigurationProperty("JoinType")]
        public TableJoinType JoinType
        {
            get { return joinType; }
            set { joinType = value; }
        }

        /// <summary>
        /// 外键，多个外键按关联表对应的主键的属性名称顺序排列，以","分隔
        /// </summary>
        [ConfigurationProperty("ForeignKeys")]
        public string ForeignKeys
        {
            get { return foreignKeys; }
            set { foreignKeys = value; }
        }
    }
}
