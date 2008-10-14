using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region CustomerDemographicsDAO
	/// <summary>
	/// DAO for object 'CustomerDemographics'.
	/// </summary>	
	public class CustomerDemographicsDAO : ObjectDAO<CustomerDemographics>, ICustomerDemographicsDAO
	{
		public CustomerDemographics GetCustomerDemographicOfCustomerCustomerDemo(CustomerCustomerDemo customerCustomerDemo)
		{
			return GetObject(customerCustomerDemo.CustomerTypeID);
		}
		
	}
	#endregion
}
