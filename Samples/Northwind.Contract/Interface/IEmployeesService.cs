using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region IEmployeesService
	/// <summary>
	/// Service for entity 'Employees'.
	/// </summary>	
	public partial interface IEmployeesService : IEntityService<Employees>, IEntityViewService<EmployeesView>
	{
        /// <summary>
        /// Get all the Employeeses by LastName.
        /// </summary>
        /// <param name="lastName">LastName of the Employees</param>
        /// <returns></returns>
        List<EmployeesView> GetAllByLastName(string lastName);
        /// <summary>
        /// Get all the Employeeses by PostalCode.
        /// </summary>
        /// <param name="postalCode">PostalCode of the Employees</param>
        /// <returns></returns>
        List<EmployeesView> GetAllByPostalCode(string postalCode);
        /// <summary>
        /// Get all the Employeeses of the ReportsToEmployee.
        /// </summary>
        /// <param name="reportsToEmployeeID">ID of ReportsToEmployee</param>
        /// <returns></returns>
		List<EmployeesView> GetAllWithReportsToEmployee(int reportsToEmployeeID);
	}
	#endregion	
}
