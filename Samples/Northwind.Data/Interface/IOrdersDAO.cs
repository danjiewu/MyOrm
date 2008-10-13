using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region IOrdersDAO
	/// <summary>
	/// Interface of DAO for object 'Orders'.
	/// </summary>	
	public interface IOrdersDAO : IObjectDAO<Orders>, IObjectDAO
	{
		Orders GetOrders(OrderDetails orderDetails);
		List<Orders> GetAllWithCustomers(Customers customers);
		List<Orders> GetAllWithEmployees(Employees employees);
		List<Orders> GetAllWithShippers(Shippers shippers);
	}
	#endregion
	
	#region IOrdersViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'OrdersView'.
	/// </summary>	
	public interface IOrdersViewDAO : IObjectViewDAO<OrdersView>, IObjectViewDAO
	{
		OrdersView GetOrders(OrderDetails orderDetails);
		List<OrdersView> GetAllWithCustomers(Customers customers);
		List<OrdersView> GetAllWithEmployees(Employees employees);
		List<OrdersView> GetAllWithShippers(Shippers shippers);
	}
	#endregion	
}
