using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region RegionService
	/// <summary>
	/// Service for entity 'Region'.
	/// </summary>	
	public partial class RegionService : EntityServiceBase<Region, Region>, IRegionService
	{
	}
	#endregion	
}
