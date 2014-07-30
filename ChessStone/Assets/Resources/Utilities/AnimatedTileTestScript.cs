using UnityEngine;
using System.Collections;
using X_UniTMX;

public class AnimatedTileTestScript : MonoBehaviour {

	public Vector2[] CoinsToAnimate;
	public Vector2[] FacesToAnimate;
	public Material defaultMaterial;
	public TextAsset TileMap;
	Map TiledMap;
	public string MapsPath = "Maps";
	

	// Use this for initialization
	void Start () {
		TiledMap = new Map(TileMap, true, MapsPath, this.gameObject, defaultMaterial, 0);

		TileLayer tileLayer = TiledMap.GetTileLayer("Background");
		for (int i = 0; i < CoinsToAnimate.Length; i++)
		{
			AnimatedTile animTile = tileLayer.Tiles[(int)CoinsToAnimate[i].x, (int)CoinsToAnimate[i].y].TileObject.AddComponent<AnimatedTile>();
			animTile.TiledMap = TiledMap;
			animTile.TileFramesGIDs = new int[] { 41, 42, 43, 44, 45, 46, 47, 48 };
			animTile.AnimationFPS = 5;
			animTile.AnimationMode = TileAnimationMode.PING_PONG;
		}
		for (int i = 0; i < FacesToAnimate.Length; i++)
		{
			AnimatedTile animTile = tileLayer.Tiles[(int)FacesToAnimate[i].x, (int)FacesToAnimate[i].y].TileObject.AddComponent<AnimatedTile>();
			animTile.TiledMap = TiledMap;
			animTile.TileFramesGIDs = new int[] { 2, 3, 19, 20, 22, 34 };
			animTile.AnimationFPS = 5;
			animTile.AnimationMode = TileAnimationMode.LOOP;
		}
	}
}
