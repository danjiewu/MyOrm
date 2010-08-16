using System;
using System.Data;
using System.Collections.Generic;
using MyOrm.Common;
using MyOrm;

namespace Northwind.Data
{	
	#region CustomersDAO
	/// <summary>
	/// DAO for object 'Customers'.
	/// </summary>	
	public partial class CustomersDAO : ObjectDAO<Customers> { }
	#endregion

	#region CustomersViewDAO
	/// <summary>
	/// DAO for object's view 'Customers'.
	/// </summary>	
	public partial class CustomersViewDAO : ObjectViewDAO<Customers>
	{
        /// <summary>
        /// Get all the Customerses by CompanyName.
        /// </summary>
        /// <param name="companyName">CompanyName of the Customers</param>
        /// <returns></returns>
        public List<Customers> GetAllByCompanyName(string companyName)
        {
            return Search(new SimpleCondition(Customers.Properties.CompanyName, companyName));
        }
        
        /// <summary>
        /// Get all the Customerses by City.
        /// </summary>
        /// <param name="city">City of the Customers</param>
        /// <returns></returns>
        public List<Customers> GetAllByCity(string city)
        {
            return Search(new SimpleCondition(Customers.Properties.City, city));
        }
        
        /// <summary>
        /// Get all the Customerses by Region.
        /// </summary>
        /// <param name="region">Region of the Customers</param>
        /// <returns></returns>
        public List<Customers> GetAllByRegion(string region)
        {
            return Search(new SimpleCondition(Customers.Properties.Region, region));
        }
        
        /// <summary>
        /// Get all the Customerses by PostalCode.
        /// </summary>
        /// <param name="postalCode">PostalCode of the Customers</param>
        /// <returns></returns>
        public List<Customers> GetAllByPostalCode(string postalCode)
        {
            return Search(new SimpleCondition(Customers.Properties.PostalCode, postalCode));
        }
        
	}
	#endregion	
}
