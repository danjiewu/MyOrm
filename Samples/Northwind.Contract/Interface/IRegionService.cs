using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region IRegionService
	/// <summary>
	/// Service for entity 'Region'.
	/// </summary>	
	public partial interface IRegionService : IEntityService<Region>, IEntityViewService<Region>
	{
	}
	#endregion	
}
