using UnityEngine;
using System.Collections;
using X_UniTMX;

public class TileSelector : MonoBehaviour
{
	public enum State {
		None,
		Select,
		Action,
		End
	}

	public Tile selectedTile { get; private set; }
	public GameUnit selectedUnit { get; private set; }
	public GameCharacter selectedCharacter { get; private set; }

	public GameCharacter actionCharacter { get; private set; }
	public Tile actionTile { get; private set; }
	
	public State currState { get; private set; }

	private void HandlePlayerControls ()
	{
		if(Input.GetButtonDown("Fire1")) {
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Map gameMap = GameMap.Instance.map;
			
			Vector2 targetIndices = gameMap.WorldPointToTileIndex(worldPoint);
			
			selectedTile = GameMap.Instance.mainGrid[(int)targetIndices.x, (int)targetIndices.y];

			if(selectedTile.currUnit) {
				selectedUnit = selectedTile.currUnit;

				if(selectedUnit is GameCharacter) {
					if(selectedCharacter != null) selectedCharacter.pathing.ClearShownTiles();

					selectedCharacter = selectedUnit as GameCharacter;
					if(actionCharacter != null){
						actionCharacter.pathing.ClearShownTiles();
						actionCharacter = selectedCharacter;
					
					}
						selectedCharacter.pathing.ShowTiles();
				}
			}
		}
	}

	public void StartSelect() {
		ResetTileSelection();
		currState = State.Select;
		StartCoroutine(UpdateState());
	}

	private IEnumerator UpdateState() {
		while(currState != State.End) {
			switch(currState) {
				case State.Select:
					yield return StartCoroutine(HandleSelect());
					break;
				case State.Action:
					yield return StartCoroutine(HandleAction());
					break;
			}
		}
	}

	private IEnumerator HandleSelect() {
		Debug.Log ("Select phase");
		
		yield return StartCoroutine(WaitForCharacterSelect());
		
		actionCharacter = selectedCharacter;

		ResetTileSelection();
		
		currState = State.Action;
		
		yield return null;
	}

	// TODO: THE CURRENT SYSTEM AS IT IS DOES NOT ALLOW SWITCHING BETWEEN ACTION CHARACTERS!!
	
	private IEnumerator HandleAction() {
		Debug.Log ("Action phase");

		actionCharacter.pathing.ShowTiles();
		
		yield return StartCoroutine(WaitForTileSelect());
		
		Tile target = selectedTile;
		if(target.currUnit) {
		} else {
			actionCharacter.pathing.IssueTileMoveOrder(target);
		}
		
		if (actionCharacter.pathing.movesLeft <= 0) 
						currState = State.Select;
				else {
						ResetTileSelection ();
				}
		
		yield return null;
	}

	private IEnumerator WaitForTileSelect() {
		while (selectedTile == null) {
			HandlePlayerControls();
			yield return null;
		}
	}

	private IEnumerator WaitForCharacterSelect() {
		while (selectedCharacter == null) {
			HandlePlayerControls();
			yield return null;
		}
	}
	
	#region Public Interaction

	public void ResetTileSelection() {
		selectedTile = null;
		selectedUnit = null;
		selectedCharacter = null;
	}
	
	
	#endregion
}