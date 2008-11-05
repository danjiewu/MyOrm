using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region ITerritoriesDAO
	/// <summary>
	/// Interface of DAO for object 'Territories'.
	/// </summary>	
	public partial interface ITerritoriesDAO : IObjectDAO<Territories>, IObjectViewDAO<Territories>
	{
		Territories GetTerritoryOfEmployeeTerritory(EmployeeTerritories employeeTerritories);
		List<Territories> GetAllWithRegion(Region region);
	}
	#endregion
	
	#region ITerritoriesViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'TerritoriesView'.
	/// </summary>	
	public partial interface ITerritoriesViewDAO : IObjectViewDAO<TerritoriesView>
	{
		TerritoriesView GetTerritoryOfEmployeeTerritory(EmployeeTerritories employeeTerritories);
		List<TerritoriesView> GetAllWithRegion(Region region);
	}
	#endregion	
}
