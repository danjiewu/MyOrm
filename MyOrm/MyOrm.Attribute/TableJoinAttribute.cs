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
        private string targetTableName;
        private string foreignKeys;
        private string aliasName;
        private TableJoinType joinType;
        private string[] conditions;

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
        /// 指定表名和关联条件生成关联信息
        /// </summary>
        /// <param name="targetTableName">关联的表名</param>
        /// <param name="conditions">关联条件</param>
        [Obsolete("Use \"TableJoinAttribute(Type targetType, string foreignKeys)\" instead.")]
        public TableJoinAttribute(string targetTableName, string[] conditions)
        {
            this.targetTableName = targetTableName;
            this.conditions = conditions;
        }

        /// <summary>
        /// 关联的对象类型
        /// </summary>
        public Type TargetType
        {
            get { return targetType; }
        }

        /// <summary>
        /// 关联的表名
        /// </summary>
        public string TargetTableName
        {
            get { return targetTableName; }
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

        /// <summary>
        /// 关联条件
        /// </summary>
        public string[] Conditions
        {
            get { return conditions; }
        }
    }
}
