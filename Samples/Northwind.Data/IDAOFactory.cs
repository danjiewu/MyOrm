using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	public interface IDAOFactory
	{
		ICategoriesDAO CategoriesDAO { get; }
		ICustomerCustomerDemoDAO CustomerCustomerDemoDAO { get; }
		ICustomerDemographicsDAO CustomerDemographicsDAO { get; }
		ICustomersDAO CustomersDAO { get; }
		IEmployeesDAO EmployeesDAO { get; }
		IEmployeeTerritoriesDAO EmployeeTerritoriesDAO { get; }
		IOrderDetailsDAO OrderDetailsDAO { get; }
		IOrdersDAO OrdersDAO { get; }
		IProductsDAO ProductsDAO { get; }
		IRegionDAO RegionDAO { get; }
		IShippersDAO ShippersDAO { get; }
		ISuppliersDAO SuppliersDAO { get; }
		ITerritoriesDAO TerritoriesDAO { get; }
		
		ICustomerCustomerDemoViewDAO CustomerCustomerDemoViewDAO { get; }
		IEmployeesViewDAO EmployeesViewDAO { get; }
		IEmployeeTerritoriesViewDAO EmployeeTerritoriesViewDAO { get; }
		IOrderDetailsViewDAO OrderDetailsViewDAO { get; }
		IOrdersViewDAO OrdersViewDAO { get; }
		IProductsViewDAO ProductsViewDAO { get; }
		ITerritoriesViewDAO TerritoriesViewDAO { get; }
		
		IObjectDAO GetObjectDAO(Type objectType);
		IObjectViewDAO GetObjectViewDAO(Type objectType);
	}
}
