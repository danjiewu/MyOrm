using System;
using System.Collections.Generic;
using System.Text;
using MyOrm.Common;
using System.Collections;

namespace MyOrm.Oracle
{
    public class OracleBuilder : SqlBuilder
    {
        protected override string ConcatSql(params string[] strs)
        {
            return String.Join(" || ", strs);
        }

        /// <summary>
        /// 将列名、表名等替换为数据库合法名称
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public override string ReplaceSqlName(string sql)
        {
            return ReplaceSqlName(sql, '"', '"');
        }
    }
}
