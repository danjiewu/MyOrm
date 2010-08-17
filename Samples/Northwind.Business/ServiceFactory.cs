using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Business
{
    public class ServiceFactory : IServiceFactory
    {
        #region Fileds
        private ICategoriesService _CategoriesService = new CategoriesService();
        private ICustomerCustomerDemoService _CustomerCustomerDemoService = new CustomerCustomerDemoService();
        private ICustomerDemographicsService _CustomerDemographicsService = new CustomerDemographicsService();
        private ICustomersService _CustomersService = new CustomersService();
        private IEmployeesService _EmployeesService = new EmployeesService();
        private IEmployeeTerritoriesService _EmployeeTerritoriesService = new EmployeeTerritoriesService();
        private IOrderDetailsService _OrderDetailsService = new OrderDetailsService();
        private IOrdersService _OrdersService = new OrdersService();
        private IProductsService _ProductsService = new ProductsService();
        private IRegionService _RegionService = new RegionService();
        private IShippersService _ShippersService = new ShippersService();
        private ISuppliersService _SuppliersService = new SuppliersService();
        private ITerritoriesService _TerritoriesService = new TerritoriesService();
        #endregion

        #region Services
        public ICategoriesService CategoriesService { get { return _CategoriesService; } }
        public ICustomerCustomerDemoService CustomerCustomerDemoService { get { return _CustomerCustomerDemoService; } }
        public ICustomerDemographicsService CustomerDemographicsService { get { return _CustomerDemographicsService; } }
        public ICustomersService CustomersService { get { return _CustomersService; } }
        public IEmployeesService EmployeesService { get { return _EmployeesService; } }
        public IEmployeeTerritoriesService EmployeeTerritoriesService { get { return _EmployeeTerritoriesService; } }
        public IOrderDetailsService OrderDetailsService { get { return _OrderDetailsService; } }
        public IOrdersService OrdersService { get { return _OrdersService; } }
        public IProductsService ProductsService { get { return _ProductsService; } }
        public IRegionService RegionService { get { return _RegionService; } }
        public IShippersService ShippersService { get { return _ShippersService; } }
        public ISuppliersService SuppliersService { get { return _SuppliersService; } }
        public ITerritoriesService TerritoriesService { get { return _TerritoriesService; } }
        #endregion
    }
}
