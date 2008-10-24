using System;
using System.Data;
using Northwind.Data;
using System.ComponentModel;
using MyOrm.Common;
using System.Collections.Generic;

namespace Northwind
{
    [DataObject]
    public class ObjectSource<T>
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

        [DataObjectMethod(DataObjectMethodType.Select, true)]
        public List<T> Select(Condition condition)
        {
            return ObjectViewDAO.Search(condition);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<T> Select(Condition condition, int startRowIndex, int maximumRows)
        {
            return ObjectViewDAO.SearchSection(condition, startRowIndex, maximumRows, null, ListSortDirection.Ascending);
        }

        [DataObjectMethod(DataObjectMethodType.Select, false)]
        public List<T> Select(Condition condition, int startRowIndex, int maximumRows, string orderBy)
        {
            ListSortDirection direction = ListSortDirection.Ascending;
            if (!String.IsNullOrEmpty(orderBy))
            {
                string[] args = orderBy.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                orderBy = args[0];
                direction = args.Length > 1 && String.Compare(args[1], "desc", true) == 0 ? ListSortDirection.Descending : ListSortDirection.Ascending;
            }
            return ObjectViewDAO.SearchSection(condition, startRowIndex, maximumRows, orderBy, direction);
        }

        public int Count(Condition condition)
        {
            return ObjectViewDAO.Count(condition);
        }
    }

    public class CategoriesSource : ObjectSource<Categories> { }
    public class CustomerCustomerDemoSource : ObjectSource<CustomerCustomerDemo> { }
    public class CustomerDemographicsSource : ObjectSource<CustomerDemographics> { }
    public class CustomersSource : ObjectSource<Customers> { }
    public class EmployeesSource : ObjectSource<Employees> { }
    public class EmployeeTerritoriesSource : ObjectSource<EmployeeTerritories> { }
    public class OrderDetailsSource : ObjectSource<OrderDetails> { }
    public class OrdersSource : ObjectSource<Orders> { }
    public class ProductsSource : ObjectSource<Products> { }
    public class RegionSource : ObjectSource<Region> { }
    public class ShippersSource : ObjectSource<Shippers> { }
    public class SuppliersSource : ObjectSource<Suppliers> { }
    public class TerritoriesSource : ObjectSource<Territories> { }
    public class CustomerCustomerDemoViewSource : ObjectSource<CustomerCustomerDemoView> { }
    public class EmployeesViewSource : ObjectSource<EmployeesView> { }
    public class EmployeeTerritoriesViewSource : ObjectSource<EmployeeTerritoriesView> { }
    public class OrderDetailsViewSource : ObjectSource<OrderDetailsView> { }
    public class OrdersViewSource : ObjectSource<OrdersView> { }
    public class ProductsViewSource : ObjectSource<ProductsView> { }
    public class TerritoriesViewSource : ObjectSource<TerritoriesView> { }
}
