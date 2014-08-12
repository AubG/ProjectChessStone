using UnityEngine;
using System.Collections.Generic;
using X_UniTMX;

public class GameMap : Singleton<GameMap>
{
	#region Serialized Data


	[SerializeField]
	private string mainTileLayerName = "Terreno";

	[SerializeField]
	private string mainObjectLayerName = "Collider_0";


	#endregion

	#region Data


	public Map map { get; private set; }

	public TileGrid mainGrid { get; private set; }

	public List<GameUnit> mainUnits { get; private set; }

	public List<GameCharacter> mainCharacters { get; private set; }


	#endregion

	public void Initialize(Map map) {
		this.map = map;
		this.mainGrid = map.GetTileLayer(mainTileLayerName).Tiles;

		this.mainUnits = new List<GameUnit> ();
		this.mainCharacters = new List<GameCharacter> ();
	}

	public void InitializeObjects() {
		MapObjectLayer mainObjectLayer = map.GetObjectLayer(mainObjectLayerName);
		
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

	protected GameMap () {}
}