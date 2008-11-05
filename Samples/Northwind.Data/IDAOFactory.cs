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
	}
	
	public static class DAOFactoryUtil{				
		public static IObjectDAO GetObjectDAO(IDAOFactory factory, Type objectType)
		{
			if(objectType == null) return null;
			else if(objectType == typeof(Categories)) return (IObjectDAO)factory.CategoriesDAO;
			else if(objectType == typeof(CustomerCustomerDemo)) return (IObjectDAO)factory.CustomerCustomerDemoDAO;
			else if(objectType == typeof(CustomerDemographics)) return (IObjectDAO)factory.CustomerDemographicsDAO;
			else if(objectType == typeof(Customers)) return (IObjectDAO)factory.CustomersDAO;
			else if(objectType == typeof(Employees)) return (IObjectDAO)factory.EmployeesDAO;
			else if(objectType == typeof(EmployeeTerritories)) return (IObjectDAO)factory.EmployeeTerritoriesDAO;
			else if(objectType == typeof(OrderDetails)) return (IObjectDAO)factory.OrderDetailsDAO;
			else if(objectType == typeof(Orders)) return (IObjectDAO)factory.OrdersDAO;
			else if(objectType == typeof(Products)) return (IObjectDAO)factory.ProductsDAO;
			else if(objectType == typeof(Region)) return (IObjectDAO)factory.RegionDAO;
			else if(objectType == typeof(Shippers)) return (IObjectDAO)factory.ShippersDAO;
			else if(objectType == typeof(Suppliers)) return (IObjectDAO)factory.SuppliersDAO;
			else if(objectType == typeof(Territories)) return (IObjectDAO)factory.TerritoriesDAO;
			else return GetObjectDAO(factory, objectType.BaseType);
		}
		
		public static IObjectViewDAO GetObjectViewDAO(IDAOFactory factory, Type objectType)
		{
			if(objectType == null) return null;
			else if(objectType == typeof(Categories)) return (IObjectViewDAO)factory.CategoriesDAO;
			else if(objectType == typeof(CustomerCustomerDemo)) return (IObjectViewDAO)factory.CustomerCustomerDemoDAO;
			else if(objectType == typeof(CustomerCustomerDemoView)) return (IObjectViewDAO)factory.CustomerCustomerDemoViewDAO;			
			else if(objectType == typeof(CustomerDemographics)) return (IObjectViewDAO)factory.CustomerDemographicsDAO;
			else if(objectType == typeof(Customers)) return (IObjectViewDAO)factory.CustomersDAO;
			else if(objectType == typeof(Employees)) return (IObjectViewDAO)factory.EmployeesDAO;
			else if(objectType == typeof(EmployeesView)) return (IObjectViewDAO)factory.EmployeesViewDAO;			
			else if(objectType == typeof(EmployeeTerritories)) return (IObjectViewDAO)factory.EmployeeTerritoriesDAO;
			else if(objectType == typeof(EmployeeTerritoriesView)) return (IObjectViewDAO)factory.EmployeeTerritoriesViewDAO;			
			else if(objectType == typeof(OrderDetails)) return (IObjectViewDAO)factory.OrderDetailsDAO;
			else if(objectType == typeof(OrderDetailsView)) return (IObjectViewDAO)factory.OrderDetailsViewDAO;			
			else if(objectType == typeof(Orders)) return (IObjectViewDAO)factory.OrdersDAO;
			else if(objectType == typeof(OrdersView)) return (IObjectViewDAO)factory.OrdersViewDAO;			
			else if(objectType == typeof(Products)) return (IObjectViewDAO)factory.ProductsDAO;
			else if(objectType == typeof(ProductsView)) return (IObjectViewDAO)factory.ProductsViewDAO;			
			else if(objectType == typeof(Region)) return (IObjectViewDAO)factory.RegionDAO;
			else if(objectType == typeof(Shippers)) return (IObjectViewDAO)factory.ShippersDAO;
			else if(objectType == typeof(Suppliers)) return (IObjectViewDAO)factory.SuppliersDAO;
			else if(objectType == typeof(Territories)) return (IObjectViewDAO)factory.TerritoriesDAO;
			else if(objectType == typeof(TerritoriesView)) return (IObjectViewDAO)factory.TerritoriesViewDAO;			
			else return null;
		}
	}
}
