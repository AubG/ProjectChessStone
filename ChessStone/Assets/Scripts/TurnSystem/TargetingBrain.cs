using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using X_UniTMX;
using Pathfinding;

public class TargetingBrain : MonoBehaviour
{
	#region Modular Data


	[SerializeField]
	private PathingBrain pathingBrain;


	#endregion

	#region Data


	[SerializeField]
	private int targetPlayerIndex;

	private Player _targetPlayer;
	private Player targetPlayer {
		get {
			if(_targetPlayer == null) _targetPlayer = PlayerManager.Instance.GetPlayer(targetPlayerIndex);
			return _targetPlayer;
		}
	}

	private List<GameCharacter> targetPool {
		get {
			return targetPlayer.characters;
		}
	}

	
	#endregion

	#region AI


	public void SearchForTarget(GameCharacter client, System.Action<GameUnit> result) {
		StartCoroutine(ProcessPotentials(client, result));
	}
	
	private IEnumerator ProcessPotentials(GameCharacter client, System.Action<GameUnit> result) {
		int currDist = 0;
		int minDist = 100;
		GameUnit min = null;

		bool pingSuccessful = false;

		foreach(GameCharacter target in targetPool) {
			yield return StartCoroutine(PingTile(client, target.currTile, value => currDist = value));

			if(currDist > 0 && currDist < minDist) {
				minDist = currDist;
				min = target;
			}

			foreach(GameUnitLink link in target.health.links) {
				yield return StartCoroutine(PingTile(client, link.currTile, value => currDist = value));

				if(currDist > 0 && currDist < minDist) {
					minDist = currDist;
					min = link;
				}
			}

			yield return null;
		}
	
		result(min);
	}
	
	private IEnumerator PingTile(GameCharacter client, Tile target, System.Action<int> result) {
		Path temp = null;
		//Debug.Log ("pinging: " + client.name + " to " + target.currUnit.name);
		pathingBrain.RequestPathTarget(client, target, (Path p) => temp = p);

		while(temp == null) yield return null;
		//Debug.Log ("pinging success: " + !temp.error);
		if(!temp.error) {
			result(temp.path.Count);
		} else {
			result(-1);
		}
	}

	
	#endregion
}

