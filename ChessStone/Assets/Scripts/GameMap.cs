using UnityEngine;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Singleton that describes a game map that has all of the main tiles and units on the map.
/// </summary>
public class GameMap : Singleton<GameMap>
{
	#region Serialized Data


	[SerializeField]
	private string mainTileLayerName = "Terreno";

	[SerializeField]
	private string mainObjectLayerName = "Collider_0";


	#endregion

	#region Data


	/// <summary>
	/// The main map instance of the game.
	/// </summary>
	public Map map { get; private set; }

	/// <summary>
	/// The main tile grid of the game.
	/// Can be used to get the 2D array of Tiles for the entire map.
	/// </summary>
	public TileGrid mainGrid { get; private set; }

	/// <summary>
	/// Gets all the units on the map.
	/// </summary>
	/// <value>The main units.</value>
	public List<GameUnit> mainUnits { get; private set; }

	/// <summary>
	/// Gets all the characters on the map.
	/// </summary>
	public List<GameCharacter> mainCharacters { get; private set; }


	#endregion

	#region Initialization


	/// <summary>
	/// Initialize the game map.
	/// </summary>
	public void Initialize(Map map) {
		this.map = map;
		this.mainGrid = map.GetTileLayer(mainTileLayerName).Tiles;

		this.mainUnits = new List<GameUnit> ();
		this.mainCharacters = new List<GameCharacter> ();
	}

	/// <summary>
	/// Initializes the objects on the map.
	/// </summary>
	public void InitializeObjects() {
		MapObjectLayer mainObjectLayer = map.GetObjectLayer(mainObjectLayerName);

		// iterate through all the map objects in the game
		// see if they are units or characters
		// then add them to the mainUnits or mainCharacters Lists
		// and initialize them properly into the scene
		foreach (MapObject o in mainObjectLayer.Objects) {
			GameUnit temp = (o.go != null) ? o.go.GetComponent<GameUnit>() : null;
			if(temp) {
				mainUnits.Add(temp);
				
				Vector2 tileIndices = map.WorldPointToTileIndex(temp.transform.position);
				mainGrid[(int)tileIndices.x, (int)tileIndices.y].currUnit = temp;
				
				if(temp is GameCharacter) {
					GameCharacter tempChar = temp as GameCharacter;
					mainCharacters.Add(tempChar);
					tempChar.pathing.SetTile((int)tileIndices.x, (int)tileIndices.y);
				}
			}
		}
	}

	/// <summary>
	/// Security to ensure that the Game Map Singleton is never instantiated publicly.
	/// </summary>
	protected GameMap () {}


	#endregion
}