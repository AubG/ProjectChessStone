using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Each GameCharacter must have a pathing script associated with it that
/// handles tile movement and interaction.
/// </summary>
public class Pathing : MonoBehaviour
{
	#region Data


	private GameUnit _currUnit;
	public GameUnit currUnit {
		get {
			if(!_currUnit) 
				_currUnit = GetComponent<GameUnit>();

			return _currUnit;
		}
	}

	/// <summary>
	/// The tile that is being stood upon.
	/// </summary>
	public Tile currTile { get; private set; }


	#endregion

	#region Public Interaction


	/// <summary>
	/// Immediately moves to a new tile.
	/// </summary>
	public void SetTile(Tile moveTile) {
		if(currTile != null) currTile.currUnit = null;

		currTile = moveTile;
		currTile.currUnit = currUnit;
		
		Map map = GameMap.Instance.map; 
		// set the position to the tile's position
		Vector3 tilePos = currTile.centerPos;
		transform.position = tilePos;
	}

	/// <summary>
	/// Immediately moves to a new tile defined by its x & y indices 
	/// on the main tile grid.
	/// </summary>
	public void SetTile(int tileX, int tileY) {
		SetTile(GameMap.Instance.mainGrid[tileX, tileY]);
	}

	/// <summary>
	/// The Manhattan distance to the tile.
	/// </summary>
	public int DistanceToTile(Tile t) {
		return Mathf.Abs (t.x - currTile.x) + Mathf.Abs (t.y - currTile.y);
	}


	#endregion
}