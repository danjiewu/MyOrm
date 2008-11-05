using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region IShippersDAO
	/// <summary>
	/// Interface of DAO for object 'Shippers'.
	/// </summary>	
	public partial interface IShippersDAO : IObjectDAO<Shippers>, IObjectViewDAO<Shippers>
	{
		Shippers GetShipperOfOrder(Orders orders);
	}
	#endregion
}
