using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region IOrderDetailsDAO
	/// <summary>
	/// Interface of DAO for object 'OrderDetails'.
	/// </summary>	
	public interface IOrderDetailsDAO : IObjectDAO<OrderDetails>, IObjectDAO
	{
		List<OrderDetails> GetAllWithOrders(Orders orders);
		List<OrderDetails> GetAllWithProducts(Products products);
	}
	#endregion
	
	#region IOrderDetailsViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'OrderDetailsView'.
	/// </summary>	
	public interface IOrderDetailsViewDAO : IObjectViewDAO<OrderDetailsView>, IObjectViewDAO
	{
		List<OrderDetailsView> GetAllWithOrders(Orders orders);
		List<OrderDetailsView> GetAllWithProducts(Products products);
	}
	#endregion	
}
