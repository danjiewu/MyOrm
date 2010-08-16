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
	public partial class ShippersDAO : ObjectDAO<Shippers> { }
	#endregion

	#region ShippersViewDAO
	/// <summary>
	/// DAO for object's view 'Shippers'.
	/// </summary>	
	public partial class ShippersViewDAO : ObjectViewDAO<Shippers>
	{
	}
	#endregion	
}
