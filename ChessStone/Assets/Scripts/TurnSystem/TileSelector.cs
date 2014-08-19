using UnityEngine;
using System.Collections;
using X_UniTMX;

public class TileSelector : MonoBehaviour
{
	#region State Data


	public enum State {
		None,
		Select,
		Action,
		End
	}

	public State currState { get; private set; }


	#endregion

	#region Graphics Data


	[SerializeField]
	private UILabel unitNameLabel;

	[SerializeField]
	private UILabel characterLabel;

	[SerializeField]
	private GameObject abilitiesParent;

	[SerializeField]
	private GameObject abilitiesButtonPrefab;


	#endregion

	#region Game Data


	public Tile selectedTile { get; private set; }
	public GameUnit selectedUnit { get; private set; }
	public GameCharacter selectedCharacter { get; private set; }

	public GameCharacter actionCharacter { get; private set; }


	#endregion

	#region Initialization


	public void StartSelect()
	{
		ResetTileSelection();
		currState = State.Select;
		StartCoroutine(UpdateState());
	}


	#endregion

	#region Update Methods


	private IEnumerator UpdateState()
	{
		while(currState != State.End)
		{
			switch(currState)
			{
				case State.Select:
					yield return StartCoroutine(HandleSelect());
					break;
				case State.Action:
					yield return StartCoroutine(HandleAction());
					break;
			}
		}
	}

	private IEnumerator HandleSelect()
	{
		Debug.Log ("Select phase");
		
		yield return StartCoroutine(WaitForCharacterSelect());
		
		actionCharacter = selectedCharacter;

		ResetTileSelection();
		
		currState = State.Action;
		
		yield return null;
	}

	private IEnumerator HandleAction()
	{
		Debug.Log ("Action phase");

		actionCharacter.pathing.ShowTiles();
		
		yield return StartCoroutine(WaitForTileSelect());
		
		Tile target = selectedTile;
		if(target.currUnit)
		{
		}
		else
		{
			actionCharacter.pathing.IssueTileMoveOrder(target);
		}
		
		if (actionCharacter.pathing.movesLeft <= 0) 
		{
			currState = State.Select;
		}
		else 
		{
			ResetTileSelection ();
		}
		
		yield return null;
	}

	private void UpdatePlayerControls ()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Map gameMap = GameMap.Instance.map;
			
			Vector2 targetIndices = gameMap.WorldPointToTileIndex(worldPoint);
			
			selectedTile = GameMap.Instance.mainGrid[(int)targetIndices.x, (int)targetIndices.y];
			
			if(selectedTile.currUnit)
			{
				selectedUnit = selectedTile.currUnit;

				if(selectedUnit is GameCharacter)
				{
					if(selectedCharacter != null) selectedCharacter.pathing.ClearShownTiles();
					
					selectedCharacter = selectedUnit as GameCharacter;
					if(actionCharacter != null)
					{
						actionCharacter.pathing.ClearShownTiles();
						actionCharacter = selectedCharacter;
						
					}
					
					selectedCharacter.pathing.ShowTiles();
				}

				UpdateLabels(selectedUnit);
			}
		}
	}


	#endregion

	#region Helpers


	private IEnumerator WaitForTileSelect() {
		while (selectedTile == null) {
			UpdatePlayerControls();
			yield return null;
		}
	}
	
	private IEnumerator WaitForCharacterSelect() {
		while (selectedCharacter == null) {
			UpdatePlayerControls();
			yield return null;
		}
	}

	private void UpdateLabels(GameUnit labelUnit) {
		this.unitNameLabel.text = "[66FA33]" + labelUnit.gameName + "[-]";

		if(labelUnit is GameCharacter) {
			// character data
			GameCharacter labelCharacter = labelUnit as GameCharacter;

			string ctext = "";
			ctext += "Health: " + labelCharacter.health.currentValue + " / " + labelCharacter.health.adjustedMaxValue + "\n";
			if(labelCharacter.mana.adjustedMaxValue > 0) 
				ctext += "Mana: " + labelCharacter.mana.currentValue + " / " + labelCharacter.mana.adjustedMaxValue + "\n";

			characterLabel.text = ctext;

			// abilities
			int i, il;
			for(i = 0, il = abilitiesParent.transform.childCount; i < il; i++) Destroy (abilitiesParent.transform.GetChild(i).gameObject);
			for(i = 0, il = labelCharacter.spellBox.spells.Length; i < il; i++) {
				BaseSpell spellData = labelCharacter.spellBox.spells[i].baseSpell;
				GameObject spellButton = NGUITools.AddChild(abilitiesParent, abilitiesButtonPrefab);
				UILabel spellLabel = spellButton.GetComponentInChildren<UILabel>();
				if(spellLabel == null) {
					Debug.LogError("Critical error: One of the spell buttons does not have a UILabel script attached!");
					return;
				} else {
					spellLabel.text = spellData.name;
				}
			}
		}
	}


	#endregion
	
	#region Public Interaction


	public void ResetTileSelection() {
		selectedTile = null;
		selectedUnit = null;
		selectedCharacter = null;
	}
	
	
	#endregion
}