using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region CategoriesDAO
	/// <summary>
	/// DAO for object 'Categories'.
	/// </summary>	
	public class CategoriesDAO : ObjectDAO<Categories>, ICategoriesDAO
	{
		public Categories GetCategories(Products products)
		{
			return GetObject(products.CategoryID);
		}
		
	}
	#endregion
}
