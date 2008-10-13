using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region ICustomerCustomerDemoDAO
	/// <summary>
	/// Interface of DAO for object 'CustomerCustomerDemo'.
	/// </summary>	
	public interface ICustomerCustomerDemoDAO : IObjectDAO<CustomerCustomerDemo>, IObjectDAO
	{
		List<CustomerCustomerDemo> GetAllWithCustomerDemographics(CustomerDemographics customerDemographics);
		List<CustomerCustomerDemo> GetAllWithCustomers(Customers customers);
	}
	#endregion
	
	#region ICustomerCustomerDemoViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'CustomerCustomerDemoView'.
	/// </summary>	
	public interface ICustomerCustomerDemoViewDAO : IObjectViewDAO<CustomerCustomerDemoView>, IObjectViewDAO
	{
		List<CustomerCustomerDemoView> GetAllWithCustomerDemographics(CustomerDemographics customerDemographics);
		List<CustomerCustomerDemoView> GetAllWithCustomers(Customers customers);
	}
	#endregion	
}
