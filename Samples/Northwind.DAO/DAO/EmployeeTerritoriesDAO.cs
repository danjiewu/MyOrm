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
	public partial class EmployeeTerritoriesDAO : ObjectDAO<EmployeeTerritories> { }
	#endregion

	#region EmployeeTerritoriesViewDAO
	/// <summary>
	/// DAO for object's view 'EmployeeTerritoriesView'.
	/// </summary>	
	public partial class EmployeeTerritoriesViewDAO : ObjectViewDAO<EmployeeTerritoriesView>
	{
        /// <summary>
        /// Get all the EmployeeTerritorieses of the Employee.
        /// </summary>
        /// <param name="employeeID">ID of Employee</param>
        /// <returns></returns>
		public List<EmployeeTerritoriesView> GetAllWithEmployee(int employeeID)
		{			
			return Search(new SimpleCondition(EmployeeTerritoriesView.Properties.EmployeeID, employeeID));
		}
		
        /// <summary>
        /// Get all the EmployeeTerritorieses of the Territory.
        /// </summary>
        /// <param name="territoryID">ID of Territory</param>
        /// <returns></returns>
		public List<EmployeeTerritoriesView> GetAllWithTerritory(string territoryID)
		{			
			return Search(new SimpleCondition(EmployeeTerritoriesView.Properties.TerritoryID, territoryID));
		}
		
	}
	#endregion	
}
