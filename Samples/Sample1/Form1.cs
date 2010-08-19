using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using Northwind.Data;
using MyOrm.Common;
using System.Reflection;
using Northwind.Business;

namespace Northwind.Windows
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBoxObjectType.DataSource =
                new Type[]{
                typeof(Categories),
                typeof(CustomerDemographics),
                typeof(Customers),
                typeof(Northwind.Data.Region),
                typeof(Shippers),
                typeof(Suppliers),
                typeof(TerritoriesView),
                typeof(CustomerCustomerDemoView),
                typeof(EmployeesView),
                typeof(EmployeeTerritoriesView),
                typeof(OrderDetailsView),
                typeof(OrdersView),
                typeof(ProductsView),
                typeof(TerritoriesView)
            };
        }

        private Type SelectedType
        {
            get { return (Type)comboBoxObjectType.SelectedItem; }
        }

        private PropertyInfo SelectedProperty
        {
            get { return (PropertyInfo)comboBoxProperty.SelectedItem; }
        }

        private void comboBoxObjectType_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBoxProperty.DataSource = SelectedType.GetProperties();
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Condition condition = null;
            if (!String.IsNullOrEmpty(textBoxValue.Text)) condition = new SimpleCondition(SelectedProperty.Name,textBoxValue.Text);
            dataGridView1.DataSource = ServiceUtil.GetEntityViewService(NorthwindFactory.ServiceFactory, SelectedType).Search(condition);
        }
    }
}
