using System;
using System.ComponentModel;
using System.Diagnostics;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind
{
    public class ConditionPagedSource : PagedBindingSource
    {
        private IObjectViewDAO _objectViewDAO;
        private Condition _condition;
        private Type _objectType;

        protected override int GetTotalCount()
        {
            return ObjectViewDAO.Count(Condition);
        }

        protected override object GetDataSource(int startIndex, int pageSize, PropertyDescriptor orderby, ListSortDirection direction)
        {
            return ObjectViewDAO.SearchSection(Condition, startIndex, pageSize, orderby == null ? null : orderby.Name, direction);
        }

        [DefaultValue(null), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Condition Condition
        {
            get
            {
                return _condition;
            }
            set
            {
                _condition = value;
                RefreshSource();
            }
        }

        [DefaultValue(null), TypeConverter(typeof(TypeTypeConverter)), RefreshProperties(RefreshProperties.Repaint)]
        public Type ObjectType
        {
            get { return _objectType; }
            set
            {
                if (_objectType != value)
                {
                    _objectType = value;
                    DataSource = value;
                }
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IObjectViewDAO ObjectViewDAO
        {
            get { return _objectViewDAO == null ? NorthwindFactory.GetObjectViewDAO(ObjectType) : _objectViewDAO; }
            set { _objectViewDAO = value; }
        }
    }

    public class TypeTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return destinationType == typeof(string) || base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string) return Type.GetType((string)value);
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string)) return ((Type)value).FullName;
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}

