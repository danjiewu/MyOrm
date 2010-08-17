using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region EmployeeTerritoriesService
	/// <summary>
	/// Service for entity 'EmployeeTerritories'.
	/// </summary>	
	public partial class EmployeeTerritoriesService : EntityServiceBase<EmployeeTerritories, EmployeeTerritoriesView>, IEmployeeTerritoriesService
	{
        /// <summary>
        /// Get all the EmployeeTerritorieses of the Employee.
        /// </summary>
        /// <param name="employeeID">ID of Employee</param>
        /// <returns></returns>
		public List<EmployeeTerritoriesView> GetAllWithEmployee(int employeeID)
		{			
			return ObjectViewDAO.Search(new SimpleCondition(EmployeeTerritoriesView.Properties.EmployeeID, employeeID));
		}
		
        /// <summary>
        /// Get all the EmployeeTerritorieses of the Territory.
        /// </summary>
        /// <param name="territoryID">ID of Territory</param>
        /// <returns></returns>
		public List<EmployeeTerritoriesView> GetAllWithTerritory(string territoryID)
		{			
			return ObjectViewDAO.Search(new SimpleCondition(EmployeeTerritoriesView.Properties.TerritoryID, territoryID));
		}
		
	}
	#endregion	
}
