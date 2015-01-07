using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using X_UniTMX;

public class CharacterSelectionController : Controller {

	#region State Data


	public enum State {
		None,
		Neutral,
		Selected,
		End
	}
	
	public State currState { get; protected set; }
	
	
	#endregion

	#region Graphics Data


	[SerializeField]
	private UICharacterList characterList;

	[SerializeField]
	private UIUnitData unitData;

	[SerializeField]
	private TileHighlighter tileHighlighter;

	[SerializeField]
	private Button beginButton;

	
	#endregion
	
	#region Game Data


	private List<Tile> spawnTiles = new List<Tile>();

	private bool spawnSelected = false;

	
	#endregion
	
	#region Initialization
	

	public override void Begin(Player p)
	{
		base.Begin(p);

		characterList.AddCharacterButtonCallback(BuildCharacter);
		unitData.AddSpellButtonCallback(PrimeSpell);

		spawnSelected = false;
		
		DefineSpawnTiles();

		currState = State.Neutral;
		p.ResetCharacters();
		TileSelector.Instance.clickCallback = OnTileClick;
		TileSelector.Instance.StartSelect();

		SelectTile(GameMap.Instance.spawnTiles[0]);
	}

	private void DefineSpawnTiles() {
		this.spawnTiles = GameMap.Instance.spawnTiles;
	}

	
	#endregion
	
	#region Update Methods


	public override void HandleEnd() {
		base.HandleEnd();

		beginButton.GetComponent<CanvasGroup>().alpha = 0;
		beginButton.interactable = false;

		characterList.Hide();

		TileSelector.Instance.StopSelect();
		currState = State.End;

		tileHighlighter.ClearHighlightedTiles();

		List<Tile> spawnTiles = GameMap.Instance.spawnTiles;
		for(int i = 0; i < spawnTiles.Count; i++) {
			if(spawnTiles[i].currUnit.gameName == "Spawn Point")
				GameMap.Instance.RemoveUnit(spawnTiles[i].currUnit);
		}
	}
	
	
	#endregion
	
	#region Interaction


	public void BuildCharacter(int id) {
		if(!spawnSelected) return;

		GameMap.Instance.RemoveUnit(selectedUnit);
		GameCharacter c = CharacterManager.Instance.BuildCharacter(id, currPlayer, selectedTile);
		c.SetTile(selectedTile);

		SelectTile(selectedTile);
	}
	
	/// <summary>
	/// Assumes the selected character has already been defined.
	/// </summary>
	public void PrimeSpell(SpellData spell) {
		primedSpell = spell;
		tileHighlighter.HighlightTile(selectedCharacter.currTile, HighlightStyle.Border, Color.green);
	}
	
	/// <summary>
	/// Assumes the selected character has already been defined.
	/// </summary>
	public void PrimeSpell(int spellId) {
		PrimeSpell(selectedCharacter.GetSpell(spellId));
	}
	
	/// <summary>
	/// Assumes the selected character has already been defined.
	/// </summary>
	public void PrimeSpellBySlotIndex(int index) {
		PrimeSpell(selectedCharacter.GetSpellBySlotIndex(index));
	}

	public void SelectTile(Tile t) {
		spawnSelected = false;

		selectedTile = t;
		if(spawnTiles.Contains(selectedTile)) spawnSelected = true;

		if(selectedTile.currUnit != null) {
			selectedUnit = selectedTile.currUnit;
			
			if(selectedUnit is GameCharacter) {
				selectedCharacter = selectedUnit as GameCharacter;
			}

			unitData.UpdateLabels(selectedUnit, false);
			tileHighlighter.ClearHighlightedTiles();
			tileHighlighter.HighlightTile(selectedTile, HighlightStyle.Border, Color.green);
		}
	}
	
	
	#endregion

	#region Event Handlers
	
	
	private void OnTileClick(Tile t) {
		if(selectedTile == null) {
			SelectTile(t);
		} else if(selectedUnit != null) {
			bool changeSelection = true;
			
			if(changeSelection) {
				primedSpell = null;
				SelectTile(t);
			}
		}
	}
	
	
	#endregion
}