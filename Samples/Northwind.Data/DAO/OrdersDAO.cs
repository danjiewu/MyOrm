using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region OrdersDAO
	/// <summary>
	/// DAO for object 'Orders'.
	/// </summary>	
	public class OrdersDAO : ObjectDAO<Orders>, IOrdersDAO
	{
	}
	#endregion
	
	#region OrdersViewDAO
	/// <summary>
	/// DAO for object's view 'OrdersView'.
	/// </summary>	
	public class OrdersViewDAO : ObjectViewDAO<OrdersView>, IOrdersViewDAO
	{
	}
	#endregion	
}
