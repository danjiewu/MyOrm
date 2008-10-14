using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region Shippers
	/// <summary>
	/// Shippers.
	/// </summary>
	[Table("Shippers")]
	[Serializable]
	public class Shippers 
	{
		#region Constant		
		public const string	_ShipperID = "ShipperID";
		public const string	_CompanyName = "CompanyName";
		public const string	_Phone = "Phone";
		#endregion
		
		#region Member Variables		
		private int shipperID;
		private string companyName;
		private string phone;
		#endregion

		#region Public Properties
		[Column(IsPrimaryKey = true)]
		public int ShipperID
		{
			get { return shipperID; }			
			set { shipperID = value; }
		}
		
		[Column]
		public string CompanyName
		{
			get { return companyName; }			
			set { companyName = value; }
		}
		
		[Column]
		public string Phone
		{
			get { return phone; }			
			set { phone = value; }
		}
		
		#endregion
	}
	#endregion
}
