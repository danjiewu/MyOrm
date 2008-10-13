using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region IOrdersDAO
	/// <summary>
	/// Interface of DAO for object 'Orders'.
	/// </summary>	
	public interface IOrdersDAO : IObjectDAO<Orders>, IObjectDAO
	{		
	}
	#endregion
	
	#region IOrdersViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'OrdersView'.
	/// </summary>	
	public interface IOrdersViewDAO : IObjectViewDAO<OrdersView>, IObjectViewDAO
	{
	}
	#endregion	
}
