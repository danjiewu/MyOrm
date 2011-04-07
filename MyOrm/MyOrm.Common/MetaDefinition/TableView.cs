using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace MyOrm.Common
{
    /// <summary>
    /// �������ⲿ��
    /// </summary>
    public class JoinedTable : TableRef
    {
        /// <summary>
        /// ����������
        /// </summary>
        /// <param name="foreignTable">�ⲿ��ı���</param>
        public JoinedTable(TableDefinition foreignTable)
            : base(foreignTable)
        {
            JoinType = TableJoinType.Left;
            List<ColumnRef> keys = new List<ColumnRef>();
            foreach (ColumnDefinition key in foreignTable.Keys)
            {
                keys.Add(new ColumnRef(this, key));
            }
            foreignPrimeKeys = keys.AsReadOnly();
        }

        private ReadOnlyCollection<ColumnRef> foreignPrimeKeys;
        private ReadOnlyCollection<ColumnRef> foreignKeys;
        /// <summary>
        /// �������ӵ����
        /// </summary>
        public ReadOnlyCollection<ColumnRef> ForeignKeys
        {
            get { return foreignKeys; }
            internal set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value.Count != foreignPrimeKeys.Count) throw new ArgumentException("Quantity of foreignKeys not same as foreignPrimeKeys.");
                foreignKeys = value;
            }
        }

        /// <summary>
        /// ����������
        /// </summary>
        public TableJoinType JoinType { get; set; }

        /// <summary>
        /// �����������
        /// </summary>
        public ReadOnlyCollection<ColumnRef> ForeignPrimeKeys
        {
            get { return foreignPrimeKeys; }
        }

        /// <summary>
        /// ��ʽ���ı��ʽ
        /// </summary>
        public override string FormattedExpression
        {
            get
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat(" {0} join {1} {2} on ", JoinType, base.FormattedExpression, FormattedName);
                for (int i = 0; i < ForeignKeys.Count; i++)
                {
                    if (i > 0) sb.Append(" and ");
                    sb.AppendFormat("{0} = {1}", ForeignKeys[i].FormattedExpression, ForeignPrimeKeys[i].FormattedExpression);
                }
                return sb.ToString();
            }
        }
    }

    /// <summary>
    /// ���ڲ�ѯ�Ĺ�����
    /// </summary>
    public class TableView : Table
    {
        /// <summary>
        /// �������ڲ�ѯ�Ĺ�����
        /// </summary>
        /// <param name="table">����</param>
        /// <param name="joinedTables">���������</param>
        /// <param name="columns">��ѯ���м���</param>
        public TableView(TableDefinition table, ICollection<JoinedTable> joinedTables, ICollection<Column> columns)
            : base(columns)
        {
            this.table = table;
            tables = new List<JoinedTable>(joinedTables);
        }

        private TableDefinition table;
        private List<JoinedTable> tables;
        private ReadOnlyCollection<JoinedTable> joinedTables;

        /// <summary>
        /// �����������
        /// </summary>
        public ReadOnlyCollection<JoinedTable> JoinedTables
        {
            get
            {
                if (joinedTables == null)
                {
                    tables.Sort(delegate(JoinedTable t1, JoinedTable t2)
                    {
                        foreach (ColumnRef column in t1.ForeignKeys)
                        {
                            if (column.Table != null && String.Equals(column.Table.Name, t2.Name, StringComparison.OrdinalIgnoreCase))
                                return 1;
                        }
                        foreach (ColumnRef column in t2.ForeignKeys)
                        {
                            if (column.Table != null && String.Equals(column.Table.Name, t1.Name, StringComparison.OrdinalIgnoreCase))
                                return -1;
                        }
                        return 0;
                    });
                    joinedTables = tables.AsReadOnly();
                }
                return joinedTables;
            }
        }

        /// <summary>
        /// ��ʽ���ı��ʽ
        /// </summary>
        public override string FormattedExpression
        {
            get
            {
                StringBuilder sb = new StringBuilder(table.FormattedName + " " + FormattedName);
                foreach (JoinedTable joinedTable in JoinedTables)
                {
                    sb.Append(joinedTable.FormattedExpression);
                }
                return sb.ToString();
            }
        }

        /// <summary>
        /// ����Ķ���
        /// </summary>
        public override TableDefinition Definition
        {
            get { return table; }
        }
    }

    /// <summary>
    /// ���������
    /// </summary>
    public enum TableJoinType
    {
        /// <summary>
        /// ������
        /// </summary>
        Inner,
        /// <summary>
        /// ������
        /// </summary>
        Left,
        /// <summary>
        /// ������
        /// </summary>
        Right,
        /// <summary>
        /// ȫ������
        /// </summary>
        Outer,
        /// <summary>
        /// ��������
        /// </summary>
        Cross
    }
}
