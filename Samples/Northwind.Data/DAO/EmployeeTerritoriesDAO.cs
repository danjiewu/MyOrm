using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region EmployeeTerritoriesDAO
	/// <summary>
	/// DAO for object 'EmployeeTerritories'.
	/// </summary>	
	public class EmployeeTerritoriesDAO : ObjectDAO<EmployeeTerritories>, IEmployeeTerritoriesDAO
	{
		public List<EmployeeTerritories> GetAllWithEmployees(Employees employees)
		{
			return Search(new SimpleCondition(EmployeeTerritories._EmployeeID, employees.EmployeeID));
		}
		
		public List<EmployeeTerritories> GetAllWithTerritories(Territories territories)
		{
			return Search(new SimpleCondition(EmployeeTerritories._TerritoryID, territories.TerritoryID));
		}
		
	}
	#endregion
	
	#region EmployeeTerritoriesViewDAO
	/// <summary>
	/// DAO for object's view 'EmployeeTerritoriesView'.
	/// </summary>	
	public class EmployeeTerritoriesViewDAO : ObjectViewDAO<EmployeeTerritoriesView>, IEmployeeTerritoriesViewDAO
	{
		public List<EmployeeTerritoriesView> GetAllWithEmployees(Employees employees)
		{
			return Search(new SimpleCondition(EmployeeTerritoriesView._EmployeeID, employees.EmployeeID));
		}
		
		public List<EmployeeTerritoriesView> GetAllWithTerritories(Territories territories)
		{
			return Search(new SimpleCondition(EmployeeTerritoriesView._TerritoryID, territories.TerritoryID));
		}
		
	}
	#endregion	
}
