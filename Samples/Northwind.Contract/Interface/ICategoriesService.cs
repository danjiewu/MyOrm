using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region ICategoriesService
	/// <summary>
	/// Service for entity 'Categories'.
	/// </summary>	
	public partial interface ICategoriesService : IEntityService<Categories>, IEntityViewService<Categories>
	{
        /// <summary>
        /// Get all the Categorieses by CategoryName.
        /// </summary>
        /// <param name="categoryName">CategoryName of the Categories</param>
        /// <returns></returns>
        List<Categories> GetAllByCategoryName(string categoryName);
	}
	#endregion	
}
