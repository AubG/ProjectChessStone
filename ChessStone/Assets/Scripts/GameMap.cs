using UnityEngine;
using X_UniTMX;

public class GameMap : Singleton<GameMap>
{
	public Map map { get; private set; }

	public TileGrid mainGrid { get; private set; }

	public void Initialize(Map map) {
		Debug.Log ("Game Map successfully initialized!");

		this.map = map;
	}

	protected GameMap () {}
}