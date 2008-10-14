using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using MyOrm.Attribute;

namespace MyOrm.Metadata
{
    /// <summary>
    /// 根据Attribute的表信息提供者
    /// </summary>
    public class AttibuteTableInfoProvider : TableInfoProvider
    {
        private Dictionary<Type, TableInfo> tableInfoCache = new Dictionary<Type, TableInfo>();

        /// <summary>
        /// 根据对象类型得到表信息
        /// </summary>
        /// <param name="objectType">对象类型</param>
        /// <returns>表信息</returns>
        public override TableInfo GetTableInfo(Type objectType)
        {
            if (objectType == null) return null;
            if (!tableInfoCache.ContainsKey(objectType))
            {
                TableInfo tableInfo = GenerateTableInfo(objectType);
                tableInfoCache[objectType] = tableInfo;
                tableInfo.JoinTables.AddRange(GenerateTableJoins(objectType));
            }
            return tableInfoCache[objectType];
        }

        private TableInfo GenerateTableInfo(Type objectType)
        {
            TableAttribute[] att = (TableAttribute[])objectType.GetCustomAttributes(typeof(TableAttribute), true);
            if (att.Length != 0)
            {
                TableAttribute tableAttribute = att[0];
                string tableName = tableAttribute.TableName;
                ColumnDefineMode defineMode = tableAttribute.ColumnDefineMode;
                if (String.IsNullOrEmpty(tableName)) tableName = objectType.Name;
                TableInfo table = new TableInfo(tableName, objectType);
                foreach (PropertyInfo property in objectType.GetProperties())
                {
                    ColumnInfo column = GenerateColumnInfo(defineMode, property);
                    if (column != null)
                    {
                        table.Columns.Add(column);
                    }
                }
                return table;
            }
            else return null;
        }

        private ColumnInfo GenerateColumnInfo(ColumnDefineMode defineMode, PropertyInfo property)
        {
            ColumnAttribute[] atts = (ColumnAttribute[])property.GetCustomAttributes(typeof(ColumnAttribute), true);
            if (atts.Length != 0 && (defineMode & ColumnDefineMode.Attribute) == ColumnDefineMode.Attribute)
            {
                ColumnAttribute columnAttribute = atts[0];
                ColumnInfo column = new ColumnInfo(property);
                column.ColumnName = String.IsNullOrEmpty(columnAttribute.ColumnName) ? property.Name : columnAttribute.ColumnName;
                column.IsPrimaryKey = columnAttribute.IsPrimaryKey;
                column.DbType = columnAttribute.DbType == DbType.Object ? Utility.ConvertToDbType(property.PropertyType) : columnAttribute.DbType;
                column.ForeignTable = columnAttribute.Foreign;
                column.Length = columnAttribute.Length;
                column.AllowNull = columnAttribute.AllowNull;
                column.Mode = columnAttribute.ColumnMode & ((property.CanRead ? ColumnMode.Write : ColumnMode.Ignore) | (property.CanWrite ? ColumnMode.Read : ColumnMode.Ignore));
                return column;
            }
            else if ((defineMode & ColumnDefineMode.Property) == ColumnDefineMode.Property)
            {
                ColumnInfo column = new ColumnInfo(property);
                column.ColumnName = property.Name;
                column.Mode = (property.CanRead ? ColumnMode.Write : ColumnMode.Ignore) | (property.CanWrite ? ColumnMode.Read : ColumnMode.Ignore);
                column.DbType = Utility.ConvertToDbType(property.PropertyType);
                return column;
            }
            return null;
        }

        private List<TableJoinInfo> GenerateTableJoins(Type objectType)
        {
            List<TableJoinInfo> tableJoins = new List<TableJoinInfo>();
            if (objectType != null)
                foreach (TableJoinAttribute att in objectType.GetCustomAttributes(typeof(TableJoinAttribute), true))
                {
                    TableJoinInfo tableJoin = new TableJoinInfo();
                    tableJoin.SourceTable = att.SourceTable;
                    tableJoin.AliasName = att.AliasName;
                    tableJoin.JoinType = att.JoinType;
                    tableJoin.TargetTable = GetTableInfo(att.TargetType);
                    tableJoin.ForeignKeys.AddRange(att.ForeignKeys.Split(','));
                    tableJoins.Add(tableJoin);
                }
            return tableJoins;
        }
    }
}
