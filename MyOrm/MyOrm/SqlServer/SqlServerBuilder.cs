using System;
using System.Collections.Generic;
using System.Text;

namespace MyOrm.SqlServer
{
    public class SqlServerBuilder : SqlBuilder
    {
        public override string GetSelectSectionSql(string select, string from, string where, string orderBy, int startIndex, int sectionSize)
        {
            if (startIndex == 0)
                return String.Format("select top {0} {1} from {2} where {3} Order by {4} ", sectionSize, select, from, where, orderBy);
            else
                return base.GetSelectSectionSql(select, from, where, orderBy, startIndex, sectionSize);
        }
    }
}
