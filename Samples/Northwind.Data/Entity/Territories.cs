using System;
using System.Data;
using Data.Common;

namespace Northwind.Data
{	
	#region Territories
	/// <summary>
	/// Territories object for table 'Territories'.
	/// </summary>
	[Table("Territories")]
	[Serializable]
	public class Territories 
	{
		#region Constant
		
		public const string	_TerritoryID = "TerritoryID";
		public const string	_TerritoryDescription = "TerritoryDescription";
		public const string	_RegionID = "RegionID";
		
		#endregion
		
		#region Member Variables
		
		private string territoryID;
		private string territoryDescription;
		private int regionID;

		#endregion

		#region Public Properties

		/// <summary>
		/// 
		/// </summary>	
		[Column(IsPrimaryKey = true)]
		public string TerritoryID
		{
			get { return territoryID; }			
			set { territoryID = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public string TerritoryDescription
		{
			get { return territoryDescription; }			
			set { territoryDescription = value; }
		}
		
		/// <summary>
		/// 
		/// </summary>	
		[Column]
		public int RegionID
		{
			get { return regionID; }			
			set { regionID = value; }
		}
		
		#endregion
	}
	#endregion
	
	#region TerritoriesView
	/// <summary>
	/// TerritoriesView.
	/// </summary>	
	[TableJoin(typeof(Region), "RegionID", AliasName = TerritoriesView.Region)]
	public class TerritoriesView : Territories
	{
		#region Constant
		
		public const string	_Region_RegionDescription = "Region_RegionDescription";			
			
		private const string Region = "Region";
		
		#endregion
		
		#region Member Variables
		
		private string region_RegionDescription;			
		
		#endregion

		#region Public Properties

		[Column("RegionDescription", Foreign = TerritoriesView.Region, ColumnMode = ColumnMode.Read)]
		public string Region_RegionDescription
		{
			get { return region_RegionDescription; }			
			set { region_RegionDescription = value; }
		}
		
		#endregion
	}
	#endregion	
}
