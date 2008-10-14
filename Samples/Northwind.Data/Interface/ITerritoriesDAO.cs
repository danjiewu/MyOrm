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
	public interface ITerritoriesDAO : IObjectDAO<Territories>, IObjectViewDAO<Territories>, IObjectDAO, IObjectViewDAO
	{
		Territories GetTerritoryOfEmployeeTerritory(EmployeeTerritories employeeTerritories);
		List<Territories> GetAllWithRegion(Region region);
	}
	#endregion
	
	#region ITerritoriesViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'TerritoriesView'.
	/// </summary>	
	public interface ITerritoriesViewDAO : IObjectViewDAO<TerritoriesView>, IObjectViewDAO
	{
		TerritoriesView GetTerritoryOfEmployeeTerritory(EmployeeTerritories employeeTerritories);
		List<TerritoriesView> GetAllWithRegion(Region region);
	}
	#endregion	
}
