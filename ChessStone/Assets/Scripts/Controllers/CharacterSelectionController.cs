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
	private UICharacterListCard characterListCard;

	[SerializeField]
	private UIUnitCard unitCard;

	[SerializeField]
	private TileHighlighter tileHighlighter;

	[SerializeField]
	private Button beginButton;

	
	#endregion
	
	#region Game Data


	private List<Tile> spawnTiles = new List<Tile>();

	private bool spawnSelected = false;

	
	#endregion

	#region Input Data


	[SerializeField]
	private TileSelector tileSelector;


	#endregion
	
	#region Initialization


	void Awake() {
		characterListCard.AddCharacterButtonCallback(BuildCharacter);
		characterListCard.Begin();
	}

	public override void Begin(Player p)
	{
		base.Begin(p);

		unitCard.AddSpellButtonCallback(PrimeSpell);

		characterListCard.Show();

		spawnSelected = false;
		
		DefineSpawnTiles();

		currState = State.Neutral;
		p.ResetCharacters();
		tileSelector.clickCallback = OnTileClick;
		tileSelector.Begin();

		SelectTile(GameMap.Instance.spawnTiles[0]);
	}

	private void DefineSpawnTiles() {
		this.spawnTiles = GameMap.Instance.spawnTiles;
	}

	
	#endregion
	
	#region Update Methods


	public override void End() {
		base.End();

		beginButton.GetComponent<CanvasGroup>().alpha = 0;
		beginButton.interactable = false;

		characterListCard.Hide();

		tileSelector.End();
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

		Tile temp = selectedUnit.currTile;
		GameMap.Instance.RemoveUnit(selectedUnit);
		GameCharacter c = CharacterBuilder.Instance.BuildCharacter(id, currPlayer, temp);
		SelectTile(temp);
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

	/// <summary>
	/// Assumes that a unit exists at the tile, and selects it.
	/// </summary>
	public void SelectTile(Tile t) {
		spawnSelected = false;

		if(spawnTiles.Contains(t)) spawnSelected = true;

		selectedUnit = t.currUnit;

		tileHighlighter.ClearHighlightedTiles();
		
		if(selectedUnit.unitType == UnitType.Character) {
			selectedCharacter = (GameCharacter)selectedUnit;
		}

		unitCard.UpdateLabels(selectedUnit, false);
		tileHighlighter.HighlightTile(t, HighlightStyle.Border, Color.green);
	}
	
	
	#endregion

	#region Event Handlers
	
	
	private void OnTileClick(Tile t) {
		if(t.currUnit != null) {
			primedSpell = null;
			SelectTile(t);
		}
	}
	
	
	#endregion
}