using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace MyOrm.Common
{
    public class JoinedTable : TableInfo
    {
        public JoinedTable(TableDefinition foreignTable)
            : base(foreignTable)
        {
            JoinType = TableJoinType.Left;
            foreignPrimeKey = new ColumnInfo(this, foreignTable.Keys[0]);
        }

        private ColumnInfo foreignPrimeKey;

        public ColumnInfo ForeignKey
        {
            get;
            internal set;
        }

        public TableJoinType JoinType { get; set; }

        public ColumnInfo ForeignPrimeKey
        {
            get { return foreignPrimeKey; }
        }

        public override string FormattedExpression
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(" {0} join {1} on {2} = {3}", JoinType, base.FormattedExpression, ForeignKey.FormattedExpression, ForeignPrimeKey.FormattedExpression);
                return sb.ToString();
            }
        }
    }

    /// <summary>
    /// 用于查询的关联表
    /// </summary>
    public class TableView : Table
    {
        public TableView(TableDefinition table, ICollection<JoinedTable> joinedTables, ICollection<Column> columns)
            : base(columns)
        {
            this.table = table;
            this.joinedTables = new List<JoinedTable>(joinedTables).AsReadOnly();
        }

        private TableDefinition table;
        private ReadOnlyCollection<JoinedTable> joinedTables;

        public ReadOnlyCollection<JoinedTable> JoinedTables
        {
            get { return joinedTables; }
        }

        public override string FormattedExpression
        {
            get
            {
                StringBuilder sb = new StringBuilder(table.FormattedName + " " + FormattedName);
                foreach (JoinedTable joinedTable in joinedTables)
                {
                    sb.Append(joinedTable.FormattedExpression);
                }
                return sb.ToString();
            }
        }

        public override TableDefinition Definition
        {
            get { return table; }
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
