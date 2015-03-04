using UnityEngine;
using System.Collections;

using SQLite4Unity3d;

public class StageData
{
	#region Data


	[PrimaryKey]
	public int id { get; set; }
	
	//[NotNull]
	public string name { get; set; }
	
	//[NotNull]
	public string description { get; set; }

	//[NotNull]
	public int rewardCash { get; set; }

	//[NotNull]
	public string mapPath { get; set; }
	
	public int objectiveId { get; set; }

	
	#endregion
}