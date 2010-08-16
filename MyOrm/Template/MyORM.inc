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

public bool IsIndex(ColumnSchema column)
{
    foreach(IndexSchema index in column.Table.Indexes){
        if(index.MemberColumns.Count == 1 && Equals(index.MemberColumns[0], column)) return true;
    }
    return false;
}

public string RemoveIDSuffix(string name)
{
	if(name.EndsWith("ID", StringComparison.OrdinalIgnoreCase))return name.Substring(0,name.Length -2 );
	return name;
}

public string MakePlural(string name)
{
	Regex plural1 = new Regex("(?<keep>[^aeiou])y$");
	Regex plural2 = new Regex("(?<keep>[aeiou]y)$");
	Regex plural3 = new Regex("(?<keep>[sxzh])$");
	Regex plural4 = new Regex("(?<keep>[^sxzhy])$");

	if(plural1.IsMatch(name))
		return plural1.Replace(name, "${keep}ies");
	else if(plural2.IsMatch(name))
		return plural2.Replace(name, "${keep}s");
	else if(plural3.IsMatch(name))
		return plural3.Replace(name, "${keep}es");
	else if(plural4.IsMatch(name))
		return plural4.Replace(name, "${keep}s");

	return name;
}

public string MakeSingle(string name)
{
	Regex plural1 = new Regex("(?<keep>[^aeiou])ies$");
	Regex plural2 = new Regex("(?<keep>[aeiou]y)s$");
	Regex plural3 = new Regex("(?<keep>[sxzh])es$");
	Regex plural4 = new Regex("(?<keep>[^sxzhyu])s$");

	if(plural1.IsMatch(name))
		return plural1.Replace(name, "${keep}y");
	else if(plural2.IsMatch(name))
		return plural2.Replace(name, "${keep}");
	else if(plural3.IsMatch(name))
		return plural3.Replace(name, "${keep}");
	else if(plural4.IsMatch(name))
		return plural4.Replace(name, "${keep}");

	return name;
}

public string ClassName(TableSchema table)
{
	string className = table.Name;
	if (className.StartsWith(RemoveTablePrefix))
		className = className.Substring(RemoveTablePrefix.Length);
	return String.Format("{0}", PascalCase(className));
}

public string ForeignTableAliasName(TableKeySchema foreignKey){
	string aliasName = MakeSingle(ClassName(foreignKey.PrimaryKeyTable));
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
	return table.ForeignKeys.Count == 0? ClassName(table) : ViewName(table);
}

public string ViewName(TableSchema table)
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

public string PropertyString(ColumnSchema column, string calssName, string propertyName){
	return calssName + ".Properties." + propertyName;
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
		case DbType.Object:
			return false;		
		default:
			return true;
	}
}

public string NullableType(ColumnSchema column){
	return NativeType(column) + (IsValueType(column) && UseNullable ? "?" : null);
}

public string MemberType(ColumnSchema column)
{
	return column.AllowDBNull ? NullableType(column) : NativeType(column);
}
</Script>