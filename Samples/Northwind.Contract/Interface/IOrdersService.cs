using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region IOrdersService
	/// <summary>
	/// Service for entity 'Orders'.
	/// </summary>	
	public partial interface IOrdersService : IEntityService<Orders>, IEntityViewService<OrdersView>
	{
        /// <summary>
        /// Get all the Orderses by CustomerID.
        /// </summary>
        /// <param name="customerID">CustomerID of the Orders</param>
        /// <returns></returns>
        List<OrdersView> GetAllByCustomerID(string customerID);
        /// <summary>
        /// Get all the Orderses by EmployeeID.
        /// </summary>
        /// <param name="employeeID">EmployeeID of the Orders</param>
        /// <returns></returns>
        List<OrdersView> GetAllByEmployeeID(int employeeID);
        /// <summary>
        /// Get all the Orderses by OrderDate.
        /// </summary>
        /// <param name="orderDate">OrderDate of the Orders</param>
        /// <returns></returns>
        List<OrdersView> GetAllByOrderDate(DateTime orderDate);
        /// <summary>
        /// Get all the Orderses by ShippedDate.
        /// </summary>
        /// <param name="shippedDate">ShippedDate of the Orders</param>
        /// <returns></returns>
        List<OrdersView> GetAllByShippedDate(DateTime shippedDate);
        /// <summary>
        /// Get all the Orderses by ShipVia.
        /// </summary>
        /// <param name="shipVia">ShipVia of the Orders</param>
        /// <returns></returns>
        List<OrdersView> GetAllByShipVia(int shipVia);
        /// <summary>
        /// Get all the Orderses by ShipPostalCode.
        /// </summary>
        /// <param name="shipPostalCode">ShipPostalCode of the Orders</param>
        /// <returns></returns>
        List<OrdersView> GetAllByShipPostalCode(string shipPostalCode);
        /// <summary>
        /// Get all the Orderses of the Customer.
        /// </summary>
        /// <param name="customerID">ID of Customer</param>
        /// <returns></returns>
		List<OrdersView> GetAllWithCustomer(string customerID);
        /// <summary>
        /// Get all the Orderses of the Employee.
        /// </summary>
        /// <param name="employeeID">ID of Employee</param>
        /// <returns></returns>
		List<OrdersView> GetAllWithEmployee(int employeeID);
        /// <summary>
        /// Get all the Orderses of the Shipper.
        /// </summary>
        /// <param name="shipperID">ID of Shipper</param>
        /// <returns></returns>
		List<OrdersView> GetAllWithShipper(int shipperID);
	}
	#endregion	
}
