using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region IRegionDAO
	/// <summary>
	/// Interface of DAO for object 'Region'.
	/// </summary>	
	public interface IRegionDAO : IObjectDAO<Region>, IObjectViewDAO<Region>
	{
		Region GetRegionOfTerritory(Territories territories);
	}
	#endregion
}
