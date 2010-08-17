using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region ISuppliersService
	/// <summary>
	/// Service for entity 'Suppliers'.
	/// </summary>	
	public partial interface ISuppliersService : IEntityService<Suppliers>, IEntityViewService<Suppliers>
	{
        /// <summary>
        /// Get all the Supplierses by CompanyName.
        /// </summary>
        /// <param name="companyName">CompanyName of the Suppliers</param>
        /// <returns></returns>
        List<Suppliers> GetAllByCompanyName(string companyName);
        /// <summary>
        /// Get all the Supplierses by PostalCode.
        /// </summary>
        /// <param name="postalCode">PostalCode of the Suppliers</param>
        /// <returns></returns>
        List<Suppliers> GetAllByPostalCode(string postalCode);
	}
	#endregion	
}
