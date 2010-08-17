using System;
using System.Collections.Generic;
using System.Text;

namespace Northwind.Business
{
    public interface IServiceFactory
    {
        ICategoriesService CategoriesService { get; }
        ICustomerCustomerDemoService CustomerCustomerDemoService { get; }
        ICustomerDemographicsService CustomerDemographicsService { get; }
        ICustomersService CustomersService { get; }
        IEmployeesService EmployeesService { get; }
        IEmployeeTerritoriesService EmployeeTerritoriesService { get; }
        IOrderDetailsService OrderDetailsService { get; }
        IOrdersService OrdersService { get; }
        IProductsService ProductsService { get; }
        IRegionService RegionService { get; }
        IShippersService ShippersService { get; }
        ISuppliersService SuppliersService { get; }
        ITerritoriesService TerritoriesService { get; }
    }
}
