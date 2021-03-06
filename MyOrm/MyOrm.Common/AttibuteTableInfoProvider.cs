﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Collections.ObjectModel;

namespace MyOrm.Common
{
    /// <summary>
    /// 根据Attribute的表信息提供者
    /// </summary>
    public class AttibuteTableInfoProvider : TableInfoProvider
    {
        private Dictionary<Type, TableDefinition> tableInfoCache = new Dictionary<Type, TableDefinition>();
        private Dictionary<PropertyInfo, ColumnDefinition> columnCache = new Dictionary<PropertyInfo, ColumnDefinition>();
        private Dictionary<Type, TableView> tableViewCache = new Dictionary<Type, TableView>();

        private static readonly object SyncLock = new object();
        /// <summary>
        /// 根据对象类型得到对应的数据库表定义
        /// </summary>
        /// <param name="objectType">对象类型</param>
        /// <returns>表定义</returns>
        public override TableDefinition GetTableDefinition(Type objectType)
        {
            if (objectType == null) return null;
            if (!tableInfoCache.ContainsKey(objectType))
            {
                lock (SyncLock)
                {
                    if (!tableInfoCache.ContainsKey(objectType))
                        tableInfoCache[objectType] = GenerateTableDefinition(objectType);
                }
            }
            return tableInfoCache[objectType];
        }

        /// <summary>
        /// 根据对象类型得到表以及关联信息
        /// </summary>
        /// <param name="objectType">对象类型</param>
        /// <returns>表信息</returns>
        public override TableView GetTableView(Type objectType)
        {
            if (objectType == null) return null;
            if (!tableViewCache.ContainsKey(objectType))
            {
                lock (SyncLock)
                {
                    if (!tableViewCache.ContainsKey(objectType))
                        tableViewCache[objectType] = GenerateTableView(objectType);
                }
            }
            return tableViewCache[objectType];
        }

        #region

        /// <summary>
        /// 根据属性得到对应字段的数据库列定义
        /// </summary>
        /// <param name="property">对象的属性</param>
        /// <returns>数据库列定义</returns>
        private ColumnDefinition GetColumnDefinition(PropertyInfo property)
        {
            if (property == null) return null;
            if (!columnCache.ContainsKey(property))
            {
                lock (SyncLock)
                {
                    if (!columnCache.ContainsKey(property))
                        columnCache[property] = GenerateColumnDefinition(property);
                }
            }
            return columnCache[property];
        }

        private TableDefinition GenerateTableDefinition(Type objectType)
        {
            TableAttribute tableAttribute = Utility.GetAttribute<TableAttribute>(objectType);
            if (tableAttribute != null)
            {
                string tableName = tableAttribute.TableName;
                if (String.IsNullOrEmpty(tableName)) tableName = objectType.Name;
                List<ColumnDefinition> columns = new List<ColumnDefinition>();
                foreach (PropertyInfo property in objectType.GetProperties())
                {
                    ColumnDefinition column = GetColumnDefinition(property);
                    if (column != null)
                    {
                        columns.Add(column);
                    }
                }
                return new TableDefinition(objectType, columns) { Name = tableName };
            }
            return null;
        }

        private ColumnDefinition GenerateColumnDefinition(PropertyInfo property)
        {
            if (property.GetIndexParameters().Length != 0) return null;
            ForeignTypeAttribute foreignTypeAttr = Utility.GetAttribute<ForeignTypeAttribute>(property);
            Type foreignType = foreignTypeAttr != null ? foreignTypeAttr.ObjectType : null;

            if (Utility.GetAttribute<ForeignColumnAttribute>(property) != null) return null;
            ColumnAttribute columnAttribute = Utility.GetAttribute<ColumnAttribute>(property);
            if (columnAttribute != null)
            {
                if (!columnAttribute.IsColumn)
                {
                    return null;
                }
                else
                {
                    ColumnDefinition column = new ColumnDefinition(property);
                    if (!String.IsNullOrEmpty(columnAttribute.ColumnName)) column.Name = columnAttribute.ColumnName;
                    column.IsPrimaryKey = columnAttribute.IsPrimaryKey;
                    column.IsIdentity = columnAttribute.IsIdentity;
                    column.IdentityExpression = columnAttribute.IdentityExpression;
                    column.IsUnique = columnAttribute.IsUnique;
                    column.IsIndex = columnAttribute.IsIndex;
                    column.DbType = columnAttribute.DbType == DbType.Object ? Utility.GetDbType(property.PropertyType) : columnAttribute.DbType;
                    column.Length = columnAttribute.Length == 0 ? Utility.GetDefaultLength(column.DbType) : columnAttribute.Length;
                    column.AllowNull = columnAttribute.AllowNull && (property.PropertyType.IsValueType ? Nullable.GetUnderlyingType(property.PropertyType) != null : true);
                    column.Mode = columnAttribute.ColumnMode & ((property.CanRead ? ColumnMode.Write : ColumnMode.None) | (property.CanWrite ? ColumnMode.Read : ColumnMode.None));
                    column.ForeignType = foreignType;
                    column.ForeignAlias = foreignTypeAttr == null ? null : foreignTypeAttr.Alias;
                    return column;
                }
            }
            else
            {
                ColumnDefinition column = new ColumnDefinition(property);
                column.Name = property.Name;
                column.Mode = (property.CanRead ? ColumnMode.Write : ColumnMode.None) | (property.CanWrite ? ColumnMode.Read : ColumnMode.None);
                column.DbType = Utility.GetDbType(property.PropertyType);
                column.Length = Utility.GetDefaultLength(column.DbType);
                column.AllowNull = property.PropertyType.IsValueType ? Nullable.GetUnderlyingType(column.PropertyType) != null : true;
                column.ForeignType = foreignType;
                column.ForeignAlias = foreignTypeAttr == null ? null : foreignTypeAttr.Alias;
                return column;
            }
        }

        private ForeignColumn GenerateForeignColumn(PropertyInfo property)
        {
            if (property.GetIndexParameters().Length != 0) return null;

            ForeignColumnAttribute foreignColumnAttribute = Utility.GetAttribute<ForeignColumnAttribute>(property);
            if (foreignColumnAttribute != null)
            {
                Type foreignType = Utility.GetAttribute<ForeignTypeAttribute>(property) != null ? Utility.GetAttribute<ForeignTypeAttribute>(property).ObjectType : null;

                ForeignColumn foreignColumn = new ForeignColumn(property);
                foreignColumn.ForeignType = foreignType;
                return foreignColumn;
            }
            else
            {
                return null;
            }
        }

        private TableView GenerateTableView(Type objectType)
        {
            TableJoinAttribute[] atts = (TableJoinAttribute[])objectType.GetCustomAttributes(typeof(TableJoinAttribute), true);
            Dictionary<string, JoinedTable> joinedTables = new Dictionary<string, JoinedTable>(StringComparer.OrdinalIgnoreCase);

            foreach (TableJoinAttribute tableJoin in atts)
            {
                JoinedTable joinedTable = new JoinedTable(GetTableDefinition(tableJoin.TargetType));
                if (String.IsNullOrEmpty(tableJoin.AliasName))
                    tableJoin.AliasName = joinedTable.Name;
                else
                    joinedTable.Name = tableJoin.AliasName;
                if (joinedTables.ContainsKey(joinedTable.Name)) throw new ArgumentException(String.Format("Duplicate table alias name \"{0}\"", joinedTable.Name));
                joinedTables.Add(joinedTable.Name, joinedTable);
            }

            List<Column> columns = new List<Column>();
            foreach (PropertyInfo property in objectType.GetProperties())
            {
                ColumnDefinition column = GetColumnDefinition(property);
                if (column != null)
                {
                    columns.Add(column);
                }
            }

            foreach (PropertyInfo property in objectType.GetProperties())
            {
                ForeignColumn foreignColumn = GenerateForeignColumn(property);
                if (foreignColumn != null) columns.Add(foreignColumn);
            }

            foreach (Column column in columns)
            {
                if (column.ForeignType != null)
                {
                    bool foreignTypeExists = false;
                    foreach (JoinedTable joinedTable in joinedTables.Values)
                    {
                        if (joinedTable.TableDefinition.ObjectType == column.ForeignType && (joinedTable.Name == column.ForeignAlias || String.IsNullOrEmpty(column.ForeignAlias)))
                        {
                            foreignTypeExists = true;
                            break;
                        }
                    }

                    if (!foreignTypeExists)
                    {
                        TableDefinition foreignTable = GetTableDefinition(column.ForeignType);
                        JoinedTable joinedTable = new JoinedTable(foreignTable);
                        if (!String.IsNullOrEmpty(column.ForeignAlias)) joinedTable.Name = column.ForeignAlias;
                        List<ColumnRef> foreignKeys = new List<ColumnRef>();
                        foreignKeys.Add(new ColumnRef(column));
                        joinedTable.ForeignKeys = foreignKeys.AsReadOnly();
                        joinedTables.Add(joinedTable.Name, joinedTable);
                    }
                }
            }

            foreach (Column column in columns)
            {
                if (column is ForeignColumn)
                {
                    ForeignColumn foreignColumn = column as ForeignColumn;
                    ForeignColumnAttribute foreignColumnAttribute = Utility.GetAttribute<ForeignColumnAttribute>(column.Property);
                    string primeProperty = String.IsNullOrEmpty(foreignColumnAttribute.Property) ? column.PropertyName : foreignColumnAttribute.Property;
                    Type primeType = foreignColumnAttribute.Foreign as Type;
                    if (primeType != null)
                    {
                        foreach (JoinedTable joinedTable in joinedTables.Values)
                        {
                            if (joinedTable.TableDefinition.ObjectType == primeType)
                            {
                                if (foreignColumn.TargetColumn != null)
                                    throw new ArgumentException(String.Format("Undeterminate table. More than one table of type {0} joined.", primeType.Name));
                                if (joinedTable.GetColumn(primeProperty) == null)
                                    throw new ArgumentException(String.Format("Foreign property {0} not exist in type {1}.", primeProperty, primeType));

                                foreignColumn.TargetColumn = joinedTable.GetColumn(primeProperty);
                            }
                        }
                        if (foreignColumn.TargetColumn == null)
                            throw new ArgumentException(String.Format("Foreign type {0} of property {1} not exist in joined tables.", primeType, column.PropertyName));
                    }
                    else
                    {
                        string foreignTable = (string)foreignColumnAttribute.Foreign;
                        if (!joinedTables.ContainsKey(foreignTable))
                            throw new ArgumentException(String.Format("Foreign table name {0} of property {1} not exist in joined tables.", foreignColumnAttribute.Foreign, column.PropertyName));
                        if (joinedTables[foreignTable].GetColumn(primeProperty) == null)
                            throw new ArgumentException(String.Format("Foreign property {0} not exist in type {1}.", primeProperty, joinedTables[foreignTable].TableDefinition.ObjectType));

                        foreignColumn.TargetColumn = joinedTables[foreignTable].GetColumn(primeProperty);
                    }
                }
            }

            TableView tableView = new TableView(GetTableDefinition(objectType), joinedTables.Values, columns) { Name = objectType.Name };

            foreach (TableJoinAttribute tableJoin in atts)
            {
                if (tableJoin.Source == null)
                {
                    List<ColumnRef> foreignKeys = new List<ColumnRef>();
                    foreach (string keyName in tableJoin.ForeignKeys.Split(','))
                    {
                        foreignKeys.Add(new ColumnRef(tableView.GetColumn(keyName)));
                    }
                    joinedTables[tableJoin.AliasName].ForeignKeys = foreignKeys.AsReadOnly();
                }
                else
                {
                    JoinedTable sourceTable = null;
                    if (tableJoin.Source is string)
                    {
                        string sourceName = (string)tableJoin.Source;
                        if (!joinedTables.ContainsKey(sourceName))
                            throw new ArgumentException(String.Format("Source table \"{0}\" not exist in joined tables.", sourceName));
                        sourceTable = joinedTables[sourceName];
                    }
                    else
                    {
                        Type sourceType = (Type)tableJoin.Source;
                        foreach (JoinedTable joinedTable in joinedTables.Values)
                        {
                            if (joinedTable.TableDefinition.ObjectType == sourceType)
                            {
                                if (sourceTable == null)
                                    sourceTable = joinedTable;
                                else
                                    throw new ArgumentException(String.Format("Undeterminate table. More than one table of type {0} joined.", sourceType));
                            }
                        }
                        if (sourceTable == null)
                            throw new ArgumentException(String.Format("Source table type {0} not exist in joined tables.", sourceType));
                    }
                    List<ColumnRef> foreignKeys = new List<ColumnRef>();
                    foreach (string keyName in tableJoin.ForeignKeys.Split(','))
                    {
                        foreignKeys.Add(sourceTable.GetColumn(keyName));
                    }
                    joinedTables[tableJoin.AliasName].ForeignKeys = foreignKeys.AsReadOnly();
                }
            }

            return tableView;
        }

        #endregion
    }
}
