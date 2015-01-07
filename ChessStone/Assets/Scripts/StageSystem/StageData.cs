using UnityEngine;
using System.Collections;

public class StageData
{
	#region Data
	
	
	//[PrimaryKey]
	public int id { get; set; }
	
	//[NotNull]
	public string name { get; set; }
	
	//[NotNull]
	public string description { get; set; }

	//[NotNull]
	public string mapPath { get; set; }

	//[NotNull]
	public int rewardCash { get; set; }

	
	#endregion
}