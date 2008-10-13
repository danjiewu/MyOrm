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
	public interface ICustomerDemographicsDAO : IObjectDAO<CustomerDemographics>, IObjectDAO
	{
		CustomerDemographics GetCustomerDemographics(CustomerCustomerDemo customerCustomerDemo);
	}
	#endregion
}
