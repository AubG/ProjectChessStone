using UnityEngine;
using System.Collections.Generic;
using X_UniTMX;

public class MapLoader : MonoBehaviour {

	public Material defaultMaterial;
	public int sortingOrder = 0;
	public TextAsset[] Maps;
	public int CurrentMap = 0;

	Map TiledMap;
	public string MapsPath = "Maps";

	public CameraController _camera;

	// Use this for initialization
	void Start () {
		LoadMap();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.LeftArrow))
		{
			CurrentMap--;
			if (CurrentMap < 0)
				CurrentMap = Maps.Length - 1;
			LoadMap();
		}
		if (Input.GetKeyDown(KeyCode.RightArrow))
		{
			CurrentMap++;
			if (CurrentMap > Maps.Length - 1)
				CurrentMap = 0;
			LoadMap();
		}
	}

	void UnloadCurrentMap()
	{
		var children = new List<GameObject>();
		foreach (Transform child in this.transform) children.Add(child.gameObject);
		children.ForEach(child => Destroy(child));

		MeshFilter filter = GetComponent<MeshFilter>();
		if (filter)
			Destroy(filter);
	}

	void LoadMap()
	{
		UnloadCurrentMap();
		TiledMap = new Map(Maps[CurrentMap], true, MapsPath, this.gameObject, defaultMaterial, sortingOrder);
		TiledMap.GenerateCollidersFromLayer("Collider_0");
		TiledMap.GenerateCollidersFromLayer("Colliders");
		Debug.Log(TiledMap.ToString());
		MapObjectLayer mol = TiledMap.GetLayer("PropertyTest") as MapObjectLayer;
		if (mol != null)
		{
			Debug.Log(mol.GetPropertyAsBoolean("test"));
		}
		// Center camera
		_camera.CamPos = new Vector3(TiledMap.Width / 2.0f, -TiledMap.Height / 2.0f, _camera.CamPos.z);
		_camera.PixelsPerUnit = TiledMap.TileHeight;
	}
}
