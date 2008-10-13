using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region CustomerCustomerDemoDAO
	/// <summary>
	/// DAO for object 'CustomerCustomerDemo'.
	/// </summary>	
	public class CustomerCustomerDemoDAO : ObjectDAO<CustomerCustomerDemo>, ICustomerCustomerDemoDAO
	{
	}
	#endregion
	
	#region CustomerCustomerDemoViewDAO
	/// <summary>
	/// DAO for object's view 'CustomerCustomerDemoView'.
	/// </summary>	
	public class CustomerCustomerDemoViewDAO : ObjectViewDAO<CustomerCustomerDemoView>, ICustomerCustomerDemoViewDAO
	{
	}
	#endregion	
}
