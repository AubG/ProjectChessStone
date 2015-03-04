using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;

public delegate void OnTileVoidDelegate(Tile t);

public sealed class TileSelector : MonoBehaviour
{
	#region State Data


	public enum State {
		None,
		Neutral,
		End
	}
	
	public State currState { get; private set; }


	#endregion

	#region Game Data
	
	
	public OnTileVoidDelegate clickCallback;


	#endregion

	#region Initialization
	

	public void Begin()
	{
		currState = State.Neutral;
		StartCoroutine(UpdateSelection());
	}

	public void End() {
		currState = State.End;
	}


	#endregion

	#region Update Methods


	private IEnumerator UpdateSelection()
	{
		while(currState != State.End) {
			PollSelection();
			
			yield return null;
		}
	}


	#endregion

	#region Helpers


	private void PollSelection() {
		if(Input.GetButtonDown("Fire1")) {
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Map gameMap = GameMap.Instance.map;
			
			Vector2 targetIndices = gameMap.WorldPointToTileIndex(worldPoint);
			TileGrid mainGrid = GameMap.Instance.mainGrid;
			if(targetIndices.x <= 0 || targetIndices.x >= mainGrid.Width || targetIndices.y <= 0 || targetIndices.y >= mainGrid.Height) return;
			//Debug.Log ("clicked " + targetIndices.x + ", " + targetIndices.y);
			Tile clickedTile = GameMap.Instance.mainGrid[(int)targetIndices.x, (int)targetIndices.y];
			clickCallback(clickedTile);
		}
	}

	
	#endregion
}