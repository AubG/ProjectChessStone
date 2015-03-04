using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Single player combat objective.
/// Is satisfied when all AI enemy characters are dead.
/// </summary>
public class DefeatAllAIObjective : StageObjective
{
	public override void Init() {
		GameMap.Instance.SubscribeKillCharacterEvent(UpdateObjective);
	}

	public override bool IsComplete() {
		return complete;
	}

	private void UpdateObjective(GameCharacter killed, GameCharacter killer) {
		if(killed.owningPlayer.controllerType == Player.ControllerType.Computer) {
			IEnumerable<Player> players = PlayerManager.Instance.GetPlayersMatching(
				(player, index) => player.controllerType == Player.ControllerType.Computer);
			
			foreach(Player p in players) {
				if(p.NumCharacters() > 0) return;
			}

			complete = true;
			StageManager.Instance.CheckObjectives();
		}
	}
}