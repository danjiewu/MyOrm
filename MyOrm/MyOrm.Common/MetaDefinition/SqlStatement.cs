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
        public const string SqlNameFormat = "[{0}]";

        private string name;
        /// <summary>
        /// 名称
        /// </summary>
        public string Name
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

        public override string ToString()
        {
            return FormattedName;
        }
    }
}
