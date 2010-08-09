using System;
using System.Collections.Generic;
using System.Text;

namespace Liquid.Common
{
    public interface IIndexedProperty
    {
        object this[string propertyName] { get; set; }
    }
}
