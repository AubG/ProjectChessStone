using UnityEngine;
using X_UniTMX;

public class GameMap : Singleton<GameMap>
{
	public Map map { get; private set; }

	public TileGrid mainGrid { get; private set; }

	public void Initialize(Map map) {
		this.map = map;

		this.mainGrid = map.GetTileLayer("Terreno").Tiles;
	}

	protected GameMap () {}
}