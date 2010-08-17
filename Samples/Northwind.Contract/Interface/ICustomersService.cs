using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using Northwind.Data;

namespace Northwind.Business
{	
	#region ICustomersService
	/// <summary>
	/// Service for entity 'Customers'.
	/// </summary>	
	public partial interface ICustomersService : IEntityService<Customers>, IEntityViewService<Customers>
	{
        /// <summary>
        /// Get all the Customerses by CompanyName.
        /// </summary>
        /// <param name="companyName">CompanyName of the Customers</param>
        /// <returns></returns>
        List<Customers> GetAllByCompanyName(string companyName);
        /// <summary>
        /// Get all the Customerses by City.
        /// </summary>
        /// <param name="city">City of the Customers</param>
        /// <returns></returns>
        List<Customers> GetAllByCity(string city);
        /// <summary>
        /// Get all the Customerses by Region.
        /// </summary>
        /// <param name="region">Region of the Customers</param>
        /// <returns></returns>
        List<Customers> GetAllByRegion(string region);
        /// <summary>
        /// Get all the Customerses by PostalCode.
        /// </summary>
        /// <param name="postalCode">PostalCode of the Customers</param>
        /// <returns></returns>
        List<Customers> GetAllByPostalCode(string postalCode);
	}
	#endregion	
}
