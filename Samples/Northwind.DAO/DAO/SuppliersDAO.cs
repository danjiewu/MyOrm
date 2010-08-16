using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region SuppliersDAO
	/// <summary>
	/// DAO for object 'Suppliers'.
	/// </summary>	
	public partial class SuppliersDAO : ObjectDAO<Suppliers> { }
	#endregion

	#region SuppliersViewDAO
	/// <summary>
	/// DAO for object's view 'Suppliers'.
	/// </summary>	
	public partial class SuppliersViewDAO : ObjectViewDAO<Suppliers>
	{
        /// <summary>
        /// Get all the Supplierses by CompanyName.
        /// </summary>
        /// <param name="companyName">CompanyName of the Suppliers</param>
        /// <returns></returns>
        public List<Suppliers> GetAllByCompanyName(string companyName)
        {
            return Search(new SimpleCondition(Suppliers.Properties.CompanyName, companyName));
        }
        
        /// <summary>
        /// Get all the Supplierses by PostalCode.
        /// </summary>
        /// <param name="postalCode">PostalCode of the Suppliers</param>
        /// <returns></returns>
        public List<Suppliers> GetAllByPostalCode(string postalCode)
        {
            return Search(new SimpleCondition(Suppliers.Properties.PostalCode, postalCode));
        }
        
	}
	#endregion	
}
