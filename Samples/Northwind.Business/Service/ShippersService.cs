using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region ShippersService
	/// <summary>
	/// Service for entity 'Shippers'.
	/// </summary>	
	public partial class ShippersService : EntityServiceBase<Shippers, Shippers>, IShippersService
	{
	}
	#endregion	
}
