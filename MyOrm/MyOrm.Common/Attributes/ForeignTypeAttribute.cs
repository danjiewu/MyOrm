using System;
using System.Collections.Generic;
using System.Text;

namespace MyOrm.Common
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class ForeignTypeAttribute : System.Attribute
    {
        private Type objectType;
        public ForeignTypeAttribute(Type objectType)
        {
            this.objectType = objectType;
        }

        public Type ObjectType
        {
            get { return objectType; }
        }

        public string FilterProperty { get; set; }

        public object FilterValue { get; set; }
    }
}
