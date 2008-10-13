using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region OrdersDAO
	/// <summary>
	/// DAO for object 'Orders'.
	/// </summary>	
	public class OrdersDAO : ObjectDAO<Orders>, IOrdersDAO
	{
		public Orders GetOrders(OrderDetails orderDetails)
		{
			return GetObject(orderDetails.OrderID);
		}
		
		public List<Orders> GetAllWithCustomers(Customers customers)
		{
			return Search(new SimpleCondition(Orders._CustomerID, customers.CustomerID));
		}
		
		public List<Orders> GetAllWithEmployees(Employees employees)
		{
			return Search(new SimpleCondition(Orders._EmployeeID, employees.EmployeeID));
		}
		
		public List<Orders> GetAllWithShippers(Shippers shippers)
		{
			return Search(new SimpleCondition(Orders._ShipVia, shippers.ShipperID));
		}
		
	}
	#endregion
	
	#region OrdersViewDAO
	/// <summary>
	/// DAO for object's view 'OrdersView'.
	/// </summary>	
	public class OrdersViewDAO : ObjectViewDAO<OrdersView>, IOrdersViewDAO
	{
		public OrdersView GetOrders(OrderDetails orderDetails)
		{
			return GetObject(orderDetails.OrderID);
		}
		
		public List<OrdersView> GetAllWithCustomers(Customers customers)
		{
			return Search(new SimpleCondition(OrdersView._CustomerID, customers.CustomerID));
		}
		
		public List<OrdersView> GetAllWithEmployees(Employees employees)
		{
			return Search(new SimpleCondition(OrdersView._EmployeeID, employees.EmployeeID));
		}
		
		public List<OrdersView> GetAllWithShippers(Shippers shippers)
		{
			return Search(new SimpleCondition(OrdersView._ShipVia, shippers.ShipperID));
		}
		
	}
	#endregion	
}
