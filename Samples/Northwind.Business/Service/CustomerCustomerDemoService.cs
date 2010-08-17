using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region CustomerCustomerDemoService
	/// <summary>
	/// Service for entity 'CustomerCustomerDemo'.
	/// </summary>	
	public partial class CustomerCustomerDemoService : EntityServiceBase<CustomerCustomerDemo, CustomerCustomerDemoView>, ICustomerCustomerDemoService
	{
        /// <summary>
        /// Get all the CustomerCustomerDemos of the CustomerDemographic.
        /// </summary>
        /// <param name="customerDemographicID">ID of CustomerDemographic</param>
        /// <returns></returns>
		public List<CustomerCustomerDemoView> GetAllWithCustomerDemographic(string customerDemographicID)
		{			
			return ObjectViewDAO.Search(new SimpleCondition(CustomerCustomerDemoView.Properties.CustomerTypeID, customerDemographicID));
		}
		
        /// <summary>
        /// Get all the CustomerCustomerDemos of the Customer.
        /// </summary>
        /// <param name="customerID">ID of Customer</param>
        /// <returns></returns>
		public List<CustomerCustomerDemoView> GetAllWithCustomer(string customerID)
		{			
			return ObjectViewDAO.Search(new SimpleCondition(CustomerCustomerDemoView.Properties.CustomerID, customerID));
		}
		
	}
	#endregion	
}
