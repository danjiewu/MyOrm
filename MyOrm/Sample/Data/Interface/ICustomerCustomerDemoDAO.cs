using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region ICustomerCustomerDemoDAO
	/// <summary>
	/// Interface of DAO for object 'CustomerCustomerDemo'.
	/// </summary>	
	public interface ICustomerCustomerDemoDAO : IObjectDAO<CustomerCustomerDemo>, IObjectDAO
	{		
	}
	#endregion
	
	#region ICustomerCustomerDemoViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'CustomerCustomerDemoView'.
	/// </summary>	
	public interface ICustomerCustomerDemoViewDAO : IObjectViewDAO<CustomerCustomerDemoView>, IObjectViewDAO
	{
	}
	#endregion	
}
