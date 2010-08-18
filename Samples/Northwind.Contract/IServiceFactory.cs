using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

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

    public static class ServiceUtil
    {
        private static Dictionary<Type, IEntityService> serviceCache = new Dictionary<Type, IEntityService>();
        private static Dictionary<Type, IEntityViewService> viewServiceCache = new Dictionary<Type, IEntityViewService>();

        public static IEntityService<T> GetEntityService<T>(IServiceFactory serviceFactory)
        {
            if (serviceCache.ContainsKey(typeof(T))) return (IEntityService<T>)serviceCache[typeof(T)];
            foreach (PropertyInfo property in typeof(IServiceFactory).GetProperties())
            {
                if (typeof(IEntityService<T>).IsAssignableFrom(property.PropertyType))
                {
                    IEntityService<T> service = (IEntityService<T>)property.GetValue(serviceFactory, null);
                    serviceCache[typeof(T)] = service;
                    return service;
                }
            }
            serviceCache[typeof(T)] = null;
            return null;
        }

        public static IEntityService GetEntityService(IServiceFactory serviceFactory, Type entityType)
        {
            if (serviceCache.ContainsKey(entityType)) return (IEntityService)serviceCache[entityType];
            Type entityServiceType = typeof(IEntityService<>).MakeGenericType(entityType);
            foreach (PropertyInfo property in typeof(IServiceFactory).GetProperties())
            {
                if (entityServiceType.IsAssignableFrom(property.PropertyType))
                {
                    IEntityService service = (IEntityService)property.GetValue(serviceFactory, null);
                    serviceCache[entityType] = service;
                    return service;
                }
            }
            serviceCache[entityType] = null;
            return null;
        }

        public static IEntityViewService<T> GetEntityViewService<T>(IServiceFactory serviceFactory) where T : new()
        {
            if (viewServiceCache.ContainsKey(typeof(T))) return (IEntityViewService<T>)viewServiceCache[typeof(T)];
            foreach (PropertyInfo property in typeof(IServiceFactory).GetProperties())
            {
                if (typeof(IEntityViewService<T>).IsAssignableFrom(property.PropertyType))
                {
                    IEntityViewService<T> viewService = (IEntityViewService<T>)property.GetValue(serviceFactory, null);
                    viewServiceCache[typeof(T)] = viewService;
                    return viewService;
                }
            }
            viewServiceCache[typeof(T)] = null;
            return null;
        }

        public static IEntityViewService GetEntityViewService(IServiceFactory serviceFactory, Type entityType)
        {
            if (viewServiceCache.ContainsKey(entityType)) return (IEntityViewService)viewServiceCache[entityType];
            Type entityServiceType = typeof(IEntityViewService<>).MakeGenericType(entityType);
            foreach (PropertyInfo property in typeof(IServiceFactory).GetProperties())
            {
                if (entityServiceType.IsAssignableFrom(property.PropertyType))
                {
                    IEntityViewService viewService = (IEntityViewService)property.GetValue(serviceFactory, null);
                    viewServiceCache[entityType] = viewService;
                    return viewService;
                }
            }
            viewServiceCache[entityType] = null;
            return null;
        }
    }
}
