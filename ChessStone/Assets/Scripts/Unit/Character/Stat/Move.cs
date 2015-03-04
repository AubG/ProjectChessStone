using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Each GameCharacter must have a pathing script associated with it that
/// handles tile movement and interaction.
/// </summary>
public class Move : MonoBehaviour
{
	
	#region Unit Data
	

	[SerializeField]
	private Health health;

	[SerializeField]
	private Pathing pathing;
	
	
	#endregion

	#region Range Data


	public int movesLeft { get; private set; }
	public int baseMoveRange { get; private set; }
	public int adjustedMoveRange { get; private set; }


	#endregion

	#region Public Interaction
	

	public void MoveToTile(Tile target) {
		if(!CanMoveToTile(target)) {
			Debug.Log ("Error: Could not move to that tile!");
			return;
		}

		Tile temp = pathing.currTile;
		pathing.SetTile (target);
		if(health.currSize >= health.maxSize && health.currSize > 1)
			health.RemoveTail();

		health.AddLink(temp);

		movesLeft--;
	}

	public void MoveToTile(int tileX, int tileY) {
		MoveToTile(GameMap.Instance.mainGrid[tileX, tileY]);
	}


	#endregion

	#region Interaction
	

	public void SetBaseMoveRange(int range) {
		adjustedMoveRange = baseMoveRange = range;
	}

	public void SetAdjustedMoveRange(int range) {
		adjustedMoveRange = range;
	}

	public bool CanMove() {
		return movesLeft > 0;
	}

	public bool CanMoveToTile(Tile t) {
		return CanMove() && t.currUnit == null && pathing.DistanceToTile(t) <= movesLeft;
	}

	public void ResetMoves() {
		movesLeft = adjustedMoveRange;
	}


	#endregion
}