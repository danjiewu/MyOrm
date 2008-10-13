using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region IOrderDetailsDAO
	/// <summary>
	/// Interface of DAO for object 'OrderDetails'.
	/// </summary>	
	public interface IOrderDetailsDAO : IObjectDAO<OrderDetails>, IObjectDAO
	{		
	}
	#endregion
	
	#region IOrderDetailsViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'OrderDetailsView'.
	/// </summary>	
	public interface IOrderDetailsViewDAO : IObjectViewDAO<OrderDetailsView>, IObjectViewDAO
	{
	}
	#endregion	
}
