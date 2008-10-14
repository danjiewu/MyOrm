using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region ICustomerDemographicsDAO
	/// <summary>
	/// Interface of DAO for object 'CustomerDemographics'.
	/// </summary>	
	public interface ICustomerDemographicsDAO : IObjectDAO<CustomerDemographics>, IObjectViewDAO<CustomerDemographics>
	{
		CustomerDemographics GetCustomerDemographicOfCustomerCustomerDemo(CustomerCustomerDemo customerCustomerDemo);
	}
	#endregion
}
