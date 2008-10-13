using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region ICategoriesDAO
	/// <summary>
	/// Interface of DAO for object 'Categories'.
	/// </summary>	
	public interface ICategoriesDAO : IObjectDAO<Categories>, IObjectDAO
	{		
	}
	#endregion
}
