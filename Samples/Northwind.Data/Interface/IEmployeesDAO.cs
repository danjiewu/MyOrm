using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region IEmployeesDAO
	/// <summary>
	/// Interface of DAO for object 'Employees'.
	/// </summary>	
	public interface IEmployeesDAO : IObjectDAO<Employees>, IObjectDAO
	{
		Employees GetEmployees(Orders orders);
		Employees GetEmployees(EmployeeTerritories employeeTerritories);
		Employees GetReportsToEmployees(Employees employees);
		List<Employees> GetAllWithReportsToEmployees(Employees reportsToEmployees);
	}
	#endregion
	
	#region IEmployeesViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'EmployeesView'.
	/// </summary>	
	public interface IEmployeesViewDAO : IObjectViewDAO<EmployeesView>, IObjectViewDAO
	{
		EmployeesView GetEmployees(Orders orders);
		EmployeesView GetEmployees(EmployeeTerritories employeeTerritories);
		EmployeesView GetReportsToEmployees(Employees employees);
		List<EmployeesView> GetAllWithReportsToEmployees(Employees reportsToEmployees);
	}
	#endregion	
}
