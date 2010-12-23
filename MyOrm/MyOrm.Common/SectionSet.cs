using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace MyOrm.Common
{
    [Serializable]
    public struct SectionSet
    {
        public int StartIndex { get; set; }
        public int SectionSize { get; set; }
        public Sorting[] Orders { get; set; }
    }

    [Serializable]
    public struct Sorting
    {
        public string PropertyName { get; set; }
        public ListSortDirection Direction { get; set; }
    }
}
