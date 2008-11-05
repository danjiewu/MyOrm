using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region TerritoriesDAO
	/// <summary>
	/// DAO for object 'Territories'.
	/// </summary>	
	public partial class TerritoriesDAO : ObjectDAO<Territories>, ITerritoriesDAO
	{
		public Territories GetTerritoryOfEmployeeTerritory(EmployeeTerritories employeeTerritories)
		{
			return GetObject(employeeTerritories.TerritoryID);
		}
		
		public List<Territories> GetAllWithRegion(Region region)
		{
			return Search(new SimpleCondition(Territories._RegionID, region.RegionID));
		}
		
	}
	#endregion
	
	#region TerritoriesViewDAO
	/// <summary>
	/// DAO for object's view 'TerritoriesView'.
	/// </summary>	
	public partial class TerritoriesViewDAO : ObjectViewDAO<TerritoriesView>, ITerritoriesViewDAO
	{
		public TerritoriesView GetTerritoryOfEmployeeTerritory(EmployeeTerritories employeeTerritories)
		{
			return GetObject(employeeTerritories.TerritoryID);
		}
		
		public List<TerritoriesView> GetAllWithRegion(Region region)
		{
			return Search(new SimpleCondition(TerritoriesView._RegionID, region.RegionID));
		}
		
	}
	#endregion	
}
