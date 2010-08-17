using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region CategoriesService
	/// <summary>
	/// Service for entity 'Categories'.
	/// </summary>	
	public partial class CategoriesService : EntityServiceBase<Categories, Categories>, ICategoriesService
	{
        /// <summary>
        /// Get all the Categorieses by CategoryName.
        /// </summary>
        /// <param name="categoryName">CategoryName of the Categories</param>
        /// <returns></returns>
        public List<Categories> GetAllByCategoryName(string categoryName)
        {
            return ObjectViewDAO.Search(new SimpleCondition(Categories.Properties.CategoryName, categoryName));
        }
        
	}
	#endregion	
}
