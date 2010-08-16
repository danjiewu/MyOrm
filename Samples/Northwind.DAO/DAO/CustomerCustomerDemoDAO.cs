using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region CustomerCustomerDemoDAO
	/// <summary>
	/// DAO for object 'CustomerCustomerDemo'.
	/// </summary>	
	public partial class CustomerCustomerDemoDAO : ObjectDAO<CustomerCustomerDemo> { }
	#endregion

	#region CustomerCustomerDemoViewDAO
	/// <summary>
	/// DAO for object's view 'CustomerCustomerDemoView'.
	/// </summary>	
	public partial class CustomerCustomerDemoViewDAO : ObjectViewDAO<CustomerCustomerDemoView>
	{
        /// <summary>
        /// Get all the CustomerCustomerDemos of the CustomerDemographic.
        /// </summary>
        /// <param name="customerDemographicID">ID of CustomerDemographic</param>
        /// <returns></returns>
		public List<CustomerCustomerDemoView> GetAllWithCustomerDemographic(string customerDemographicID)
		{			
			return Search(new SimpleCondition(CustomerCustomerDemoView.Properties.CustomerTypeID, customerDemographicID));
		}
		
        /// <summary>
        /// Get all the CustomerCustomerDemos of the Customer.
        /// </summary>
        /// <param name="customerID">ID of Customer</param>
        /// <returns></returns>
		public List<CustomerCustomerDemoView> GetAllWithCustomer(string customerID)
		{			
			return Search(new SimpleCondition(CustomerCustomerDemoView.Properties.CustomerID, customerID));
		}
		
	}
	#endregion	
}
