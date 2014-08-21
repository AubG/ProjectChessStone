using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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


<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
	// Temporary Stuff
	[SerializeField]
	private GameObject rangePrefab;
	
	[SerializeField]
	private GameObject adjPrefab;

	// GUI Stuff
<<<<<<< HEAD
=======
=======
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
	[SerializeField]
	private UILabel unitNameLabel;

	[SerializeField]
	private UILabel characterLabel;

<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
	[SerializeField]
	private GameObject abilitiesParent;

	[SerializeField]
	private GameObject abilitiesButtonPrefab;

	// Continuation of Temporary stuff
	private List<GameObject> adjTileObjs;
	private List<GameObject> rangeTileObjs;
	
<<<<<<< HEAD
=======
=======
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7

	#endregion

	#region Game Data


<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
	/// <summary>
	/// Whether the tile selector is currently active.
	/// </summary>
	public bool selectorActive { get; private set; }

	// Temporary storage for selections
<<<<<<< HEAD
=======
=======
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
	public Tile selectedTile { get; private set; }
	public GameUnit selectedUnit { get; private set; }
	public GameCharacter selectedCharacter { get; private set; }

	// Cached selections + their stuffies
	public GameCharacter actionCharacter { get; private set; }
<<<<<<< HEAD
	public GameSpell primedSpell { get; private set; }
=======
<<<<<<< HEAD
	public GameSpell primedSpell { get; private set; }
=======
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7


	#endregion

	#region Initialization

<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7

	void Start() {
		rangeTileObjs = new List<GameObject>();
		adjTileObjs = new List<GameObject>();
	}
<<<<<<< HEAD
=======
=======
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7

	public void StartSelect()
	{
		ResetTileSelection();
		currState = State.Select;
		selectorActive = true;
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
		
<<<<<<< HEAD
		yield return StartCoroutine(WaitForSelect());
=======
		yield return StartCoroutine(WaitForCharacterSelect());
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
		
		actionCharacter = selectedCharacter;
		
		ResetTileSelection();
		
		currState = State.Action;
		
		yield return null;
	}

	private IEnumerator HandleAction()
	{
		Debug.Log ("Action phase");

<<<<<<< HEAD
		ShowTiles();

		yield return StartCoroutine(WaitForSelect());
		if(primedSpell != null)
		{
			primedSpell.Cast(actionCharacter, selectedTile);
		}
		else
		{
			actionCharacter.pathing.IssueTileMoveOrder(selectedTile);
		}

		ResetTileSelection();
		
		if (primedSpell != null || actionCharacter.pathing.tileRange <= 0) 
		{
			currState = State.Select;

			actionCharacter = null;
			primedSpell = null;
		}
		else 
		{
=======
<<<<<<< HEAD
		ShowTiles();

		Tile target = null;
		Debug.Log (primedSpell);
		if(primedSpell != null)
		{
			yield return StartCoroutine(WaitForViableSpellTargetSelect());
			Debug.Log (target.currUnit);
			target = selectedTile;
			primedSpell.Cast(actionCharacter, target);
		}
		else
		{
			yield return StartCoroutine(WaitForTileSelect());
			target = selectedTile;
				actionCharacter.pathing.IssueTileMoveOrder(target);
		}
		
		if (primedSpell != null || actionCharacter.pathing.tileRange <= 0) 
		{
			ClearShownTiles();
=======
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
>>>>>>> origin/master
			currState = State.Select;
		}
		else 
		{
<<<<<<< HEAD
			Debug.Log ("ffff");
=======
>>>>>>> origin/master
			ResetTileSelection ();
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
		}
		
		yield return null;
	}

<<<<<<< HEAD
	private void UpdateSelection ()
=======
<<<<<<< HEAD
	private void UpdateSelection ()
=======
	private void UpdatePlayerControls ()
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
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
<<<<<<< HEAD
					selectedCharacter = selectedUnit as GameCharacter;
				}

				if(primedSpell == null) UpdateLabels(selectedUnit);
=======
<<<<<<< HEAD
					selectedCharacter = selectedUnit as GameCharacter;
=======
					if(selectedCharacter != null) selectedCharacter.pathing.ClearShownTiles();
					
					selectedCharacter = selectedUnit as GameCharacter;
					if(actionCharacter != null)
					{
						actionCharacter.pathing.ClearShownTiles();
						actionCharacter = selectedCharacter;
						
					}
					
					selectedCharacter.pathing.ShowTiles();
>>>>>>> origin/master
				}

				UpdateLabels(selectedUnit);
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
			}
		}
	}


	#endregion

	#region Helpers
<<<<<<< HEAD
	

	private IEnumerator WaitForSelect() {
		bool selectionComplete = false;

		while(true) {
			if(selectedTile == null) UpdateSelection();

			if(selectedTile != null) {
				switch(currState) {
					case State.Select:
						// if the selector state is in Select mode then we must select a viable character
						if(selectedCharacter != null) selectionComplete = true;
						break;
					case State.Action:
						// if a spell has been primd then we need to make sure its a viable target for the spell
						if(primedSpell != null) {
							BaseSpell.TargetingData targeting = primedSpell.baseSpell.targetingData;
							if((targeting.allowCharacters) && selectedTile.currUnit != null) {
								if(targeting.allowCharacters && selectedTile.currUnit is GameCharacter) selectionComplete = true;
							}
						} else {
						// otherwise just ensure that a viable tile has been selected to move to
							if(selectedTile != null) selectionComplete = true;
						}
						break;
				}

				if(selectionComplete) break;
			}

=======


	private IEnumerator WaitForTileSelect() {
		while (selectedTile == null) {
<<<<<<< HEAD
			UpdateSelection();
=======
			UpdatePlayerControls();
>>>>>>> origin/master
			yield return null;
		}
	}
	
	private IEnumerator WaitForCharacterSelect() {
		while (selectedCharacter == null) {
<<<<<<< HEAD
			UpdateSelection();
			yield return null;
		}
	}

	private IEnumerator WaitForViableSpellTargetSelect() {
		while(true) {
			UpdateSelection();

			if(selectedTile != null) {
				BaseSpell.TargetingData targeting = primedSpell.baseSpell.targetingData;
				if((targeting.allowCharacters) && selectedTile.currUnit != null) {
					if(targeting.allowCharacters && selectedTile.currUnit is GameCharacter) break;
				}
			}

=======
			UpdatePlayerControls();
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
			yield return null;
		}
	}

	private void UpdateLabels(GameUnit labelUnit) {
		this.unitNameLabel.text = "[66FA33]" + labelUnit.gameName + "[-]";

		if(labelUnit is GameCharacter) {
<<<<<<< HEAD
			// character data
=======
<<<<<<< HEAD
			// character data
=======
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
			GameCharacter labelCharacter = labelUnit as GameCharacter;

			string ctext = "";
			ctext += "Health: " + labelCharacter.health.currentValue + " / " + labelCharacter.health.adjustedMaxValue + "\n";
			if(labelCharacter.mana.adjustedMaxValue > 0) 
				ctext += "Mana: " + labelCharacter.mana.currentValue + " / " + labelCharacter.mana.adjustedMaxValue + "\n";

			characterLabel.text = ctext;
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7

			// abilities
			int i, il;
			for(i = 0, il = abilitiesParent.transform.childCount; i < il; i++) Destroy (abilitiesParent.transform.GetChild(i).gameObject);
			for(i = 0, il = labelCharacter.spellBox.spells.Length; i < il; i++) {
				GameSpell spell = labelCharacter.spellBox.spells[i];
				BaseSpell spellData = spell.baseSpell;
				GameObject spellButton = NGUITools.AddChild(abilitiesParent, abilitiesButtonPrefab);
				UILabel spellLabel = spellButton.GetComponentInChildren<UILabel>();
				if(spellLabel == null) {
					Debug.LogError("Critical error: One of the spell buttons does not have a UILabel script attached!");
					return;
				} else {
					spellLabel.text = spellData.name;
					ActivateSpellOnClick activateScript = spellButton.GetComponent<ActivateSpellOnClick>();
					activateScript.currSelector = this;
					activateScript.DefineSpell(spellData.name);
				}
			}
<<<<<<< HEAD
=======
=======
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
		}
	}


	#endregion
	
	#region Public Interaction


	public void ResetTileSelection() {
		selectedTile = null;
		selectedUnit = null;
		selectedCharacter = null;

		ClearShownTiles();
	}

	/// <summary>
	/// Assumes the actionCharacter has already been defined.
	/// </summary>
	public void PrimeSpell(string spellName) {
		GameSpell spell = actionCharacter.spellBox.GetSpell(spellName);
		primedSpell = spell;
		actionCharacter.pathing.ForceRecomputeTiles(primedSpell.baseSpell.tileRange);
		ShowTiles();
<<<<<<< HEAD
=======
		Debug.Log (primedSpell);
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
	}

	/// <summary>
	/// Assumes the actionCharacter has been initialized.
	/// Shows the tiles in range and adjacent tiles.
	/// </summary>
	public void ShowTiles() {
		ClearShownTiles();

<<<<<<< HEAD
		foreach(Tile t in actionCharacter.pathing.rangeTiles) {
			Transform tileTransform = t.TileObject.transform;
			Vector3 newPos = tileTransform.position + new Vector3(t.TileSet.WorldDims.x * 0.5f, t.TileSet.WorldDims.y * 0.5f, 0);
				
			if(actionCharacter.pathing.adjTiles.Contains(t))
				adjTileObjs.Add(Instantiate(adjPrefab, newPos, Quaternion.identity) as GameObject);
			else
				rangeTileObjs.Add(Instantiate(rangePrefab, newPos, Quaternion.identity) as GameObject);
		}
=======
			foreach(Tile t in actionCharacter.pathing.rangeTiles) {
				Transform tileTransform = t.TileObject.transform;
				Vector3 newPos = tileTransform.position + new Vector3(t.TileSet.WorldDims.x * 0.5f, t.TileSet.WorldDims.y * 0.5f, 0);
				
				if(actionCharacter.pathing.adjTiles.Contains(t))
					adjTileObjs.Add(Instantiate(adjPrefab, newPos, Quaternion.identity) as GameObject);
				else
					rangeTileObjs.Add(Instantiate(rangePrefab, newPos, Quaternion.identity) as GameObject);
			}
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
	}

	/// <summary>
	/// Clears the shown in-range and adjacent tiles.
	/// </summary>
	public void ClearShownTiles() {
		foreach(GameObject g in rangeTileObjs) Destroy (g);
		foreach(GameObject g in adjTileObjs) Destroy (g);
		
		rangeTileObjs.Clear();
		adjTileObjs.Clear();
	}
	
	
	#endregion
}