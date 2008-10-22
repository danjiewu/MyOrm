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

	}
}
