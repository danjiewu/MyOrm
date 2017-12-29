﻿using MyOrm.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace MyOrm.MySql
{
    /// <summary>
    /// MySql生成Sql语句的辅助类
    /// </summary>
    public class MySqlBuilder : SqlBuilder
    {
        /// <summary>
        /// 连接各字符串的SQL语句
        /// </summary>
        /// <param name="strs">需要连接的sql字符串</param>
        /// <returns>SQL语句</returns>
        protected override string ConcatSql(params string[] strs)
        {
            return String.Format("CONCAT({0})", String.Join(",", strs));
        }

        public override string BuildIdentityInsertSQL(IDbCommand command, ColumnDefinition identityColumn, string tableName, string strColumns, string strValues)
        {
            return String.Format("insert into {0} ({1}) values ({2}); {3};", tableName, strColumns, strValues, "select @@IDENTITY as [ID];");
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
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public override string ReplaceSqlName(string sql)
        {
            return ReplaceSqlName(sql, '`', '`');
        }

        /// <summary>
        /// 生成分页查询的SQL语句
        /// </summary>
        /// <param name="select">select内容</param>
        /// <param name="from">from块</param>
        /// <param name="where">where条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="startIndex">起始位置，从0开始</param>
        /// <param name="sectionSize">查询条数</param>
        /// <returns></returns>
        public override string GetSelectSectionSql(string select, string from, string where, string orderBy, int startIndex, int sectionSize)
        {
            return String.Format("select {0} from {1} where {2} Order by {3} limit {4},{5} ", select, from, where, orderBy, startIndex, sectionSize);
        }
    }
}
