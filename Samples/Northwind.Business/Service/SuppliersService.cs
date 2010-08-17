using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region SuppliersService
	/// <summary>
	/// Service for entity 'Suppliers'.
	/// </summary>	
	public partial class SuppliersService : EntityServiceBase<Suppliers, Suppliers>, ISuppliersService
	{
        /// <summary>
        /// Get all the Supplierses by CompanyName.
        /// </summary>
        /// <param name="companyName">CompanyName of the Suppliers</param>
        /// <returns></returns>
        public List<Suppliers> GetAllByCompanyName(string companyName)
        {
            return ObjectViewDAO.Search(new SimpleCondition(Suppliers.Properties.CompanyName, companyName));
        }
        
        /// <summary>
        /// Get all the Supplierses by PostalCode.
        /// </summary>
        /// <param name="postalCode">PostalCode of the Suppliers</param>
        /// <returns></returns>
        public List<Suppliers> GetAllByPostalCode(string postalCode)
        {
            return ObjectViewDAO.Search(new SimpleCondition(Suppliers.Properties.PostalCode, postalCode));
        }
        
	}
	#endregion	
}
