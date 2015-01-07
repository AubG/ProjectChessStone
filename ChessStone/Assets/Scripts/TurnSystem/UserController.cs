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
	private UIUnitData unitData;

	[SerializeField]
	private TileHighlighter tileHighlighter;
	

	#endregion

	#region Initialization


	public override void Begin(Player p)
	{
		base.Begin(p);

		unitData.AddSpellButtonCallback(PrimeSpell);
		unitData.noActionCallback = OnCharacterFinished;

		p.ResetCharacters();
		currState = State.Neutral;

		TileSelector.Instance.clickCallback = OnTileClick;
		TileSelector.Instance.StartSelect();

		if(p.characters.Count > 0) SelectTile(p.characters[0].currTile);
	}


	#endregion

	#region Update Methods
	

	public override void HandleEnd() {
		base.HandleEnd();

		currState = State.End;
		TileSelector.Instance.StopSelect();

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

	public void SelectTile(Tile t) {
		selectedTile = t;

		if(selectedTile.currUnit != null) {
			selectedUnit = selectedTile.currUnit;

			tileHighlighter.ClearHighlightedTiles();

			switch(selectedUnit.unitType) {
			case UnitType.Character:
				selectedCharacter = (GameCharacter)selectedUnit;
				tileHighlighter.HighlightTilesInPathingRange(selectedTile, selectedCharacter.move.movesLeft, Color.white, true);
				break;
			case UnitType.Link:
				GameUnitLink temp = (GameUnitLink)selectedUnit;
				SelectTile(temp.head.currTile);
				return;
			}

			tileHighlighter.HighlightTile(selectedTile, HighlightStyle.Border, Color.green);
			unitData.UpdateLabels(selectedUnit, true);
		}
	}


	#endregion

	#region Event Handlers


	private void OnTileClick(Tile t) {
		if(selectedTile == null) {
			SelectTile(t);
		} else if(selectedUnit != null) {
			bool changeSelection = true;

			if(selectedCharacter != null && selectedCharacter.owningPlayer == currPlayer && !selectedCharacter.finished) {
				// check if a spell is ready to be cast
				if(primedSpell != null) {
				   if(selectedCharacter.CanCastOnTarget(primedSpell, t)) {
						selectedCharacter.Cast(primedSpell, t);
						primedSpell = null;
						OnCharacterFinished();
					}
					changeSelection = false;
				} else if(selectedCharacter.DistanceToTile(t) == 1 && selectedCharacter.CanMoveToTile(t)) {
					selectedCharacter.MoveToTile(t);
					SelectTile(selectedCharacter.currTile);
					changeSelection = false;
					primedSpell = null;
				}
			}

			if(changeSelection) {
				primedSpell = null;
				SelectTile(t);
			}
		}
	}

	private void OnCharacterFinished() {
		selectedCharacter.Finish();
		SelectTile(selectedCharacter.currTile);

		CheckFinishedCharacters();
	}


	#endregion

	#region Helpers


	protected override void CheckFinishedCharacters() {
		if(currPlayer.CheckFinishedCharacters()) {
			HandleEnd();
		}
	}

	
	#endregion
}