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
	public interface ICustomerCustomerDemoDAO : IObjectDAO<CustomerCustomerDemo>, IObjectViewDAO<CustomerCustomerDemo>
	{
		List<CustomerCustomerDemo> GetAllWithCustomerDemographic(CustomerDemographics customerDemographic);
		List<CustomerCustomerDemo> GetAllWithCustomer(Customers customer);
	}
	#endregion
	
	#region ICustomerCustomerDemoViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'CustomerCustomerDemoView'.
	/// </summary>	
	public interface ICustomerCustomerDemoViewDAO : IObjectViewDAO<CustomerCustomerDemoView>
	{
		List<CustomerCustomerDemoView> GetAllWithCustomerDemographic(CustomerDemographics customerDemographic);
		List<CustomerCustomerDemoView> GetAllWithCustomer(Customers customer);
	}
	#endregion	
}
