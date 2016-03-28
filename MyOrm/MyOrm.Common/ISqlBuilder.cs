using System;
using MyOrm;

namespace MyOrm.Common
{
    public interface ISqlBuilder
    {
        string BuildConditionSql(SqlBuildContext context, Condition conditon, System.Collections.IList outputParams);
        string GetSelectSectionSql(string select, string from, string where, string orderBy, int startIndex, int sectionSize);
        string ReplaceSqlName(string sql);
        string ToNativeName(string paramName);
        string ToParamName(string nativeName);
        string ToSqlName(string name);
        string ToSqlParam(string nativeName);
    }
}
