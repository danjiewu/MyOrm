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
	public interface IEmployeesDAO : IObjectDAO<Employees>, IObjectViewDAO<Employees>, IObjectDAO, IObjectViewDAO
	{
		Employees GetEmployeeOfOrder(Orders orders);
		Employees GetEmployeeOfEmployeeTerritory(EmployeeTerritories employeeTerritories);
		Employees GetReportsToEmployeeOfEmployee(Employees employees);
		List<Employees> GetAllWithReportsToEmployee(Employees reportsToEmployee);
	}
	#endregion
	
	#region IEmployeesViewDAO
	/// <summary>
	/// Interface of DAO for object's view 'EmployeesView'.
	/// </summary>	
	public interface IEmployeesViewDAO : IObjectViewDAO<EmployeesView>, IObjectViewDAO
	{
		EmployeesView GetEmployeeOfOrder(Orders orders);
		EmployeesView GetEmployeeOfEmployeeTerritory(EmployeeTerritories employeeTerritories);
		EmployeesView GetReportsToEmployeeOfEmployee(Employees employees);
		List<EmployeesView> GetAllWithReportsToEmployee(Employees reportsToEmployee);
	}
	#endregion	
}
