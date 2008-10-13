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
	public class EmployeesDAO : ObjectDAO<Employees>, IEmployeesDAO
	{
		public Employees GetEmployees(Orders orders)
		{
			return GetObject(orders.EmployeeID);
		}
		
		public Employees GetEmployees(EmployeeTerritories employeeTerritories)
		{
			return GetObject(employeeTerritories.EmployeeID);
		}
		
		public Employees GetReportsToEmployees(Employees employees)
		{
			return GetObject(employees.ReportsTo);
		}
		
		public List<Employees> GetAllWithReportsToEmployees(Employees reportsToEmployees)
		{
			return Search(new SimpleCondition(Employees._ReportsTo, reportsToEmployees.EmployeeID));
		}
		
	}
	#endregion
	
	#region EmployeesViewDAO
	/// <summary>
	/// DAO for object's view 'EmployeesView'.
	/// </summary>	
	public class EmployeesViewDAO : ObjectViewDAO<EmployeesView>, IEmployeesViewDAO
	{
		public EmployeesView GetEmployees(Orders orders)
		{
			return GetObject(orders.EmployeeID);
		}
		
		public EmployeesView GetEmployees(EmployeeTerritories employeeTerritories)
		{
			return GetObject(employeeTerritories.EmployeeID);
		}
		
		public EmployeesView GetReportsToEmployees(Employees employees)
		{
			return GetObject(employees.ReportsTo);
		}
		
		public List<EmployeesView> GetAllWithReportsToEmployees(Employees reportsToEmployees)
		{
			return Search(new SimpleCondition(EmployeesView._ReportsTo, reportsToEmployees.EmployeeID));
		}
		
	}
	#endregion	
}
