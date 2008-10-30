namespace Northwind
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.textBoxValue = new System.Windows.Forms.TextBox();
            this.buttonSearch = new System.Windows.Forms.Button();
            this.comboBoxProperty = new System.Windows.Forms.ComboBox();
            this.comboBoxObjectType = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.pagedBindingSource1 = new EE_Veloce.PagedBindingSource();
            this.pageNavigator1 = new EE_Veloce.PageNavigator();
            this.pageNavigatorCurrentPageItem = new System.Windows.Forms.ToolStripTextBox();
            this.pageNavigatorMoveFirstPageItem = new System.Windows.Forms.ToolStripButton();
            this.pageNavigatorMovePreviousPageItem = new System.Windows.Forms.ToolStripButton();
            this.pageNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.pageNavigatorPageCountItem = new System.Windows.Forms.ToolStripLabel();
            this.pageNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.pageNavigatorMoveNextPageItem = new System.Windows.Forms.ToolStripButton();
            this.pageNavigatorMoveLastPageItem = new System.Windows.Forms.ToolStripButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pagedBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pageNavigator1)).BeginInit();
            this.pageNavigator1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBoxValue
            // 
            this.textBoxValue.Location = new System.Drawing.Point(314, 3);
            this.textBoxValue.Name = "textBoxValue";
            this.textBoxValue.Size = new System.Drawing.Size(132, 21);
            this.textBoxValue.TabIndex = 3;
            // 
            // buttonSearch
            // 
            this.buttonSearch.Location = new System.Drawing.Point(455, 3);
            this.buttonSearch.Name = "buttonSearch";
            this.buttonSearch.Size = new System.Drawing.Size(75, 23);
            this.buttonSearch.TabIndex = 2;
            this.buttonSearch.Text = "Search";
            this.buttonSearch.UseVisualStyleBackColor = true;
            this.buttonSearch.Click += new System.EventHandler(this.buttonSearch_Click);
            // 
            // comboBoxProperty
            // 
            this.comboBoxProperty.DisplayMember = "Name";
            this.comboBoxProperty.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxProperty.FormattingEnabled = true;
            this.comboBoxProperty.Location = new System.Drawing.Point(156, 3);
            this.comboBoxProperty.Name = "comboBoxProperty";
            this.comboBoxProperty.Size = new System.Drawing.Size(152, 20);
            this.comboBoxProperty.TabIndex = 1;
            // 
            // comboBoxObjectType
            // 
            this.comboBoxObjectType.DisplayMember = "Name";
            this.comboBoxObjectType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBoxObjectType.FormattingEnabled = true;
            this.comboBoxObjectType.Location = new System.Drawing.Point(3, 3);
            this.comboBoxObjectType.Name = "comboBoxObjectType";
            this.comboBoxObjectType.Size = new System.Drawing.Size(147, 20);
            this.comboBoxObjectType.TabIndex = 0;
            this.comboBoxObjectType.SelectedIndexChanged += new System.EventHandler(this.comboBoxObjectType_SelectedIndexChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.dataGridView1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.pageNavigator1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(592, 456);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.buttonSearch);
            this.panel1.Controls.Add(this.textBoxValue);
            this.panel1.Controls.Add(this.comboBoxObjectType);
            this.panel1.Controls.Add(this.comboBoxProperty);
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(533, 29);
            this.panel1.TabIndex = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AutoGenerateColumns = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.DataSource = this.pagedBindingSource1;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 58);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.Size = new System.Drawing.Size(586, 395);
            this.dataGridView1.TabIndex = 2;
            // 
            // pagedBindingSource1
            // 
            this.pagedBindingSource1.PageChanged += new EE_Veloce.PageChangedEventHandler(this.pagedBindingSource1_PageChanged);
            this.pagedBindingSource1.CountNeeded += new EE_Veloce.GetCountEventHandler(this.pagedBindingSource1_CountNeeded);
            // 
            // pageNavigator1
            // 
            this.pageNavigator1.AddNewItem = null;
            this.pageNavigator1.CountItem = null;
            this.pageNavigator1.CurrentPageItem = this.pageNavigatorCurrentPageItem;
            this.pageNavigator1.DeleteItem = null;
            this.pageNavigator1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pageNavigatorMoveFirstPageItem,
            this.pageNavigatorMovePreviousPageItem,
            this.pageNavigatorSeparator,
            this.pageNavigatorCurrentPageItem,
            this.pageNavigatorPageCountItem,
            this.pageNavigatorSeparator1,
            this.pageNavigatorMoveNextPageItem,
            this.pageNavigatorMoveLastPageItem});
            this.pageNavigator1.Location = new System.Drawing.Point(0, 35);
            this.pageNavigator1.MoveFirstItem = null;
            this.pageNavigator1.MoveFirstPageItem = this.pageNavigatorMoveFirstPageItem;
            this.pageNavigator1.MoveLastItem = null;
            this.pageNavigator1.MoveLastPageItem = this.pageNavigatorMoveLastPageItem;
            this.pageNavigator1.MoveNextItem = null;
            this.pageNavigator1.MoveNextPageItem = this.pageNavigatorMoveNextPageItem;
            this.pageNavigator1.MovePreviousItem = null;
            this.pageNavigator1.MovePreviousPageItem = this.pageNavigatorMovePreviousPageItem;
            this.pageNavigator1.Name = "pageNavigator1";
            this.pageNavigator1.PageCountFormat = "/{0}";
            this.pageNavigator1.PageCountItem = this.pageNavigatorPageCountItem;
            this.pageNavigator1.PageSource = this.pagedBindingSource1;
            this.pageNavigator1.PositionItem = null;
            this.pageNavigator1.Size = new System.Drawing.Size(592, 20);
            this.pageNavigator1.TabIndex = 3;
            this.pageNavigator1.Text = "pageNavigator1";
            // 
            // pageNavigatorCurrentPageItem
            // 
            this.pageNavigatorCurrentPageItem.AccessibleName = "PageNavigatorPositionAccessibleName";
            this.pageNavigatorCurrentPageItem.AutoSize = false;
            this.pageNavigatorCurrentPageItem.Name = "pageNavigatorCurrentPageItem";
            this.pageNavigatorCurrentPageItem.Size = new System.Drawing.Size(50, 21);
            this.pageNavigatorCurrentPageItem.Text = "0";
            this.pageNavigatorCurrentPageItem.ToolTipText = "Current page.";
            // 
            // pageNavigatorMoveFirstPageItem
            // 
            this.pageNavigatorMoveFirstPageItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pageNavigatorMoveFirstPageItem.Enabled = false;
            this.pageNavigatorMoveFirstPageItem.Image = ((System.Drawing.Image)(resources.GetObject("pageNavigatorMoveFirstPageItem.Image")));
            this.pageNavigatorMoveFirstPageItem.Name = "pageNavigatorMoveFirstPageItem";
            this.pageNavigatorMoveFirstPageItem.RightToLeftAutoMirrorImage = true;
            this.pageNavigatorMoveFirstPageItem.Size = new System.Drawing.Size(23, 17);
            this.pageNavigatorMoveFirstPageItem.Text = "PageNavigatorMoveFirstPageItemText";
            this.pageNavigatorMoveFirstPageItem.ToolTipText = "Move to first page.";
            // 
            // pageNavigatorMovePreviousPageItem
            // 
            this.pageNavigatorMovePreviousPageItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pageNavigatorMovePreviousPageItem.Enabled = false;
            this.pageNavigatorMovePreviousPageItem.Image = ((System.Drawing.Image)(resources.GetObject("pageNavigatorMovePreviousPageItem.Image")));
            this.pageNavigatorMovePreviousPageItem.Name = "pageNavigatorMovePreviousPageItem";
            this.pageNavigatorMovePreviousPageItem.RightToLeftAutoMirrorImage = true;
            this.pageNavigatorMovePreviousPageItem.Size = new System.Drawing.Size(23, 17);
            this.pageNavigatorMovePreviousPageItem.Text = "PageNavigatorMovePreviousPageItemText";
            this.pageNavigatorMovePreviousPageItem.ToolTipText = "Move to previous page.";
            // 
            // pageNavigatorSeparator
            // 
            this.pageNavigatorSeparator.Name = "pageNavigatorSeparator";
            this.pageNavigatorSeparator.Size = new System.Drawing.Size(6, 20);
            // 
            // pageNavigatorPageCountItem
            // 
            this.pageNavigatorPageCountItem.Name = "pageNavigatorPageCountItem";
            this.pageNavigatorPageCountItem.Size = new System.Drawing.Size(17, 17);
            this.pageNavigatorPageCountItem.Text = "/0";
            this.pageNavigatorPageCountItem.ToolTipText = "Total page count.";
            // 
            // pageNavigatorSeparator1
            // 
            this.pageNavigatorSeparator1.Name = "pageNavigatorSeparator1";
            this.pageNavigatorSeparator1.Size = new System.Drawing.Size(6, 20);
            // 
            // pageNavigatorMoveNextPageItem
            // 
            this.pageNavigatorMoveNextPageItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pageNavigatorMoveNextPageItem.Enabled = false;
            this.pageNavigatorMoveNextPageItem.Image = ((System.Drawing.Image)(resources.GetObject("pageNavigatorMoveNextPageItem.Image")));
            this.pageNavigatorMoveNextPageItem.Name = "pageNavigatorMoveNextPageItem";
            this.pageNavigatorMoveNextPageItem.RightToLeftAutoMirrorImage = true;
            this.pageNavigatorMoveNextPageItem.Size = new System.Drawing.Size(23, 17);
            this.pageNavigatorMoveNextPageItem.Text = "PageNavigatorMoveNextPageItemText";
            this.pageNavigatorMoveNextPageItem.ToolTipText = "Move to next page.";
            // 
            // pageNavigatorMoveLastPageItem
            // 
            this.pageNavigatorMoveLastPageItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.pageNavigatorMoveLastPageItem.Enabled = false;
            this.pageNavigatorMoveLastPageItem.Image = ((System.Drawing.Image)(resources.GetObject("pageNavigatorMoveLastPageItem.Image")));
            this.pageNavigatorMoveLastPageItem.Name = "pageNavigatorMoveLastPageItem";
            this.pageNavigatorMoveLastPageItem.RightToLeftAutoMirrorImage = true;
            this.pageNavigatorMoveLastPageItem.Size = new System.Drawing.Size(23, 17);
            this.pageNavigatorMoveLastPageItem.Text = "PageNavigatorMoveLastPageItemText";
            this.pageNavigatorMoveLastPageItem.ToolTipText = "Move to last page.";
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonSearch;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 456);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pagedBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pageNavigator1)).EndInit();
            this.pageNavigator1.ResumeLayout(false);
            this.pageNavigator1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox comboBoxProperty;
        private System.Windows.Forms.ComboBox comboBoxObjectType;
        private System.Windows.Forms.Button buttonSearch;
        private System.Windows.Forms.TextBox textBoxValue;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private EE_Veloce.PageNavigator pageNavigator1;
        private System.Windows.Forms.ToolStripTextBox pageNavigatorCurrentPageItem;
        private System.Windows.Forms.ToolStripButton pageNavigatorMoveFirstPageItem;
        private System.Windows.Forms.ToolStripButton pageNavigatorMovePreviousPageItem;
        private System.Windows.Forms.ToolStripSeparator pageNavigatorSeparator;
        private System.Windows.Forms.ToolStripLabel pageNavigatorPageCountItem;
        private System.Windows.Forms.ToolStripSeparator pageNavigatorSeparator1;
        private System.Windows.Forms.ToolStripButton pageNavigatorMoveNextPageItem;
        private System.Windows.Forms.ToolStripButton pageNavigatorMoveLastPageItem;
        private EE_Veloce.PagedBindingSource pagedBindingSource1;
    }
}

