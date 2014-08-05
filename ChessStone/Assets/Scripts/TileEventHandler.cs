using UnityEngine;
using System.Collections;
using X_UniTMX;

public class TileEventHandler : MonoBehaviour
{
	public PathScript selectedPathing;
	//public PathScript selectedPathing { get; set; };

	void Update() {
		HandlePlayerControls();
	}

	private void HandlePlayerControls ()
	{
		if(Input.GetButtonDown("Fire1")) {
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Map gameMap = GameMap.Instance.map;
			Vector2 targetIndices = gameMap.WorldPointToTileIndex(worldPoint);
			
			selectedPathing.IssueTileMoveOrder(targetIndices);
			Debug.Log ("Moving player to " + targetIndices);
		}
	}
}