using System;
using System.Data;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	public class DAOFactory : IDAOFactory
	{
		private ICategoriesDAO _CategoriesDAO = new CategoriesDAO();
		private ICustomerCustomerDemoDAO _CustomerCustomerDemoDAO = new CustomerCustomerDemoDAO();
		private ICustomerDemographicsDAO _CustomerDemographicsDAO = new CustomerDemographicsDAO();
		private ICustomersDAO _CustomersDAO = new CustomersDAO();
		private IEmployeesDAO _EmployeesDAO = new EmployeesDAO();
		private IEmployeeTerritoriesDAO _EmployeeTerritoriesDAO = new EmployeeTerritoriesDAO();
		private IOrderDetailsDAO _OrderDetailsDAO = new OrderDetailsDAO();
		private IOrdersDAO _OrdersDAO = new OrdersDAO();
		private IProductsDAO _ProductsDAO = new ProductsDAO();
		private IRegionDAO _RegionDAO = new RegionDAO();
		private IShippersDAO _ShippersDAO = new ShippersDAO();
		private ISuppliersDAO _SuppliersDAO = new SuppliersDAO();
		private ITerritoriesDAO _TerritoriesDAO = new TerritoriesDAO();
		
		private ICustomerCustomerDemoViewDAO _CustomerCustomerDemoViewDAO = new CustomerCustomerDemoViewDAO();
		private IEmployeesViewDAO _EmployeesViewDAO = new EmployeesViewDAO();
		private IEmployeeTerritoriesViewDAO _EmployeeTerritoriesViewDAO = new EmployeeTerritoriesViewDAO();
		private IOrderDetailsViewDAO _OrderDetailsViewDAO = new OrderDetailsViewDAO();
		private IOrdersViewDAO _OrdersViewDAO = new OrdersViewDAO();
		private IProductsViewDAO _ProductsViewDAO = new ProductsViewDAO();
		private ITerritoriesViewDAO _TerritoriesViewDAO = new TerritoriesViewDAO();
		
		public ICategoriesDAO CategoriesDAO { get{ return _CategoriesDAO; } }
		public ICustomerCustomerDemoDAO CustomerCustomerDemoDAO { get{ return _CustomerCustomerDemoDAO; } }
		public ICustomerDemographicsDAO CustomerDemographicsDAO { get{ return _CustomerDemographicsDAO; } }
		public ICustomersDAO CustomersDAO { get{ return _CustomersDAO; } }
		public IEmployeesDAO EmployeesDAO { get{ return _EmployeesDAO; } }
		public IEmployeeTerritoriesDAO EmployeeTerritoriesDAO { get{ return _EmployeeTerritoriesDAO; } }
		public IOrderDetailsDAO OrderDetailsDAO { get{ return _OrderDetailsDAO; } }
		public IOrdersDAO OrdersDAO { get{ return _OrdersDAO; } }
		public IProductsDAO ProductsDAO { get{ return _ProductsDAO; } }
		public IRegionDAO RegionDAO { get{ return _RegionDAO; } }
		public IShippersDAO ShippersDAO { get{ return _ShippersDAO; } }
		public ISuppliersDAO SuppliersDAO { get{ return _SuppliersDAO; } }
		public ITerritoriesDAO TerritoriesDAO { get{ return _TerritoriesDAO; } }
		
		public ICustomerCustomerDemoViewDAO CustomerCustomerDemoViewDAO { get { return _CustomerCustomerDemoViewDAO; } }
		public IEmployeesViewDAO EmployeesViewDAO { get { return _EmployeesViewDAO; } }
		public IEmployeeTerritoriesViewDAO EmployeeTerritoriesViewDAO { get { return _EmployeeTerritoriesViewDAO; } }
		public IOrderDetailsViewDAO OrderDetailsViewDAO { get { return _OrderDetailsViewDAO; } }
		public IOrdersViewDAO OrdersViewDAO { get { return _OrdersViewDAO; } }
		public IProductsViewDAO ProductsViewDAO { get { return _ProductsViewDAO; } }
		public ITerritoriesViewDAO TerritoriesViewDAO { get { return _TerritoriesViewDAO; } }
		
		public IObjectDAO GetObjectDAO(Type objectType)
		{
			if(objectType == null) return null;
			else if(objectType == typeof(Categories)) return (IObjectDAO)CategoriesDAO;
			else if(objectType == typeof(CustomerCustomerDemo)) return (IObjectDAO)CustomerCustomerDemoDAO;
			else if(objectType == typeof(CustomerDemographics)) return (IObjectDAO)CustomerDemographicsDAO;
			else if(objectType == typeof(Customers)) return (IObjectDAO)CustomersDAO;
			else if(objectType == typeof(Employees)) return (IObjectDAO)EmployeesDAO;
			else if(objectType == typeof(EmployeeTerritories)) return (IObjectDAO)EmployeeTerritoriesDAO;
			else if(objectType == typeof(OrderDetails)) return (IObjectDAO)OrderDetailsDAO;
			else if(objectType == typeof(Orders)) return (IObjectDAO)OrdersDAO;
			else if(objectType == typeof(Products)) return (IObjectDAO)ProductsDAO;
			else if(objectType == typeof(Region)) return (IObjectDAO)RegionDAO;
			else if(objectType == typeof(Shippers)) return (IObjectDAO)ShippersDAO;
			else if(objectType == typeof(Suppliers)) return (IObjectDAO)SuppliersDAO;
			else if(objectType == typeof(Territories)) return (IObjectDAO)TerritoriesDAO;
			else return GetObjectDAO(objectType.BaseType);
		}
		
		public IObjectViewDAO GetObjectViewDAO(Type objectType)
		{
			if(objectType == null) return null;
			else if(objectType == typeof(Categories)) return (IObjectViewDAO)CategoriesDAO;
			else if(objectType == typeof(CustomerCustomerDemo)) return (IObjectViewDAO)CustomerCustomerDemoDAO;
			else if(objectType == typeof(CustomerCustomerDemoView)) return (IObjectViewDAO)CustomerCustomerDemoViewDAO;			
			else if(objectType == typeof(CustomerDemographics)) return (IObjectViewDAO)CustomerDemographicsDAO;
			else if(objectType == typeof(Customers)) return (IObjectViewDAO)CustomersDAO;
			else if(objectType == typeof(Employees)) return (IObjectViewDAO)EmployeesDAO;
			else if(objectType == typeof(EmployeesView)) return (IObjectViewDAO)EmployeesViewDAO;			
			else if(objectType == typeof(EmployeeTerritories)) return (IObjectViewDAO)EmployeeTerritoriesDAO;
			else if(objectType == typeof(EmployeeTerritoriesView)) return (IObjectViewDAO)EmployeeTerritoriesViewDAO;			
			else if(objectType == typeof(OrderDetails)) return (IObjectViewDAO)OrderDetailsDAO;
			else if(objectType == typeof(OrderDetailsView)) return (IObjectViewDAO)OrderDetailsViewDAO;			
			else if(objectType == typeof(Orders)) return (IObjectViewDAO)OrdersDAO;
			else if(objectType == typeof(OrdersView)) return (IObjectViewDAO)OrdersViewDAO;			
			else if(objectType == typeof(Products)) return (IObjectViewDAO)ProductsDAO;
			else if(objectType == typeof(ProductsView)) return (IObjectViewDAO)ProductsViewDAO;			
			else if(objectType == typeof(Region)) return (IObjectViewDAO)RegionDAO;
			else if(objectType == typeof(Shippers)) return (IObjectViewDAO)ShippersDAO;
			else if(objectType == typeof(Suppliers)) return (IObjectViewDAO)SuppliersDAO;
			else if(objectType == typeof(Territories)) return (IObjectViewDAO)TerritoriesDAO;
			else if(objectType == typeof(TerritoriesView)) return (IObjectViewDAO)TerritoriesViewDAO;			
			else return null;
		}
	}
}
