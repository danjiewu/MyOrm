using System;
using System.Data;
using Northwind.Data;
using System.ComponentModel;
using MyOrm.Common;
using System.Collections.Generic;

namespace Northwind
{
    [DataObject]
    public class DataObjectSource<T>
    {
        private IObjectViewDAO<T> objectViewDAO;
        public IObjectViewDAO<T> ObjectViewDAO
        {
            get
            {
                if (objectViewDAO == null) objectViewDAO = (IObjectViewDAO<T>)NorthwindFactory.GetObjectViewDAO(typeof(T));
                return objectViewDAO;
            }
        }

        public List<T> Select(Condition condition, int startRowIndex, int maximumRows, string orderBy)
        {
            bool desc = false;
            if (!String.IsNullOrEmpty(orderBy))
            {
                string[] args = orderBy.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                orderBy = args[0];
                desc = args.Length > 1 && String.Compare(args[1], "desc", true) == 0;
            }
            return ObjectViewDAO.SearchSection(condition, startRowIndex, maximumRows, orderBy, desc);
        }

        public int Count(Condition condition)
        {
            return ObjectViewDAO.Count(condition);
        }
    }

    public class CategoriesSource : DataObjectSource<Categories> { }
    public class CustomerCustomerDemoSource : DataObjectSource<CustomerCustomerDemo> { }
    public class CustomerDemographicsSource : DataObjectSource<CustomerDemographics> { }
    public class CustomersSource : DataObjectSource<Customers> { }
    public class EmployeesSource : DataObjectSource<Employees> { }
    public class EmployeeTerritoriesSource : DataObjectSource<EmployeeTerritories> { }
    public class OrderDetailsSource : DataObjectSource<OrderDetails> { }
    public class OrdersSource : DataObjectSource<Orders> { }
    public class ProductsSource : DataObjectSource<Products> { }
    public class RegionSource : DataObjectSource<Region> { }
    public class ShippersSource : DataObjectSource<Shippers> { }
    public class SuppliersSource : DataObjectSource<Suppliers> { }
    public class TerritoriesSource : DataObjectSource<Territories> { }
    public class CustomerCustomerDemoViewSource : DataObjectSource<CustomerCustomerDemoView> { }
    public class EmployeesViewSource : DataObjectSource<EmployeesView> { }
    public class EmployeeTerritoriesViewSource : DataObjectSource<EmployeeTerritoriesView> { }
    public class OrderDetailsViewSource : DataObjectSource<OrderDetailsView> { }
    public class OrdersViewSource : DataObjectSource<OrdersView> { }
    public class ProductsViewSource : DataObjectSource<ProductsView> { }
    public class TerritoriesViewSource : DataObjectSource<TerritoriesView> { }
}
