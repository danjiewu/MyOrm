using System;
using System.Collections.Generic;
using System.Text;

namespace MyOrm.Common
{
    public abstract class SqlStatement
    {
        public const string SqlNameFormat = "[{0}]";

        private string name;
        public string Name
        {
            get { return name; }
            internal set
            {
                name = value;
                FormattedName = String.Format(SqlNameFormat, name);
            }
        }

        public string FormattedName { get; private set; }

        public abstract string FormattedExpression { get; }

        public override string ToString()
        {
            return FormattedName;
        }
    }
}
