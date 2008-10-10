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
        private string targetTableName;
        private string aliasName;
        private List<string> foreignKeys = new List<string>();
        private List<string> joinConditions = new List<string>();
        private TableJoinType joinType;

        /// <summary>
        /// �����ı���Ϣ��TargetTable��TargetTableName���Ṳ��
        /// </summary>
        public TableInfo TargetTable
        {
            get { return targetTable; }
            set { targetTable = value; }
        }

        /// <summary>
        /// �����ı�����TargetTable��TargetTableName���Ṳ��
        /// </summary>
        public string TargetTableName
        {
            get { return targetTableName; }
            set { targetTableName = value; }
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
        /// ���������������TargetTable����ʱ������
        /// </summary>
        public List<string> ForeignKeys
        {
            get { return foreignKeys; }
        }

        /// <summary>
        /// ����������������TargetTableName����ʱ������
        /// </summary>
        public List<string> JoinConditions
        {
            get { return joinConditions; }
        }

        /// <summary>
        /// ��������
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
        Left,
        Right,
        Outer,
        Inner
    }
}
