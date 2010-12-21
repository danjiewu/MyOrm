using System;
using System.Collections.Generic;
using System.Text;

namespace MyOrm.MySql
{
    public class MySqlBuilder : SqlBuilder
    {
        protected override string ConcatSql(params string[] strs)
        {
            return String.Format("CONCAT({0})", String.Join(",", strs));
        }

        /// <summary>
        /// 参数名称转化为原始名称
        /// </summary>
        /// <param name="paramName">参数名称</param>
        /// <returns>原始名称</returns>
        public override string ToNativeName(string paramName)
        {
            return paramName.TrimStart('@');
        }

        /// <summary>
        /// 原始名称转化为参数名称
        /// </summary>
        /// <param name="nativeName">原始名称</param>
        /// <returns>参数名称</returns>
        public override string ToParamName(string nativeName)
        {
            return String.Format("@{0}", nativeName);
        }

        /// <summary>
        /// 将列名、表名等替换为数据库合法名称
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override string ReplaceSqlName(string sql)
        {
            return ReplaceSqlName(sql, '`', '`');
        }

        public override string GetSelectSectionSql(string select, string from, string where, string orderBy, int startIndex, int sectionSize)
        {
            return String.Format("select {0} from {1} where {2} Order by {3} limit {4},{5} ", select, from, where, orderBy, startIndex, sectionSize);
        }
    }
}
