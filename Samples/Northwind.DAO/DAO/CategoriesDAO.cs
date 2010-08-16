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
	public partial class CategoriesDAO : ObjectDAO<Categories> { }
	#endregion

	#region CategoriesViewDAO
	/// <summary>
	/// DAO for object's view 'Categories'.
	/// </summary>	
	public partial class CategoriesViewDAO : ObjectViewDAO<Categories>
	{
        /// <summary>
        /// Get all the Categorieses by CategoryName.
        /// </summary>
        /// <param name="categoryName">CategoryName of the Categories</param>
        /// <returns></returns>
        public List<Categories> GetAllByCategoryName(string categoryName)
        {
            return Search(new SimpleCondition(Categories.Properties.CategoryName, categoryName));
        }
        
	}
	#endregion	
}
