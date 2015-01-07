using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class StageManager : Singleton<StageManager>
{

	//public SimpleSQL.SimpleSQLManager dbManager;

	public StageData LoadStage(int id) {
		/*List<CharacterData> _characters = new List<CharacterData> (from w dbManager.Table<Weapon> ()
		                  select w);*/
		
		StageData data = new StageData() {
				id = 12,
				name = "DAE Archive #544",
				description = "",
				mapPath = "GameMaps/0_stage_0",
				rewardCash = 500
		};

		return data;
	}
}