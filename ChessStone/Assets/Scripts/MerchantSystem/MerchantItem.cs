using UnityEngine;
using System.Collections;

public class MerchantItem
{
	#region Data
	
	
	//[PrimaryKey]
	public int id { get; set; }

	//[NotNull]
	public int itemId { get; set; }

	//[NotNull]
	public int itemCost { get; set; }
	
	
	#endregion
}