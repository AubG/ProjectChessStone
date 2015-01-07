using UnityEngine;
using System.Collections;
using X_UniTMX;

public enum UnitType {
	None,
	Link,
	Character
}

public class GameUnit : MonoBehaviour
{
	#region State Data
	

	public UnitType unitType { get; protected set; }


	#endregion

	#region Modular Data


	[SerializeField]
	private string _gameName;
	public string gameName {
		get { return _gameName; }
		set { _gameName = value; }
	}

	[SerializeField]
	private string _gameDescription;
	public string gameDescription {
		get { return _gameDescription; }
		set { _gameDescription = value; }
	}

	// TODO: completely encapsulate pathing
	[SerializeField]
	private Pathing _pathing;
	public Pathing pathing { 
		get { return _pathing; } 
		protected set { _pathing = value; }
	}


	#endregion

	#region Game Data

	
	public SpriteRenderer avatarRenderer;

	public Tile currTile {
		get { return pathing.currTile; }
	}

	public bool zombie { get; private set; }


	#endregion

	#region Interaction


	/// <summary>
	/// Immediately moves to a new tile.
	/// </summary>
	public void SetTile(Tile moveTile) {
		pathing.SetTile(moveTile);
	}
	
	/// <summary>
	/// Immediately moves to a new tile defined by its x & y indices 
	/// on the main tile grid.
	/// </summary>
	public void SetTile(int tileX, int tileY) {
		pathing.SetTile(tileX, tileY);
	}

	public Tile GetTile() {
		return pathing.currTile;
	}
	
	public int GetTileX() {
		return pathing.currTile.x;
	}
	
	public int GetTileY() {
		return pathing.currTile.y;
	}

	public int DistanceToTile(Tile t) {
		return pathing.DistanceToTile(t);
	}


	#endregion
}