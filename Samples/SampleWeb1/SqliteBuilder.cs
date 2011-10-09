using System;
using System.Collections.Generic;
using System.Web;
using MyOrm;

namespace Northwind
{
    public class SqliteBuilder : SqlBuilder
    {
        protected override string ConcatSql(params string[] strs)
        {
            return String.Join("||", strs);
        }

        public override string GetSelectSectionSql(string select, string from, string where, string orderBy, int startIndex, int sectionSize)
        {
            return String.Format("select {0} from {1} where {2} order by {3} limit {4},{5}", select, from, where, orderBy, startIndex, sectionSize);
        }
    }
}