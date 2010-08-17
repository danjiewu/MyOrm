using System;
using System.Collections.Generic;
using System.Text;
using Northwind.Data;
using MyOrm.Common;
using System.Reflection;

namespace Northwind.DAO
{
    public class DAOFactory
    {
        #region Fileds
        private CategoriesDAO _CategoriesDAO = new CategoriesDAO();
        private CustomerCustomerDemoDAO _CustomerCustomerDemoDAO = new CustomerCustomerDemoDAO();
        private CustomerDemographicsDAO _CustomerDemographicsDAO = new CustomerDemographicsDAO();
        private CustomersDAO _CustomersDAO = new CustomersDAO();
        private EmployeesDAO _EmployeesDAO = new EmployeesDAO();
        private EmployeeTerritoriesDAO _EmployeeTerritoriesDAO = new EmployeeTerritoriesDAO();
        private OrderDetailsDAO _OrderDetailsDAO = new OrderDetailsDAO();
        private OrdersDAO _OrdersDAO = new OrdersDAO();
        private ProductsDAO _ProductsDAO = new ProductsDAO();
        private RegionDAO _RegionDAO = new RegionDAO();
        private ShippersDAO _ShippersDAO = new ShippersDAO();
        private SuppliersDAO _SuppliersDAO = new SuppliersDAO();
        private TerritoriesDAO _TerritoriesDAO = new TerritoriesDAO();

        private CategoriesViewDAO _CategoriesViewDAO = new CategoriesViewDAO();
        private CustomerCustomerDemoViewDAO _CustomerCustomerDemoViewDAO = new CustomerCustomerDemoViewDAO();
        private CustomerDemographicsViewDAO _CustomerDemographicsViewDAO = new CustomerDemographicsViewDAO();
        private CustomersViewDAO _CustomersViewDAO = new CustomersViewDAO();
        private EmployeesViewDAO _EmployeesViewDAO = new EmployeesViewDAO();
        private EmployeeTerritoriesViewDAO _EmployeeTerritoriesViewDAO = new EmployeeTerritoriesViewDAO();
        private OrderDetailsViewDAO _OrderDetailsViewDAO = new OrderDetailsViewDAO();
        private OrdersViewDAO _OrdersViewDAO = new OrdersViewDAO();
        private ProductsViewDAO _ProductsViewDAO = new ProductsViewDAO();
        private RegionViewDAO _RegionViewDAO = new RegionViewDAO();
        private ShippersViewDAO _ShippersViewDAO = new ShippersViewDAO();
        private SuppliersViewDAO _SuppliersViewDAO = new SuppliersViewDAO();
        private TerritoriesViewDAO _TerritoriesViewDAO = new TerritoriesViewDAO();
        #endregion

        #region DAO
        public CategoriesDAO CategoriesDAO { get { return _CategoriesDAO; } }
        public CustomerCustomerDemoDAO CustomerCustomerDemoDAO { get { return _CustomerCustomerDemoDAO; } }
        public CustomerDemographicsDAO CustomerDemographicsDAO { get { return _CustomerDemographicsDAO; } }
        public CustomersDAO CustomersDAO { get { return _CustomersDAO; } }
        public EmployeesDAO EmployeesDAO { get { return _EmployeesDAO; } }
        public EmployeeTerritoriesDAO EmployeeTerritoriesDAO { get { return _EmployeeTerritoriesDAO; } }
        public OrderDetailsDAO OrderDetailsDAO { get { return _OrderDetailsDAO; } }
        public OrdersDAO OrdersDAO { get { return _OrdersDAO; } }
        public ProductsDAO ProductsDAO { get { return _ProductsDAO; } }
        public RegionDAO RegionDAO { get { return _RegionDAO; } }
        public ShippersDAO ShippersDAO { get { return _ShippersDAO; } }
        public SuppliersDAO SuppliersDAO { get { return _SuppliersDAO; } }
        public TerritoriesDAO TerritoriesDAO { get { return _TerritoriesDAO; } }
        #endregion

        #region ViewDAO
        public CategoriesViewDAO CategoriesViewDAO { get { return _CategoriesViewDAO; } }
        public CustomerCustomerDemoViewDAO CustomerCustomerDemoViewDAO { get { return _CustomerCustomerDemoViewDAO; } }
        public CustomerDemographicsViewDAO CustomerDemographicsViewDAO { get { return _CustomerDemographicsViewDAO; } }
        public CustomersViewDAO CustomersViewDAO { get { return _CustomersViewDAO; } }
        public EmployeesViewDAO EmployeesViewDAO { get { return _EmployeesViewDAO; } }
        public EmployeeTerritoriesViewDAO EmployeeTerritoriesViewDAO { get { return _EmployeeTerritoriesViewDAO; } }
        public OrderDetailsViewDAO OrderDetailsViewDAO { get { return _OrderDetailsViewDAO; } }
        public OrdersViewDAO OrdersViewDAO { get { return _OrdersViewDAO; } }
        public ProductsViewDAO ProductsViewDAO { get { return _ProductsViewDAO; } }
        public RegionViewDAO RegionViewDAO { get { return _RegionViewDAO; } }
        public ShippersViewDAO ShippersViewDAO { get { return _ShippersViewDAO; } }
        public SuppliersViewDAO SuppliersViewDAO { get { return _SuppliersViewDAO; } }
        public TerritoriesViewDAO TerritoriesViewDAO { get { return _TerritoriesViewDAO; } }
        #endregion

        private Dictionary<Type, IObjectDAO> daoCache = new Dictionary<Type, IObjectDAO>();
        private Dictionary<Type, IObjectViewDAO> viewDAOCache = new Dictionary<Type, IObjectViewDAO>();

        public IObjectDAO<T> GetObjectDAO<T>()
        {
            if(daoCache.ContainsKey(typeof(T)))return (IObjectDAO<T>)daoCache[typeof(T)];
            foreach (PropertyInfo property in typeof(DAOFactory).GetProperties())
            {
                if (typeof(IObjectDAO<T>).IsAssignableFrom(property.PropertyType))
                {
                    IObjectDAO<T> dao = (IObjectDAO<T>)property.GetValue(this, null);
                    daoCache[typeof(T)] = dao;
                    return dao;
                }
            }
            daoCache[typeof(T)] = null;
            return null;
        }

        public IObjectDAO GetObjectDAO(Type objectType)
        {
            if (daoCache.ContainsKey(objectType)) return (IObjectDAO)daoCache[objectType];
            Type objectDAOType = typeof(IObjectDAO<>).MakeGenericType(objectType);
            foreach (PropertyInfo property in typeof(DAOFactory).GetProperties())
            {
                if (objectDAOType.IsAssignableFrom(property.PropertyType))
                {
                    IObjectDAO dao = (IObjectDAO)property.GetValue(this, null);
                    daoCache[objectType] = dao;
                    return dao;
                }
            }
            daoCache[objectType] = null;
            return null;
        }
        
        public IObjectViewDAO<T> GetObjectViewDAO<T>() where T : new()
        {
            if (viewDAOCache.ContainsKey(typeof(T))) return (IObjectViewDAO<T>)viewDAOCache[typeof(T)];
            foreach (PropertyInfo property in typeof(DAOFactory).GetProperties())
            {
                if (typeof(IObjectViewDAO<T>).IsAssignableFrom(property.PropertyType))
                {
                    IObjectViewDAO<T> viewDAO = (IObjectViewDAO<T>)property.GetValue(this, null);
                    viewDAOCache[typeof(T)] = viewDAO;
                    return viewDAO;
                }
            }
            viewDAOCache[typeof(T)] = null;
            return null;
        }

        public IObjectViewDAO GetObjectViewDAO(Type objectType)
        {
            if (viewDAOCache.ContainsKey(objectType)) return (IObjectViewDAO)viewDAOCache[objectType];
            Type objectDAOType = typeof(IObjectViewDAO<>).MakeGenericType(objectType);
            foreach (PropertyInfo property in typeof(DAOFactory).GetProperties())
            {
                if (objectDAOType.IsAssignableFrom(property.PropertyType))
                {
                    IObjectViewDAO viewDAO = (IObjectViewDAO)property.GetValue(this, null);
                    viewDAOCache[objectType] = viewDAO;
                    return viewDAO;
                }
            }
            viewDAOCache[objectType] = null;
            return null;
        }
    }
}
