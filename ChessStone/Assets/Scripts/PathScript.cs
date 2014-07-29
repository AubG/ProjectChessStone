using UnityEngine;
using System.Collections.Generic;
using X_UniTMX;

public class PathScript : MonoBehaviour
{
	#region Data


	/// <summary>
	/// The tile that is being stood upon.
	/// </summary>
	public Tile currTile { get; set; }

	/// <summary>
	/// A list of the adjacent tiles to the current tile.
	/// </summary>
	public List<Tile> adjacentTiles { get; private set; }


	#endregion

	#region Public Interaction


	/// <summary>
	/// Moves to a new tile.
	/// </summary>
	public void HandleMove(Tile moveTile) {
		currTile = moveTile;

		ComputeAdjacentTiles();
	}

	/// <summary>
	/// Moves in a horizontal direction with a specified range.
	/// </summary>
	public void HandleMove(int range, Vector2 moveDir) {
		//Tile moveTile = GameMap.In
	}


	#endregion

	#region Helpers


	private void ComputeAdjacentTiles() {

	}


	#endregion
}