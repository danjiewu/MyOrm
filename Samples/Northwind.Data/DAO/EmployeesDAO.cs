using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region EmployeesDAO
	/// <summary>
	/// DAO for object 'Employees'.
	/// </summary>	
	public class EmployeesDAO : ObjectDAO<Employees>, IEmployeesDAO
	{
	}
	#endregion
	
	#region EmployeesViewDAO
	/// <summary>
	/// DAO for object's view 'EmployeesView'.
	/// </summary>	
	public class EmployeesViewDAO : ObjectViewDAO<EmployeesView>, IEmployeesViewDAO
	{
	}
	#endregion	
}
