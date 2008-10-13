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
		public List<CustomerCustomerDemo> GetAllWithCustomerDemographics(CustomerDemographics customerDemographics)
		{
			return Search(new SimpleCondition(CustomerCustomerDemo._CustomerTypeID, customerDemographics.CustomerTypeID));
		}
		
		public List<CustomerCustomerDemo> GetAllWithCustomers(Customers customers)
		{
			return Search(new SimpleCondition(CustomerCustomerDemo._CustomerID, customers.CustomerID));
		}
		
	}
	#endregion
	
	#region CustomerCustomerDemoViewDAO
	/// <summary>
	/// DAO for object's view 'CustomerCustomerDemoView'.
	/// </summary>	
	public class CustomerCustomerDemoViewDAO : ObjectViewDAO<CustomerCustomerDemoView>, ICustomerCustomerDemoViewDAO
	{
		public List<CustomerCustomerDemoView> GetAllWithCustomerDemographics(CustomerDemographics customerDemographics)
		{
			return Search(new SimpleCondition(CustomerCustomerDemoView._CustomerTypeID, customerDemographics.CustomerTypeID));
		}
		
		public List<CustomerCustomerDemoView> GetAllWithCustomers(Customers customers)
		{
			return Search(new SimpleCondition(CustomerCustomerDemoView._CustomerID, customers.CustomerID));
		}
		
	}
	#endregion	
}
