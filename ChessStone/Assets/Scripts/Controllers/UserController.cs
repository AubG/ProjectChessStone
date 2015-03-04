using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;

public class UserController : Controller
{
	#region State Data


	public enum State {
		Neutral,
		Active,
		End
	}
	
	public State currState { get; protected set; }


	#endregion

	#region Graphics Data


	[SerializeField]
	private UIUnitCard unitCard;

	[SerializeField]
	private TileHighlighter tileHighlighter;
	

	#endregion

	#region Input Data


	[SerializeField]
	private TileSelector tileSelector;


	#endregion

	#region Initialization


	public override void Begin(Player p)
	{
		base.Begin(p);

		unitCard.AddSpellButtonCallback(PrimeSpell);
		unitCard.noActionCallback = FinishCharacter;

		p.ResetCharacters();
		currState = State.Neutral;

		tileSelector.clickCallback = OnTileClick;
		tileSelector.Begin();

		if(p.characters.Count > 0) SelectTile(p.characters[0].currTile);
	}


	#endregion

	#region Update Methods
	

	public override void End() {
		base.End();

		currState = State.End;
		tileSelector.End();
		unitCard.ClearLabels();

		tileHighlighter.ClearHighlightedTiles();
	}


	#endregion

	#region Interaction
	

	/// <summary>
	/// Assumes the selected character has already been defined.
	/// </summary>
	public void PrimeSpell(SpellData spell) {
		primedSpell = spell;
		tileHighlighter.ClearHighlightedTiles();
		tileHighlighter.HighlightTile(selectedCharacter.currTile, HighlightStyle.Border, Color.green);
		tileHighlighter.HighlightTilesInRange(selectedCharacter.currTile, primedSpell.targetRange, new Color(0f, 0.5f, 1f), false);
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

	public void ClearPrimedSpell() {
		primedSpell = null;
		tileHighlighter.ClearHighlightedTiles();
	}

	/// <summary>
	/// Assumes that a unit exists at the tile, and selects it.
	/// </summary>
	public void SelectTile(Tile t) {
		selectedCharacter = null;

		selectedUnit = t.currUnit;

		tileHighlighter.ClearHighlightedTiles();

		switch(selectedUnit.unitType) {
		case UnitType.Character:
			selectedCharacter = (GameCharacter)selectedUnit;
			if(!selectedCharacter.finished)
				tileHighlighter.HighlightTilesInPathingRange(t, selectedCharacter.move.movesLeft, Color.white, true);
			break;
		case UnitType.Link:
			GameUnitLink temp = (GameUnitLink)selectedUnit;
			SelectTile(temp.head.currTile);
			return;
		}

		tileHighlighter.HighlightTile(t, HighlightStyle.Border, Color.green);
		unitCard.UpdateLabels(selectedUnit, true);
	}


	#endregion

	#region Event Handlers


	private void OnTileClick(Tile t) {
		if(selectedUnit != null) {
			bool changeSelection = false;
			if(selectedUnit.unitType == UnitType.Character && selectedCharacter.owningPlayer == currPlayer && !selectedCharacter.finished) {
				// check if a spell is ready to be cast
				if(primedSpell != null) {
				   if(selectedCharacter.CanCastOnTarget(primedSpell, t)) {
						selectedCharacter.Cast(primedSpell, t);
						FinishCharacter(selectedCharacter);
					}
				} else if(selectedCharacter.DistanceToTile(t) == 1 && selectedCharacter.CanMoveToTile(t)) {
					selectedCharacter.MoveToTile(t);
					SelectTile(selectedCharacter.currTile);
					primedSpell = null;
				} else if(t.currUnit != null) {
					changeSelection = true;
				}
			} else if(t.currUnit != null) {
				changeSelection = true;
			}

			if(changeSelection) {
				primedSpell = null;
				SelectTile(t);
			}
		}
	}

	private void FinishCharacter(GameCharacter character) {
		character.Finish();
		primedSpell = null;
		SelectTile(character.currTile);

		CheckFinishedCharacters();

		GameCharacter next = currPlayer.GetNextUnfinishedCharacter();
		if(next != null) StartCoroutine(DelayedSelectCharacter(selectedCharacter, next));
	}

	private IEnumerator DelayedSelectCharacter(GameCharacter original, GameCharacter character) {
		yield return new WaitForSeconds(0.1f);

		if(currState != State.End && original == selectedCharacter)
			SelectTile(character.currTile);
	}


	#endregion

	#region Helpers


	protected override void CheckFinishedCharacters() {
		if(currPlayer.CheckFinishedCharacters()) {
			End();
		}
	}

	
	#endregion
}