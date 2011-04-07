using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Data;

namespace MyOrm.Common
{
    [Serializable]
    [XmlType("Table")]
    public class TableNode
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string ObjectType { get; set; }
        public ColumnNode[] Columns { get; set; }
    }

    [Serializable]
    [XmlType("Column")]
    public class ColumnNode
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string Property { get; set; }
        [XmlAttribute]
        public DbType DbType { get; set; }
        [XmlAttribute]
        public int Length { get; set; }
        [XmlAttribute]
        public bool AllowNull { get; set; }
        [XmlAttribute]
        public bool IsIdentity { get; set; }
        [XmlAttribute]
        public bool IsPrimaryKey { get; set; }
        [XmlAttribute]
        public bool IsUnique { get; set; }
        [XmlAttribute]
        public bool IsIndex { get; set; }
        [XmlAttribute]
        public ColumnMode Mode { get; set; }
    }

    [Serializable]
    [XmlType("TableView")]
    public class TableViewNode 
    {
        [XmlAttribute]
        public string Name { get; set; }
        [XmlAttribute]
        public string ObjectType { get; set; }
        public JoinedTableNode[] JoinedTables { get; set; }
        public ViewColumnNode[] Columns { get; set; }
    }

    [Serializable]
    [XmlType("JoinedTable")]
    public class JoinedTableNode
    {
        [XmlAttribute]
        public TableJoinType JoinType { get; set; }
        [XmlAttribute]
        public string ForeignType { get; set; }
        [XmlAttribute]
        public string ForeignKeys { get; set; }
    }

    [Serializable]
    [XmlType("Column")]
    public class ViewColumnNode
    {
        [XmlAttribute]
        public string Property { get; set; }
        [XmlAttribute]
        public string Foreign { get; set; }
        [XmlAttribute]
        public string TargetProperty { get; set; }
    }
}
