using UnityEngine;
using System.Collections;

using SQLite4Unity3d;

public class MerchantItemData
{
	#region Data
	
	
	[PrimaryKey]
	public int id { get; set; }
	
	public int merchantId { get; set; }

	public int characterId { get; set; }
	
	
	#endregion
}