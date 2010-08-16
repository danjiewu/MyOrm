using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region EmployeesDAO
	/// <summary>
	/// DAO for object 'Employees'.
	/// </summary>	
	public partial class EmployeesDAO : ObjectDAO<Employees> { }
	#endregion

	#region EmployeesViewDAO
	/// <summary>
	/// DAO for object's view 'EmployeesView'.
	/// </summary>	
	public partial class EmployeesViewDAO : ObjectViewDAO<EmployeesView>
	{
        /// <summary>
        /// Get all the Employeeses by LastName.
        /// </summary>
        /// <param name="lastName">LastName of the Employees</param>
        /// <returns></returns>
        public List<EmployeesView> GetAllByLastName(string lastName)
        {
            return Search(new SimpleCondition(EmployeesView.Properties.LastName, lastName));
        }
        
        /// <summary>
        /// Get all the Employeeses by PostalCode.
        /// </summary>
        /// <param name="postalCode">PostalCode of the Employees</param>
        /// <returns></returns>
        public List<EmployeesView> GetAllByPostalCode(string postalCode)
        {
            return Search(new SimpleCondition(EmployeesView.Properties.PostalCode, postalCode));
        }
        
        /// <summary>
        /// Get all the Employeeses of the ReportsToEmployee.
        /// </summary>
        /// <param name="reportsToEmployeeID">ID of ReportsToEmployee</param>
        /// <returns></returns>
		public List<EmployeesView> GetAllWithReportsToEmployee(int reportsToEmployeeID)
		{			
			return Search(new SimpleCondition(EmployeesView.Properties.ReportsTo, reportsToEmployeeID));
		}
		
	}
	#endregion	
}
