using System;
using System.Data;
using MyOrm.Metadata;
using MyOrm.Attribute;

namespace Northwind.Data
{	
	#region Territories
	/// <summary>
	/// Territories.
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
		[Column(IsPrimaryKey = true)]
		public string TerritoryID
		{
			get { return territoryID; }			
			set { territoryID = value; }
		}
		
		[Column]
		public string TerritoryDescription
		{
			get { return territoryDescription; }			
			set { territoryDescription = value; }
		}
		
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
			
		public const string Region = "Region";
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
