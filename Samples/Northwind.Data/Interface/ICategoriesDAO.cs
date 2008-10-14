using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region ICategoriesDAO
	/// <summary>
	/// Interface of DAO for object 'Categories'.
	/// </summary>	
	public interface ICategoriesDAO : IObjectDAO<Categories>, IObjectViewDAO<Categories>
	{
		Categories GetCategoryOfProduct(Products products);
	}
	#endregion
}
