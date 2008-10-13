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
	public class RegionDAO : ObjectDAO<Region>, IRegionDAO
	{
		public Region GetRegion(Territories territories)
		{
			return GetObject(territories.RegionID);
		}
		
	}
	#endregion
}
