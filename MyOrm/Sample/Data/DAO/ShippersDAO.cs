using System;
using System.Data;
using MyOrm;

namespace Northwind.Data
{	
	#region ShippersDAO
	/// <summary>
	/// DAO for object 'Shippers'.
	/// </summary>	
	public class ShippersDAO : ObjectDAO<Shippers>, IShippersDAO
	{
	}
	#endregion
}
