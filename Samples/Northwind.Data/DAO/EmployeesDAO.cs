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
		public Employees GetEmployeeOfOrder(Orders orders)
		{
			return GetObject(orders.EmployeeID);
		}
		
		public Employees GetEmployeeOfEmployeeTerritory(EmployeeTerritories employeeTerritories)
		{
			return GetObject(employeeTerritories.EmployeeID);
		}
		
		public Employees GetReportsToEmployeeOfEmployee(Employees employees)
		{
			return GetObject(employees.ReportsTo);
		}
		
		public List<Employees> GetAllWithReportsToEmployee(Employees reportsToEmployee)
		{
			return Search(new SimpleCondition(Employees._ReportsTo, reportsToEmployee.EmployeeID));
		}
		
	}
	#endregion
	
	#region EmployeesViewDAO
	/// <summary>
	/// DAO for object's view 'EmployeesView'.
	/// </summary>	
	public class EmployeesViewDAO : ObjectViewDAO<EmployeesView>, IEmployeesViewDAO
	{
		public EmployeesView GetEmployeeOfOrder(Orders orders)
		{
			return GetObject(orders.EmployeeID);
		}
		
		public EmployeesView GetEmployeeOfEmployeeTerritory(EmployeeTerritories employeeTerritories)
		{
			return GetObject(employeeTerritories.EmployeeID);
		}
		
		public EmployeesView GetReportsToEmployeeOfEmployee(Employees employees)
		{
			return GetObject(employees.ReportsTo);
		}
		
		public List<EmployeesView> GetAllWithReportsToEmployee(Employees reportsToEmployee)
		{
			return Search(new SimpleCondition(EmployeesView._ReportsTo, reportsToEmployee.EmployeeID));
		}
		
	}
	#endregion	
}
