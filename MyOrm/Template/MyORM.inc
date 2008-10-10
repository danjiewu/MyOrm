<Script runat="template">
private Regex cleanRegEx = new Regex(@"\s+|_|-|\.", RegexOptions.Compiled);

public string CleanName(string name)
{
	return cleanRegEx.Replace(name, "");
}

public string CamelCase(string name)
{
	string output = CleanName(name);
	return char.ToLower(output[0]) + output.Substring(1);
}

public string PascalCase(string name)
{
	string output = CleanName(name);
	return char.ToUpper(output[0]) + output.Substring(1);
}

public string ClassName(TableSchema table)
{
	string className = table.Name;
	if (className.StartsWith(RemoveTablePrefix))
		className = className.Substring(RemoveTablePrefix.Length);
	return String.Format("{0}", PascalCase(className));
}

public string ForeignKeysName(TableKeySchema foreignKey){
	List<string> foreignKeys = new List<string>();
	foreach(ColumnSchema column in foreignKey.ForeignKeyMemberColumns){
		foreignKeys.Add(PropertyName(column));
	}
	foreignKeys.Sort();
	return String.Join(",",foreignKeys.ToArray());
}

public string ForeignTableAliasName(TableKeySchema foreignKey){
	string aliasName = ClassName(foreignKey.PrimaryKeyTable);
	bool duplicate = false;
	if(Equals(foreignKey.PrimaryKeyTable,foreignKey.ForeignKeyTable))
		duplicate = true;
	else 
	foreach(TableKeySchema key in foreignKey.ForeignKeyTable.ForeignKeys){
		if(Equals(key.PrimaryKeyTable, foreignKey.PrimaryKeyTable) && !Equals(key, foreignKey)){
			duplicate = true;
			break;
		}
	}
	if(duplicate){
		List<string> foreignKeys = new List<string>();
		foreach(ColumnSchema column in foreignKey.ForeignKeyMemberColumns){
			foreignKeys.Add(PropertyName(column));
		}
		foreignKeys.Sort();
		aliasName = String.Join("",foreignKeys.ToArray()) + aliasName ;
	}
	return aliasName;
}

public string ForeignPropertyName(TableKeySchema foreignKey, ColumnSchema column){
	return ForeignTableAliasName(foreignKey) + "_" + PropertyName(column);
}

public string ForeignMemberName(TableKeySchema foreignKey, ColumnSchema column)
{
	return CamelCase(ForeignTableAliasName(foreignKey)) + "_" + PascalCase(column.Name);
}

public string ViewClassName(TableSchema table)
{
	return ClassName(table)+"View";
}

public string MemberName(ColumnSchema column)
{
	return CamelCase(column.Name);
}

public string PropertyName(ColumnSchema column)
{
	return PascalCase(column.Name);
}

public string NativeType(ColumnSchema column){
	switch (column.DataType)
	{
		case DbType.AnsiString: return "string";
		case DbType.AnsiStringFixedLength: return "string";
		case DbType.Binary: return "byte[]";
		case DbType.Boolean: return "bool";
		case DbType.Byte: return "byte";
		case DbType.Currency: return "decimal";
		case DbType.Date: return "DateTime";
		case DbType.DateTime: return "DateTime";
		case DbType.Decimal: return "decimal";
		case DbType.Double: return "double";
		case DbType.Guid: return "Guid";
		case DbType.Int16: return "short";
		case DbType.Int32: return "int";
		case DbType.Int64: return "long";
		case DbType.Object: return "object";
		case DbType.SByte: return "sbyte";
		case DbType.Single: return "float";
		case DbType.String: return "string";
		case DbType.StringFixedLength: return "string";
		case DbType.Time: return "TimeSpan";
		case DbType.UInt16: return "ushort";
		case DbType.UInt32: return "uint";
		case DbType.UInt64: return "ulong";
		case DbType.VarNumeric: return "decimal";
		default:
			return column.NativeType;
	}
}

public bool IsValueType(ColumnSchema column)
{
	switch (column.DataType)
	{
		case DbType.AnsiString: 
		case DbType.AnsiStringFixedLength:		 
		case DbType.String:
		case DbType.StringFixedLength:
		case DbType.Binary:
		case DbType.Object: return false;		
		default:
			return true;
	}
}

public string MemberType(ColumnSchema column)
{
	return NativeType(column) + ((IsValueType(column) && column.AllowDBNull && UseNullable) ? "?" : null);
}
</Script>