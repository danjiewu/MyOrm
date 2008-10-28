using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace EE_Veloce
{
    public class PageNavigator : BindingNavigator
    {
        private ToolStripItem _currentPageItem;
        private ToolStripItem _moveFirstPageItem;
        private ToolStripItem _moveLastPageItem;
        private ToolStripItem _moveNextPageItem;
        private ToolStripItem _movePreviousPageItem;
        private string _pageCountFormat;
        private ToolStripItem _pageCountItem;
        private PagedBindingSource _pageSource;

        public override void AddStandardItems()
        {
            MoveFirstPageItem = new ToolStripButton();
            MovePreviousPageItem = new ToolStripButton();
            MoveNextPageItem = new ToolStripButton();
            MoveLastPageItem = new ToolStripButton();
            CurrentPageItem = new ToolStripTextBox();
            PageCountItem = new ToolStripLabel();
            ToolStripSeparator separator = new ToolStripSeparator();
            ToolStripSeparator separator2 = new ToolStripSeparator();
            char ch = string.IsNullOrEmpty(base.Name) || char.IsLower(base.Name[0]) ? 'p' : 'P';
            MoveFirstPageItem.Name = ch + "ageNavigatorMoveFirstPageItem";
            MovePreviousPageItem.Name = ch + "ageNavigatorMovePreviousPageItem";
            MoveNextPageItem.Name = ch + "ageNavigatorMoveNextPageItem";
            MoveLastPageItem.Name = ch + "ageNavigatorMoveLastPageItem";
            CurrentPageItem.Name = ch + "ageNavigatorCurrentPageItem";
            PageCountItem.Name = ch + "ageNavigatorPageCountItem";
            separator.Name = ch + "ageNavigatorSeparator";
            separator2.Name = ch + "ageNavigatorSeparator";
            MoveFirstPageItem.Text = "PageNavigatorMoveFirstPageItemText";
            MovePreviousPageItem.Text = "PageNavigatorMovePreviousPageItemText";
            MoveNextPageItem.Text = "PageNavigatorMoveNextPageItemText";
            MoveLastPageItem.Text = "PageNavigatorMoveLastPageItemText";
            MoveFirstPageItem.ToolTipText = "Move to first page.";
            MovePreviousPageItem.ToolTipText = "Move to previous page.";
            MoveNextPageItem.ToolTipText = "Move to next page.";
            MoveLastPageItem.ToolTipText = "Move to last page.";
            PageCountItem.ToolTipText = "Total page count.";
            CurrentPageItem.ToolTipText = "Current page.";
            PageCountItem.AutoToolTip = false;
            PageCountItem.Text = "/0";
            CurrentPageItem.AutoToolTip = false;
            CurrentPageItem.Text = "0";
            CurrentPageItem.AccessibleName = "PageNavigatorPositionAccessibleName";
            PageCountFormat = "/{0}";
            Bitmap bitmap1 = new Bitmap(typeof(BindingNavigator), "BindingNavigator.MoveFirst.bmp");
            Bitmap bitmap2 = new Bitmap(typeof(BindingNavigator), "BindingNavigator.MovePrevious.bmp");
            Bitmap bitmap3 = new Bitmap(typeof(BindingNavigator), "BindingNavigator.MoveNext.bmp");
            Bitmap bitmap4 = new Bitmap(typeof(BindingNavigator), "BindingNavigator.MoveLast.bmp");
            bitmap1.MakeTransparent(Color.Magenta);
            bitmap2.MakeTransparent(Color.Magenta);
            bitmap3.MakeTransparent(Color.Magenta);
            bitmap4.MakeTransparent(Color.Magenta);
            MoveFirstPageItem.Image = bitmap1;
            MovePreviousPageItem.Image = bitmap2;
            MoveNextPageItem.Image = bitmap3;
            MoveLastPageItem.Image = bitmap4;
            MoveFirstPageItem.RightToLeftAutoMirrorImage = true;
            MovePreviousPageItem.RightToLeftAutoMirrorImage = true;
            MoveNextPageItem.RightToLeftAutoMirrorImage = true;
            MoveLastPageItem.RightToLeftAutoMirrorImage = true;
            MoveFirstPageItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            MovePreviousPageItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            MoveNextPageItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            MoveLastPageItem.DisplayStyle = ToolStripItemDisplayStyle.Image;
            CurrentPageItem.AutoSize = false;
            CurrentPageItem.Width = 50;
            Items.AddRange(new ToolStripItem[] { MoveFirstPageItem, MovePreviousPageItem, separator, CurrentPageItem, PageCountItem, separator2, MoveNextPageItem, MoveLastPageItem });
        }

        private void AcceptNewPage()
        {
            if (CurrentPageItem != null && _pageSource != null)
            {
                int currentPage;
                int.TryParse(CurrentPageItem.Text, out currentPage);
                currentPage--;
                if (currentPage >= 0)
                {
                    int startIndex = currentPage * _pageSource.PageSize;
                    if (((((startIndex != _pageSource.StartIndex) && (startIndex >= 0)) && (startIndex < _pageSource.TotalCount)) ? 1 : 0) != 0)
                    {
                        _pageSource.StartIndex = startIndex;
                        if (!_pageSource.AutoRefresh) _pageSource.RefreshCurrentPage();
                        return;
                    }
                }
                CancelNewPage();
            }
        }

        private void CancelNewPage()
        {
            RefreshPageProperty();
        }

        private void OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) e.IsInputKey = true;
        }

        private void OnCurrentPageKey(object sender, KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode;
            if (keyCode == Keys.Return)
            {
                AcceptNewPage();
                e.Handled = true;
            }
            else if (keyCode == Keys.Escape)
            {
                CancelNewPage();
                e.Handled = true;
            }
        }

        private void OnCurrentPageLostFocus(object sender, EventArgs e)
        {
            AcceptNewPage();
        }

        private void OnMoveFirstPage(object sender, EventArgs e)
        {
            if (Validate() && _pageSource != null)
            {
                _pageSource.StartIndex = 0;
                if (!_pageSource.AutoRefresh) _pageSource.RefreshCurrentPage();
            }
        }

        private void OnMoveLastPage(object sender, EventArgs e)
        {
            if (Validate() && _pageSource != null)
            {
                _pageSource.StartIndex = (_pageSource.TotalCount / _pageSource.PageSize) * _pageSource.PageSize;
                if (!_pageSource.AutoRefresh) _pageSource.RefreshCurrentPage();
            }
        }

        private void OnMoveNextPage(object sender, EventArgs e)
        {
            if (Validate() && _pageSource != null)
            {
                _pageSource.StartIndex += _pageSource.PageSize;
                if (!_pageSource.AutoRefresh) _pageSource.RefreshCurrentPage();
            }
        }

        private void OnMovePreviousPage(object sender, EventArgs e)
        {
            if (Validate() && _pageSource != null)
            {
                _pageSource.StartIndex -= _pageSource.PageSize;
                if (!_pageSource.AutoRefresh) _pageSource.RefreshCurrentPage();
            }
        }


        public void RefreshPageProperty()
        {
            if (_pageSource != null)
            {
                int pageSize = _pageSource.PageSize;
                if (pageSize > 0)
                {
                    int currentPage = _pageSource.StartIndex / pageSize;
                    int totalPage = ((_pageSource.TotalCount + pageSize) - 1) / pageSize;
                    PageCountItem.Text = string.Format(PageCountFormat, totalPage);
                    if (totalPage == 0)
                    {
                        CurrentPageItem.Text = "0";
                    }
                    else
                    {
                        CurrentPageItem.Text = Convert.ToString(currentPage + 1);
                    }
                    if (currentPage <= 0)
                    {
                        MoveFirstPageItem.Enabled = false;
                        MovePreviousPageItem.Enabled = false;
                    }
                    else
                    {
                        MoveFirstPageItem.Enabled = true;
                        MovePreviousPageItem.Enabled = true;
                    }
                    if (currentPage >= (totalPage - 1))
                    {
                        MoveLastPageItem.Enabled = false;
                        MoveNextPageItem.Enabled = false;
                    }
                    else
                    {
                        MoveLastPageItem.Enabled = true;
                        MoveNextPageItem.Enabled = true;
                    }
                }
            }
        }

        private void WireUpButton(ref ToolStripItem oldButton, ToolStripItem newButton, EventHandler clickHandler)
        {
            if (oldButton != newButton)
            {
                if (oldButton != null)
                {
                    oldButton.Click -= clickHandler;
                }
                if (newButton != null)
                {
                    newButton.Click += clickHandler;
                }
                oldButton = newButton;
            }
        }

        private void WireUpTextBox(ref ToolStripItem oldTextBox, ToolStripItem newTextBox, PreviewKeyDownEventHandler previewKeyDownHandle, KeyEventHandler keyUpHandler, EventHandler lostFocusHandler)
        {
            if (oldTextBox != newTextBox)
            {
                ToolStripControlHost host = oldTextBox as ToolStripControlHost;
                ToolStripControlHost host2 = newTextBox as ToolStripControlHost;
                if (host != null)
                {
                    host.Control.PreviewKeyDown -= previewKeyDownHandle;
                    host.KeyUp -= keyUpHandler;
                    host.LostFocus -= lostFocusHandler;
                }
                if (host2 != null)
                {
                    host2.Control.PreviewKeyDown += previewKeyDownHandle;
                    host2.KeyUp += keyUpHandler;
                    host2.LostFocus += lostFocusHandler;
                }
                oldTextBox = newTextBox;
            }
        }

        public ToolStripItem CurrentPageItem
        {
            get
            {
                return _currentPageItem;
            }
            set
            {
                WireUpTextBox(ref _currentPageItem, value, new PreviewKeyDownEventHandler(OnPreviewKeyDown), new KeyEventHandler(OnCurrentPageKey), new EventHandler(OnCurrentPageLostFocus));
            }
        }

        public ToolStripItem MoveFirstPageItem
        {
            get
            {
                return _moveFirstPageItem;
            }
            set
            {
                WireUpButton(ref _moveFirstPageItem, value, new EventHandler(OnMoveFirstPage));
            }
        }

        public ToolStripItem MoveLastPageItem
        {
            get
            {
                return _moveLastPageItem;
            }
            set
            {
                WireUpButton(ref _moveLastPageItem, value, new EventHandler(OnMoveLastPage));
            }
        }

        public ToolStripItem MoveNextPageItem
        {
            get
            {
                return _moveNextPageItem;
            }
            set
            {
                WireUpButton(ref _moveNextPageItem, value, new EventHandler(OnMoveNextPage));
            }
        }

        public ToolStripItem MovePreviousPageItem
        {
            get
            {
                return _movePreviousPageItem;
            }
            set
            {
                WireUpButton(ref _movePreviousPageItem, value, new EventHandler(OnMovePreviousPage));
            }
        }

        public string PageCountFormat
        {
            get
            {
                return _pageCountFormat;
            }
            set
            {
                if (!(_pageCountFormat == value))
                {
                    _pageCountFormat = value;
                }
            }
        }

        public ToolStripItem PageCountItem
        {
            get
            {
                return _pageCountItem;
            }
            set
            {
                _pageCountItem = value;
            }
        }

        public PagedBindingSource PageSource
        {
            get
            {
                return _pageSource;
            }
            set
            {
                if (value != _pageSource)
                {
                    if (_pageSource != null)
                    {
                        _pageSource.PropertyChanged -= new PropertyChangedEventHandler(PageSource_PropertyChanged);
                    }
                    _pageSource = value;
                    RefreshPageProperty();
                    if (_pageSource != null)
                    {
                        _pageSource.PropertyChanged += new PropertyChangedEventHandler(PageSource_PropertyChanged);
                    }
                }
            }
        }

        void PageSource_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            RefreshPageProperty();
        }
    }
}

