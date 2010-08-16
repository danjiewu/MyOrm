using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region Shippers
	/// <summary>
	/// Shippers.
	/// </summary>
	[Table("Shippers")]	
	[Serializable]
	public partial class Shippers : EntityBase
	{		
		#region Constant
        public static class Properties
        {
		    public const string	ShipperID = "ShipperID";
		    public const string	CompanyName = "CompanyName";
		    public const string	Phone = "Phone";
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// ShipperID
		/// </summary>
		[Column(IsPrimaryKey = true)]
		public int ShipperID { get; set; }	
        
		/// <summary>
		/// CompanyName
		/// </summary>
		[Column]
		public string CompanyName { get; set; }	
        
		/// <summary>
		/// Phone
		/// </summary>
		[Column]
		public string Phone { get; set; }	
        
		#endregion
	}
	#endregion
}
