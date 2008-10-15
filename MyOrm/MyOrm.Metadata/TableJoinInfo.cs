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
        private string sourceTable;
        private string aliasName;
        private List<string> foreignKeys = new List<string>();
        private TableJoinType joinType = TableJoinType.Left;

        /// <summary>
        /// 源表
        /// </summary>
        public string SourceTable
        {
            get { return sourceTable; }
            set { sourceTable = value; }
        }

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
        /// 关联的外键，按关联表对应的主键的属性名称顺序排列
        /// </summary>
        public List<string> ForeignKeys
        {
            get { return foreignKeys; }
        }

        /// <summary>
        /// 关联类型，默认为TableJoinType.Left
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
        /// <summary>
        /// 内连接
        /// </summary>
        Inner,
        /// <summary>
        /// 左连接
        /// </summary>
        Left,
        /// <summary>
        /// 右连接
        /// </summary>
        Right,
        /// <summary>
        /// 全外连接
        /// </summary>
        Outer,
        /// <summary>
        /// 交叉连接
        /// </summary>
        Cross
    }
}
