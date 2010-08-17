using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region ICustomerCustomerDemoService
	/// <summary>
	/// Service for entity 'CustomerCustomerDemo'.
	/// </summary>	
	public partial interface ICustomerCustomerDemoService : IEntityService<CustomerCustomerDemo>, IEntityViewService<CustomerCustomerDemoView>
	{
        /// <summary>
        /// Get all the CustomerCustomerDemos of the CustomerDemographic.
        /// </summary>
        /// <param name="customerDemographicID">ID of CustomerDemographic</param>
        /// <returns></returns>
		List<CustomerCustomerDemoView> GetAllWithCustomerDemographic(string customerDemographicID);
        /// <summary>
        /// Get all the CustomerCustomerDemos of the Customer.
        /// </summary>
        /// <param name="customerID">ID of Customer</param>
        /// <returns></returns>
		List<CustomerCustomerDemoView> GetAllWithCustomer(string customerID);
	}
	#endregion	
}
