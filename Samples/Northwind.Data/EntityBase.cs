using System;
using System.Collections.Generic;
using System.Text;
using MyOrm.Common;
using System.Reflection;

namespace Northwind.Data
{
    public class EntityBase : ICloneable, ICopyable, IIndexedProperty
    {
        #region ICloneable 成员

        public virtual object Clone()
        {
            return MemberwiseClone();
        }

        #endregion

        #region ICopyable 成员

        public virtual void CopyFrom(object o)
        {
            if (o == null || !this.GetType().IsInstanceOfType(o))
                return;

            foreach (PropertyInfo property in this.GetType().GetProperties())
            {
                if (property.GetIndexParameters().Length == 0 && property.CanRead && property.CanWrite)
                    property.SetValue(this, property.GetValue(o, null), null);
            }
        }

        #endregion    

        #region IIndexedProperty 成员

        public virtual object this[string propertyName]
        {
            get
            {
                throw new ArgumentException(String.Format("Property {0} not exist.", propertyName), "propertyName");
            }
            set
            {
                throw new ArgumentException(String.Format("Property {0} not exist.", propertyName), "propertyName");
            }
        }

        #endregion
    }

    public interface ICopyable
    {
        void CopyFrom(object o);
    }
}
