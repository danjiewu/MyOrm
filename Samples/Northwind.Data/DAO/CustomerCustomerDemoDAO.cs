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
	public class CustomerCustomerDemoDAO : ObjectDAO<CustomerCustomerDemo>, ICustomerCustomerDemoDAO
	{
		public List<CustomerCustomerDemo> GetAllWithCustomerDemographic(CustomerDemographics customerDemographic)
		{
			return Search(new SimpleCondition(CustomerCustomerDemo._CustomerTypeID, customerDemographic.CustomerTypeID));
		}
		
		public List<CustomerCustomerDemo> GetAllWithCustomer(Customers customer)
		{
			return Search(new SimpleCondition(CustomerCustomerDemo._CustomerID, customer.CustomerID));
		}
		
	}
	#endregion
	
	#region CustomerCustomerDemoViewDAO
	/// <summary>
	/// DAO for object's view 'CustomerCustomerDemoView'.
	/// </summary>	
	public class CustomerCustomerDemoViewDAO : ObjectViewDAO<CustomerCustomerDemoView>, ICustomerCustomerDemoViewDAO
	{
		public List<CustomerCustomerDemoView> GetAllWithCustomerDemographic(CustomerDemographics customerDemographic)
		{
			return Search(new SimpleCondition(CustomerCustomerDemoView._CustomerTypeID, customerDemographic.CustomerTypeID));
		}
		
		public List<CustomerCustomerDemoView> GetAllWithCustomer(Customers customer)
		{
			return Search(new SimpleCondition(CustomerCustomerDemoView._CustomerID, customer.CustomerID));
		}
		
	}
	#endregion	
}
