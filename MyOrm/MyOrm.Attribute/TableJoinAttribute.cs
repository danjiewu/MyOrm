using System;
using System.Collections.Generic;
using System.Text;
using MyOrm.Metadata;

namespace MyOrm.Attribute
{
    /// <summary>
    /// 数据库表在查询时的关联关系
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
        /// 指定源表，关联的对象类型和外键生成关联信息
        /// </summary>
        /// <param name="sourceTable">关联的源表</param>
        /// <param name="targetType">关联的对象类型</param>
        /// <param name="foreignKeys">外键，多个外键按关联表对应的主键名称顺序排列，以","分隔</param>
        public TableJoinAttribute(string sourceTable, Type targetType, string foreignKeys)
        {
            this.sourceTable = sourceTable;
            this.targetType = targetType;
            this.foreignKeys = foreignKeys;
        }

        /// <summary>
        /// 指定关联的对象类型和外键生成关联信息
        /// </summary>
        /// <param name="targetType">关联的对象类型</param>
        /// <param name="foreignKeys">外键，多个外键按关联表对应的主键名称顺序排列，以","分隔</param>
        public TableJoinAttribute(Type targetType, string foreignKeys)
        {
            this.targetType = targetType;
            this.foreignKeys = foreignKeys;
        }

        /// <summary>
        /// 源表
        /// </summary>
        public string SourceTable
        {
            get { return sourceTable; }
        }

        /// <summary>
        /// 关联的对象类型
        /// </summary>
        public Type TargetType
        {
            get { return targetType; }
        }

        /// <summary>
        /// 别名
        /// </summary>
        public string AliasName
        {
            get { return aliasName; }
            set { aliasName = value; }
        }

        /// <summary>
        /// 关联类型，可能为外联、左联、右联、内联
        /// </summary>
        public TableJoinType JoinType
        {
            get { return joinType; }
            set { joinType = value; }
        }

        /// <summary>
        /// 外键，多个外键按关联表对应的主键名称顺序排列，以","分隔
        /// </summary>
        public string ForeignKeys
        {
            get { return foreignKeys; }
        }
    }
}
