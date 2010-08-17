using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region IEmployeeTerritoriesService
	/// <summary>
	/// Service for entity 'EmployeeTerritories'.
	/// </summary>	
	public partial interface IEmployeeTerritoriesService : IEntityService<EmployeeTerritories>, IEntityViewService<EmployeeTerritoriesView>
	{
        /// <summary>
        /// Get all the EmployeeTerritorieses of the Employee.
        /// </summary>
        /// <param name="employeeID">ID of Employee</param>
        /// <returns></returns>
		List<EmployeeTerritoriesView> GetAllWithEmployee(int employeeID);
        /// <summary>
        /// Get all the EmployeeTerritorieses of the Territory.
        /// </summary>
        /// <param name="territoryID">ID of Territory</param>
        /// <returns></returns>
		List<EmployeeTerritoriesView> GetAllWithTerritory(string territoryID);
	}
	#endregion	
}
