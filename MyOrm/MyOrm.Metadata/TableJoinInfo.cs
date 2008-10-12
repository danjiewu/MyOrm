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
        private string aliasName;
        private List<string> foreignKeys = new List<string>();
        private TableJoinType joinType;

        /// <summary>
        /// 关联的表信息
        /// </summary>
        public TableInfo TargetTable
        {
            get { return targetTable; }
            set { targetTable = value; }
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
