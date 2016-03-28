using System;
using System.Collections.Generic;
using System.Text;

namespace MyOrm.Common
{
    /// <summary>
    /// SqlBuilder生成sql时的上下文
    /// </summary>
    public class SqlBuildContext
    {
        /// <summary>
        /// 表别名
        /// </summary>
        public string TableAliasName { get; set; }
        /// <summary>
        /// 表信息
        /// </summary>
        public Table Table { get; set; }
        /// <summary>
        /// 序列，用来生成表别名
        /// </summary>
        public int Sequence { get; set; }
    }
}
