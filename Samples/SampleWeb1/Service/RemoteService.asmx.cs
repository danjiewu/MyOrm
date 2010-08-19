using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Northwind.Business;
using System.Xml.Serialization;
using Northwind.Data;
using System.Reflection;
using MyOrm.Common;

namespace SampleService
{
    /// <summary>
    /// RemoteService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://northwind.sample/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [XmlInclude(typeof(SimpleCondition))]
    [XmlInclude(typeof(ConditionSet))]
    [XmlInclude(typeof(ForeignCondition))]
    [XmlInclude(typeof(List<Categories>))]
    [XmlInclude(typeof(List<CustomerCustomerDemoView>))]
    [XmlInclude(typeof(List<CustomerDemographics>))]
    [XmlInclude(typeof(List<Customers>))]
    [XmlInclude(typeof(List<EmployeesView>))]
    [XmlInclude(typeof(List<EmployeeTerritoriesView>))]
    [XmlInclude(typeof(List<OrderDetailsView>))]
    [XmlInclude(typeof(List<OrdersView>))]
    [XmlInclude(typeof(List<ProductsView>))]
    [XmlInclude(typeof(List<Region>))]
    [XmlInclude(typeof(List<Shippers>))]
    [XmlInclude(typeof(List<Suppliers>))]
    [XmlInclude(typeof(List<TerritoriesView>))]
    public class RemoteService : System.Web.Services.WebService
    {
        [WebMethod]
        public object Test()
        {
            return NorthwindFactory.ServiceFactory.CustomersService.Search(null);
        }

        [WebMethod]
        public object InvokeService(string serviceName, string methodName, object[] args)
        {
            PropertyInfo property = typeof(IServiceFactory).GetProperty(serviceName);
            if (property == null) throw new ArgumentException(String.Format("Service \"{0}\" does not exist", serviceName), "serviceName");
            object service = property.GetValue(NorthwindFactory.ServiceFactory, null);
            if (service == null) throw new ArgumentException(String.Format("Service \"{0}\" is not available", serviceName), "serviceName");
            Type serviceType = property.PropertyType;
            MethodInfo method = FindMethod(serviceType, methodName, args);
            if (method == null) throw new ArgumentException(String.Format("Service \"{0}\" does not have method \"{1}\"", serviceName, methodName), "methodName");
            return method.Invoke(service, args);
        }

        private MethodInfo FindMethod(Type type, string methodName, object[] args)
        {
            MethodInfo method = type.GetMethod(methodName);
            if (method != null && method.GetParameters().Length == args.Length) return method;
            foreach (Type interfaceType in type.GetInterfaces())
            {
                method = FindMethod(interfaceType, methodName, args);
                if (method != null) return method;
            }
            return null;
        }
    }
}
