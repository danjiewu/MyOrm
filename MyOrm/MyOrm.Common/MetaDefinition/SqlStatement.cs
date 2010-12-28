using System;
using System.Collections.Generic;
using System.Text;

namespace MyOrm.Common
{
    /// <summary>
    /// SQL项
    /// </summary>
    public abstract class SqlStatement
    {
        /// <summary>
        /// Sql名称的格式
        /// </summary>
        public const string SqlNameFormat = "[{0}]";

        private string name;
        /// <summary>
        /// 名称
        /// </summary>
        public virtual string Name
        {
            get { return name; }
            internal set
            {
                name = value;
                FormattedName = String.Format(SqlNameFormat, name);
            }
        }

        /// <summary>
        /// 格式化的名称
        /// </summary>
        public string FormattedName { get; private set; }

        /// <summary>
        /// 格式化的表达式
        /// </summary>
        public abstract string FormattedExpression { get; }


        /// <summary>
        /// 得到名称的字符串
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return FormattedName;
        }
    }
}
