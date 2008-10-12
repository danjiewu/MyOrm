using System;
using System.Data;
using MyOrm;

namespace Northwind.Data
{	
	#region RegionDAO
	/// <summary>
	/// DAO for object 'Region'.
	/// </summary>	
	public class RegionDAO : ObjectDAO<Region>, IRegionDAO
	{
	}
	#endregion
}
