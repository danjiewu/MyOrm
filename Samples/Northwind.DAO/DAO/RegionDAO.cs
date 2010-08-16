using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region RegionDAO
	/// <summary>
	/// DAO for object 'Region'.
	/// </summary>	
	public partial class RegionDAO : ObjectDAO<Region> { }
	#endregion

	#region RegionViewDAO
	/// <summary>
	/// DAO for object's view 'Region'.
	/// </summary>	
	public partial class RegionViewDAO : ObjectViewDAO<Region>
	{
	}
	#endregion	
}
