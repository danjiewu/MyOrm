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
	public partial class RegionDAO : ObjectDAO<Region>, IRegionDAO
	{
		public Region GetRegionOfTerritory(Territories territories)
		{
			return GetObject(territories.RegionID);
		}
		
	}
	#endregion
}
