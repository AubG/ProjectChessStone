using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public enum StageObjectiveType {
	DefeatAllAI,
}

public enum WinState {
	None,
	Victory,
	Defeat
}

public class StageManager : Singleton<StageManager>
{
	#region Data


	//public SimpleSQL.SimpleSQLManager dbManager;

	private List<StageObjective> stageObjectives = new List<StageObjective>();

	private StageData currStage;


	#endregion

	#region Init


	public StageData LoadStage(int id) {
		DataService ds = new DataService("stages.bytes");
		
		StageData loaded = ds.GetItemMatching<StageData>(x => x.id == id);
		if(loaded != null) {
			currStage = loaded;
			InitializeObjective();
			InitializeLoseState();
			
			return currStage;
		}
		
		return null;
	}


	#endregion

	#region Objectives


	public void AddObjective(StageObjective objective) {
		stageObjectives.Add(objective);
	}

	private void InitializeObjective() {
		switch((StageObjectiveType)currStage.objectiveId) {
		case StageObjectiveType.DefeatAllAI:
			DefeatAllAIObjective newObjective = new DefeatAllAIObjective();
			newObjective.Init();
			AddObjective(newObjective);
			break;
		}
	}

	private void InitializeLoseState() {
		GameMap.Instance.SubscribeKillCharacterEvent(CheckLocalPlayer);
	}

	public void CheckObjectives() {
		foreach(StageObjective objective in stageObjectives) {
			if(!objective.IsComplete()) return;
		}

		EndStage(WinState.Victory);
	}


	#endregion

	#region Interaction


	public void EndStage(WinState winState = default(WinState)) {
		if(winState == WinState.Victory)
			PlayerData.Instance.RegisterCompletedStage(currStage.id);

		StartCoroutine(DelayedShowWinScreen(GameStateManager.Instance.HandleEnd));
	}


	#endregion

	#region Helpers


	private IEnumerator DelayedShowWinScreen(Action callback) {
		yield return new WaitForSeconds(1.0f);

		callback();
	}

	private void CheckLocalPlayer(GameCharacter killed, GameCharacter killer) {
		Player localPlayer = PlayerManager.Instance.GetLocalPlayer();

		if(killed.owningPlayer == localPlayer) {
			if(localPlayer.NumCharacters() == 0)
				EndStage(WinState.Defeat);
		}
	}


	#endregion
}