using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region EmployeesService
	/// <summary>
	/// Service for entity 'Employees'.
	/// </summary>	
	public partial class EmployeesService : EntityServiceBase<Employees, EmployeesView>, IEmployeesService
	{
        /// <summary>
        /// Get all the Employeeses by LastName.
        /// </summary>
        /// <param name="lastName">LastName of the Employees</param>
        /// <returns></returns>
        public List<EmployeesView> GetAllByLastName(string lastName)
        {
            return ObjectViewDAO.Search(new SimpleCondition(EmployeesView.Properties.LastName, lastName));
        }
        
        /// <summary>
        /// Get all the Employeeses by PostalCode.
        /// </summary>
        /// <param name="postalCode">PostalCode of the Employees</param>
        /// <returns></returns>
        public List<EmployeesView> GetAllByPostalCode(string postalCode)
        {
            return ObjectViewDAO.Search(new SimpleCondition(EmployeesView.Properties.PostalCode, postalCode));
        }
        
        /// <summary>
        /// Get all the Employeeses of the ReportsToEmployee.
        /// </summary>
        /// <param name="reportsToEmployeeID">ID of ReportsToEmployee</param>
        /// <returns></returns>
		public List<EmployeesView> GetAllWithReportsToEmployee(int reportsToEmployeeID)
		{			
			return ObjectViewDAO.Search(new SimpleCondition(EmployeesView.Properties.ReportsTo, reportsToEmployeeID));
		}
		
	}
	#endregion	
}
