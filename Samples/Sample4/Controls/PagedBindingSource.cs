using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.ComponentModel;

namespace EE_Veloce
{
    public abstract class PagedBindingSource : BindingSource, INotifyPropertyChanged
    {
        protected const int DefaultPageSize = 20;
        private bool _autoRefresh = true;
        private int _totalCount;
        private int _startIndex;
        private int _pageSize = DefaultPageSize;
        //private PropertyDescriptor _sortProperty;
        //private ListSortDirection _sortDirection;

        //public override bool SupportsSorting
        //{
        //    get { return true; }
        //}

        //public override void ApplySort(PropertyDescriptor property, ListSortDirection sort)
        //{
        //    if (property == _sortProperty) sort = _sortDirection ^ ListSortDirection.Descending;
        //    _sortProperty = property;
        //    _sortDirection = sort;
        //    RefreshCurrentPage();
        //}

        //public override void RemoveSort()
        //{
        //    _sortProperty = null;
        //    RefreshCurrentPage();
        //}

        //public override bool IsSorted
        //{
        //    get { return _sortProperty != null; }
        //}

        //public override PropertyDescriptor SortProperty
        //{
        //    get { return _sortProperty; }
        //}

        //public override ListSortDirection SortDirection
        //{
        //    get { return _sortDirection; }
        //}

        protected abstract object GetDataSource(int startIndex, int pageSize, PropertyDescriptor orderby, ListSortDirection direction);
        protected abstract int GetTotalCount();

        [DefaultValue(true)]
        public bool AutoRefresh
        {
            get { return _autoRefresh; }
            set { _autoRefresh = value; }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int TotalCount
        {
            get { return _totalCount; }
        }

        [DefaultValue(DefaultPageSize)]
        public int PageSize
        {
            get { return _pageSize; }
            set
            {
                if (_pageSize != value)
                {
                    _pageSize = value;
                    OnPropertyChanged("PageSize");
                    if (AutoRefresh) RefreshCurrentPage();
                }
            }
        }

        [DefaultValue(0)]
        public int StartIndex
        {
            get { return _startIndex; }
            set
            {
                if (_startIndex != value)
                {
                    _startIndex = value;
                    OnPropertyChanged("StartIndex");
                    if (AutoRefresh) RefreshCurrentPage();
                }
            }
        }

        public override void Clear()
        {
            _totalCount = 0;
            _startIndex = 0;           
            OnPropertyChanged(null);
            DataSource = null;
        }

        public virtual void RefreshSource()
        {
            _totalCount = GetTotalCount();
            _startIndex = 0;
            OnPropertyChanged(null);
            RefreshCurrentPage();
        }

        public virtual void RefreshCurrentPage()
        {
            DataSource = GetDataSource(StartIndex, PageSize, SortProperty, SortDirection);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
