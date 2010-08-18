using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Northwind.Business;
using System.Xml.Serialization;
using Northwind.Data;
using System.Reflection;

namespace SampleService
{
    /// <summary>
    /// RemoteService 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://northwind.sample/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [SoapInclude(typeof(List<Categories>))]
    [SoapInclude(typeof(List<CustomerCustomerDemo>))]
    [SoapInclude(typeof(List<CustomerDemographics>))]
    [SoapInclude(typeof(List<Customers>))]
    [SoapInclude(typeof(List<Employees>))]
    [SoapInclude(typeof(List<EmployeeTerritories>))]
    [SoapInclude(typeof(List<OrderDetails>))]
    [SoapInclude(typeof(List<Orders>))]
    [SoapInclude(typeof(List<Products>))]
    [SoapInclude(typeof(List<Region>))]
    [SoapInclude(typeof(List<Shippers>))]
    [SoapInclude(typeof(List<Suppliers>))]
    [SoapInclude(typeof(List<Territories>))]
    public class RemoteService : System.Web.Services.WebService
    {
        [WebMethod]
        public object Test()
        {
            return NorthwindFactory.ServiceFactory.CustomersService.Search(null);
        }

        [WebMethod]
        [SoapInclude(typeof(List<Categories>))]
        [SoapInclude(typeof(List<CustomerCustomerDemo>))]
        [SoapInclude(typeof(List<CustomerDemographics>))]
        [SoapInclude(typeof(List<Customers>))]
        [SoapInclude(typeof(List<Employees>))]
        [SoapInclude(typeof(List<EmployeeTerritories>))]
        [SoapInclude(typeof(List<OrderDetails>))]
        [SoapInclude(typeof(List<Orders>))]
        [SoapInclude(typeof(List<Products>))]
        [SoapInclude(typeof(List<Region>))]
        [SoapInclude(typeof(List<Shippers>))]
        [SoapInclude(typeof(List<Suppliers>))]
        [SoapInclude(typeof(List<Territories>))]
        public object InvokeService(string service, string method, object[] args)
        {
            PropertyInfo property = typeof(IServiceFactory).GetProperty(service);
            Type serviceType = property.PropertyType;
            return serviceType.InvokeMember(method,BindingFlags.Instance| BindingFlags.InvokeMethod| BindingFlags.Public,null, property.GetValue(NorthwindFactory.ServiceFactory, null), args);
        }
    }
}
