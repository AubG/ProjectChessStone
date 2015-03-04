using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using X_UniTMX;

public class ComputerController : Controller
{
	#region State Data


	public enum State {
		None,
		Neutral,
		Selected,
		Action,
		End
	}
	
	public State currState { get; protected set; }


	#endregion

	#region Modular Data


	#endregion

	#region AI Data


	[SerializeField]
	private PathingBrain pathingBrain;

	[SerializeField]
	private TargetingBrain targetingBrain;


	#endregion

	#region Game Data


	private Queue<GameCharacter> characterQueue = new Queue<GameCharacter>();

	private GameUnit selectedTarget;


	#endregion

	#region Initialization
	

	public override void Begin(Player p)
	{
		base.Begin(p);
		
		for(int i = 0; i < currPlayer.characters.Count; i++) {
			characterQueue.Enqueue(currPlayer.characters[i]);
		}
		
		currState = State.Neutral;
		StartCoroutine(UpdateState());
	}


	#endregion

	#region Update Methods


	public override void End ()
	{
		base.End();

		currState = State.End;
	}

	private IEnumerator UpdateState()
	{
		while(currState != State.End) {
			switch(currState)
			{
			case State.Neutral:
				yield return StartCoroutine(HandleNeutralState());
				break;
			case State.Selected:
				yield return StartCoroutine(HandleSelectedState());
				break;
			case State.Action:
				yield return StartCoroutine(HandleActionState());
				break;
			}
		}
	}

	#endregion

	#region Public Interaction
	

	/// <summary>
	/// Assumes the selected character has already been defined.
	/// </summary>
	public void PrimeSpell(int index) {
		if(index < selectedCharacter.spellBox.numSpells) {
			primedSpell = selectedCharacter.GetSpellBySlotIndex(index);
		}
	}
	

	#endregion

	#region Event Handlers


	private IEnumerator HandleNeutralState() {
		if(characterQueue.Count <= 0) {
			End();
			return false;
		}

		selectedCharacter = characterQueue.Dequeue();

		PrimeSpell(0);

		currState = State.Selected;

		yield return null;
	}

	private IEnumerator HandleSelectedState() {
		bool searchComplete = false;
		selectedTarget = null;

		targetingBrain.SearchForTarget(selectedCharacter, value => { 
			selectedTarget = value;
			searchComplete = true;
		});

		while(!searchComplete) yield return null;

		if(selectedTarget == null) {
			if(characterQueue.Count > 0)
				currState = State.Neutral;
			else
				End();

			return false;
		}

		currState = State.Action;
	}

	private IEnumerator HandleActionState() {
		int gridWidth = AstarPath.active.astarData.gridGraph.Width;
		int maxDist = selectedCharacter.move.adjustedMoveRange;

		//Debug.Log ("selected target: " + selectedTarget.name);

		if(selectedCharacter.DistanceToTile(selectedTarget.currTile) > primedSpell.targetRange) {
			pathingBrain.RequestPathTarget(selectedCharacter, selectedTarget.currTile);

			while(!pathingBrain.IsPathLoaded()) {
				yield return null;
			}
			
			while(selectedCharacter.CanMove() && pathingBrain.distanceTraveled < maxDist && !pathingBrain.EndOfPath()) {
				// this functionality belongs in the astarai
				GridNode node = pathingBrain.NextNode();

				int gridY = node.NodeInGridIndex / gridWidth;
				int gridX = node.NodeInGridIndex - gridY * gridWidth;
				Tile t = GameMap.Instance.mainGrid[gridX, gridY];

				//Debug.Log ("Moving " + selectedCharacter.gameName + " to (" + gridX + ", " + gridY + ")");

				selectedCharacter.MoveToTile(t);

				yield return new WaitForSeconds(0.25f);
			}
		}

		if(selectedCharacter.DistanceToTile(selectedTarget.currTile) <= primedSpell.targetRange) {
			bool castFinished = false;
			selectedCharacter.Cast(primedSpell, selectedTarget.currTile, () => castFinished = true);
			primedSpell = null;

			while(!castFinished) yield return null;
		}

		selectedCharacter.Finish();

		currState = State.Neutral;

		CheckFinishedCharacters();
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