using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region IShippersService
	/// <summary>
	/// Service for entity 'Shippers'.
	/// </summary>	
	public partial interface IShippersService : IEntityService<Shippers>, IEntityViewService<Shippers>
	{
	}
	#endregion	
}
