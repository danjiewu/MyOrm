using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region ShippersDAO
	/// <summary>
	/// DAO for object 'Shippers'.
	/// </summary>	
	public class ShippersDAO : ObjectDAO<Shippers>, IShippersDAO
	{
		public Shippers GetShipperOfOrder(Orders orders)
		{
			return GetObject(orders.ShipVia);
		}
		
	}
	#endregion
}
