using System;
using System.Data;
using MyOrm;

namespace Northwind.Data
{	
	#region CategoriesDAO
	/// <summary>
	/// DAO for object 'Categories'.
	/// </summary>	
	public class CategoriesDAO : ObjectDAO<Categories>, ICategoriesDAO
	{
	}
	#endregion
}
