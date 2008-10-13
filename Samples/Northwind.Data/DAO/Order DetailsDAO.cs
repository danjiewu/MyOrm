using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region OrderDetailsDAO
	/// <summary>
	/// DAO for object 'OrderDetails'.
	/// </summary>	
	public class OrderDetailsDAO : ObjectDAO<OrderDetails>, IOrderDetailsDAO
	{
	}
	#endregion
	
	#region OrderDetailsViewDAO
	/// <summary>
	/// DAO for object's view 'OrderDetailsView'.
	/// </summary>	
	public class OrderDetailsViewDAO : ObjectViewDAO<OrderDetailsView>, IOrderDetailsViewDAO
	{
	}
	#endregion	
}
