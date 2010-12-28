﻿using System;
using System.Collections.Generic;
using System.Text;

namespace MyOrm.SqlServer
{
    /// <summary>
    /// SqlServer生成Sql语句的辅助类
    /// </summary>
    public class SqlServerBuilder : SqlBuilder
    {
        /// <summary>
        /// 生成分页查询的SQL语句
        /// </summary>
        /// <param name="select">select内容</param>
        /// <param name="from">from块</param>
        /// <param name="where">where条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="startIndex">起始位置，从1开始</param>
        /// <param name="sectionSize">查询条数</param>
        /// <returns></returns>
        public override string GetSelectSectionSql(string select, string from, string where, string orderBy, int startIndex, int sectionSize)
        {
            if (startIndex == 0)
                return String.Format("select top {0} {1} from {2} where {3} Order by {4} ", sectionSize, select, from, where, orderBy);
            else
                return base.GetSelectSectionSql(select, from, where, orderBy, startIndex, sectionSize);
        }
    }
}
