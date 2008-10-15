using System;
using System.Collections.Generic;
using System.Text;

namespace MyOrm.Metadata
{
    /// <summary>
    /// �������Ϣ
    /// </summary>
    public class TableJoinInfo
    {
        private TableInfo targetTable;
        private string sourceTable;
        private string aliasName;
        private List<string> foreignKeys = new List<string>();
        private TableJoinType joinType = TableJoinType.Left;

        /// <summary>
        /// Դ��
        /// </summary>
        public string SourceTable
        {
            get { return sourceTable; }
            set { sourceTable = value; }
        }

        /// <summary>
        /// �����ı���Ϣ
        /// </summary>
        public TableInfo TargetTable
        {
            get { return targetTable; }
            set { targetTable = value; }
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
        /// ��������������������Ӧ����������������˳������
        /// </summary>
        public List<string> ForeignKeys
        {
            get { return foreignKeys; }
        }

        /// <summary>
        /// �������ͣ�Ĭ��ΪTableJoinType.Left
        /// </summary>
        public TableJoinType JoinType
        {
            get { return joinType; }
            set { joinType = value; }
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
