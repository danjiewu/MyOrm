using System;
using System.Data;
using MyOrm.Common;

namespace Northwind.Data
{	
	#region Region
	/// <summary>
	/// Region.
	/// </summary>
	[Table("Region")]	
	[Serializable]
	public partial class Region : EntityBase
	{		
		#region Constant
        public static class Properties
        {
		    public const string	RegionID = "RegionID";
		    public const string	RegionDescription = "RegionDescription";
        }
		#endregion

		#region Public Properties
		/// <summary>
		/// RegionID
		/// </summary>
		[Column(IsPrimaryKey = true)]
		public int RegionID { get; set; }	
        
		/// <summary>
		/// RegionDescription
		/// </summary>
		[Column]
		public string RegionDescription { get; set; }	
        
		#endregion
	}
	#endregion
}
