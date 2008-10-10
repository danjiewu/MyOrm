using System;
using System.Collections.Generic;
using System.Text;

namespace MyOrm.Metadata
{
    /// <summary>
    /// 表关联信息
    /// </summary>
    public class TableJoinInfo
    {
        private TableInfo targetTable;
        private string targetTableName;
        private string aliasName;
        private List<string> foreignKeys = new List<string>();
        private List<string> joinConditions = new List<string>();
        private TableJoinType joinType;

        /// <summary>
        /// 关联的表信息，TargetTable与TargetTableName不会共存
        /// </summary>
        public TableInfo TargetTable
        {
            get { return targetTable; }
            set { targetTable = value; }
        }

        /// <summary>
        /// 关联的表名，TargetTable与TargetTableName不会共存
        /// </summary>
        public string TargetTableName
        {
            get { return targetTableName; }
            set { targetTableName = value; }
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
        /// 关联的外键，仅在TargetTable存在时有意义
        /// </summary>
        public List<string> ForeignKeys
        {
            get { return foreignKeys; }
        }

        /// <summary>
        /// 关联的条件，仅在TargetTableName存在时有意义
        /// </summary>
        public List<string> JoinConditions
        {
            get { return joinConditions; }
        }

        /// <summary>
        /// 关联类型
        /// </summary>
        public TableJoinType JoinType
        {
            get { return joinType; }
            set { joinType = value; }
        }
    }

    /// <summary>
    /// 表关联类型
    /// </summary>
    public enum TableJoinType
    {
        Left,
        Right,
        Outer,
        Inner
    }
}
