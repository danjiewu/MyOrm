using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region Region
	/// <summary>
	/// Region object for table 'Region'.
	/// </summary>
	[Table("Region")]
	[Serializable]
	public class Region 
	{
		#region Constant
		
		public const string	_RegionID = "RegionID";
		public const string	_RegionDescription = "RegionDescription";
		
		#endregion
		
		#region Member Variables
		
		private int regionID;
		private string regionDescription;

		#endregion

		#region Public Properties

		/// <summary>
		/// 
		/// </summary>	
		[Column(IsPrimaryKey = true)]
		public int RegionID
		{
			get { return regionID; }			
			set { regionID = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string RegionDescription
		{
			get { return regionDescription; }			
			set { regionDescription = value; }
		}
		
		#endregion
	}
	#endregion
}
