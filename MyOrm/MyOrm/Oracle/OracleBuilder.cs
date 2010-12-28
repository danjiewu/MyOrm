using System;
using System.Collections.Generic;
using System.Text;
using MyOrm.Common;
using System.Collections;

namespace MyOrm.Oracle
{
    /// <summary>
    /// Oracle生成Sql语句的辅助类
    /// </summary>
    public class OracleBuilder : SqlBuilder
    {
        /// <summary>
        /// 连接各字符串的SQL语句
        /// </summary>
        /// <param name="strs">需要连接的sql字符串</param>
        /// <returns>SQL语句</returns>
        protected override string ConcatSql(params string[] strs)
        {
            return String.Join(" || ", strs);
        }

        /// <summary>
        /// 将列名、表名等替换为数据库合法名称
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public override string ReplaceSqlName(string sql)
        {
            return ReplaceSqlName(sql, '"', '"');
        }
    }
}
