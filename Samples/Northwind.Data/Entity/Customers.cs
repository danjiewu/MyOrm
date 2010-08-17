using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{
    #region Customers
    /// <summary>
    /// Customers.
    /// </summary>
    [Table("Customers")]
    [Serializable]
    public partial class Customers : EntityBase
    {
        #region Constant
        public static class Properties
        {
            public const string CustomerID = "CustomerID";
            public const string CompanyName = "CompanyName";
            public const string ContactName = "ContactName";
            public const string ContactTitle = "ContactTitle";
            public const string Address = "Address";
            public const string City = "City";
            public const string Region = "Region";
            public const string PostalCode = "PostalCode";
            public const string Country = "Country";
            public const string Phone = "Phone";
            public const string Fax = "Fax";
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// CustomerID
        /// </summary>
        [Column(IsPrimaryKey = true)]
        public string CustomerID { get; set; }

        /// <summary>
        /// CompanyName
        /// </summary>
        [Column(IsIndex = true)]
        public string CompanyName { get; set; }

        /// <summary>
        /// ContactName
        /// </summary>
        [Column]
        public string ContactName { get; set; }

        /// <summary>
        /// ContactTitle
        /// </summary>
        [Column]
        public string ContactTitle { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        [Column]
        public string Address { get; set; }

        /// <summary>
        /// City
        /// </summary>
        [Column(IsIndex = true)]
        public string City { get; set; }

        /// <summary>
        /// Region
        /// </summary>
        [Column(IsIndex = true)]
        public string Region { get; set; }

        /// <summary>
        /// PostalCode
        /// </summary>
        [Column(IsIndex = true)]
        public string PostalCode { get; set; }

        /// <summary>
        /// Country
        /// </summary>
        [Column]
        public string Country { get; set; }

        /// <summary>
        /// Phone
        /// </summary>
        [Column]
        public string Phone { get; set; }

        /// <summary>
        /// Fax
        /// </summary>
        [Column]
        public string Fax { get; set; }

        #endregion

        #region IIndexedProperty
        public override object this[string propertyName]
        {
            get
            {
                switch (propertyName)
                {
                    case Properties.CustomerID: return CustomerID;
                    case Properties.CompanyName: return CompanyName;
                    case Properties.ContactName: return ContactName;
                    case Properties.ContactTitle: return ContactTitle;
                    case Properties.Address: return Address;
                    case Properties.City: return City;
                    case Properties.Region: return Region;
                    case Properties.PostalCode: return PostalCode;
                    case Properties.Country: return Country;
                    case Properties.Phone: return Phone;
                    case Properties.Fax: return Fax;
                    default: return base[propertyName];
                }
            }
            set
            {
                switch (propertyName)
                {
                    case Properties.CustomerID: CustomerID = (string)value; break;
                    case Properties.CompanyName: CompanyName = (string)value; break;
                    case Properties.ContactName: ContactName = (string)value; break;
                    case Properties.ContactTitle: ContactTitle = (string)value; break;
                    case Properties.Address: Address = (string)value; break;
                    case Properties.City: City = (string)value; break;
                    case Properties.Region: Region = (string)value; break;
                    case Properties.PostalCode: PostalCode = (string)value; break;
                    case Properties.Country: Country = (string)value; break;
                    case Properties.Phone: Phone = (string)value; break;
                    case Properties.Fax: Fax = (string)value; break;
                    default: base[propertyName] = value; break;
                }
            }
        }

        #endregion
    }
    #endregion
}
