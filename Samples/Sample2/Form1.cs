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

namespace Northwind
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
                typeof(CustomerCustomerDemo),
                typeof(CustomerDemographics),
                typeof(Customers),
                typeof(Employees),
                typeof(EmployeeTerritories),
                typeof(OrderDetails),
                typeof(Orders),
                typeof(Products),
                typeof(Northwind.Data.Region),
                typeof(Shippers),
                typeof(Suppliers),
                typeof(Territories),
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
            //MyOrm.Configuration.DefaultConnection.Open();
            Condition condition = null;
            if (!String.IsNullOrEmpty(textBoxValue.Text)) condition = new SimpleCondition(SelectedProperty.Name, ConditionOperator.Contains, textBoxValue.Text);
            dataGridView1.DataSource = NorthwindFactory.DAOFactory.GetObjectViewDAO(SelectedType).Search(condition);
            //MyOrm.Configuration.DefaultConnection.Close();
        }
    }
}
