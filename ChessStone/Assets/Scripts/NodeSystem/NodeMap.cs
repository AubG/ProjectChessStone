using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;
using Pathfinding;

/// <summary>
/// Singleton that describes a game map that has all of the main tiles and units on the map.
/// </summary>
public class NodeMap : Singleton<NodeMap>
{
	#region Serialized Data

	
	public string MapTMXPath = "GameMaps/";
	public Material materialDefaultFile;
	public int DefaultSortingOrder = 0;
	public bool MakeUniqueTiles = true;
	
	[SerializeField]
	private string mainTileLayerName = "Tiles";

	[SerializeField]
	private string mainObjectLayerName = "Objects";

	private MapRegion currRegion;


	#endregion

	#region Data


	/// <summary>
	/// The main map instance of the game.
	/// </summary>
	public Map map { get; private set; }

	public Bounds mainBounds { get; private set; }

	/// <summary>
	/// Nodes on the map.
	/// </summary>
	private List<MapNode> _nodes = new List<MapNode>();
	public List<MapNode> nodes {
		get { return _nodes; }
		private set { _nodes = value; }
	}


	#endregion

	#region Initialization


	public void LoadRegion(MapRegion region) {
		StartCoroutine(LoadRegionProgress(region));
	}

	public void UnloadRegion() {
		StartCoroutine(UnloadRegionProgress());
	}

	/// <summary>
	/// Initialize the game map.
	/// </summary>
	private IEnumerator LoadRegionProgress(MapRegion region) {
		yield return StartCoroutine(WaitUntilLoaded());
		if(region != currRegion) {
			yield return StartCoroutine (UnloadRegionProgress());
			currRegion = region;
		}
		
		map = new Map(currRegion.nodeMapTMX, MakeUniqueTiles, MapTMXPath, gameObject, materialDefaultFile, DefaultSortingOrder);
		Resources.UnloadUnusedAssets();

		ConfigureTiles();
		ConfigureCamera();
		ConfigureObjects();
	}

	public IEnumerator UnloadRegionProgress() {
		map = null;

		foreach(MapNode node in _nodes) {
			Destroy(node.gameObject);
			yield return null;
		}
	}

	private IEnumerator WaitUntilLoaded() {
		while(!PlayerData.Instance || !PlayerData.Instance.loaded) {
			yield return null;
		}
	}

	private void ConfigureTiles() {
		TileGrid grid = map.GetTileLayer("Tiles").Tiles;

		Vector2 dims = map.TileSets[0].WorldDims;

		float sizeX = dims.x * grid.Width;
		float sizeY = dims.y * grid.Height;
		
		mainBounds = new Bounds(map.MapObject.transform.position + new Vector3(sizeX * 0.5f, -sizeY * 0.5f, 0f),
		                        new Vector3(sizeX, sizeY, 1f));
	}

	private void ConfigureCamera() {
		// center the main camera
		Camera.main.transform.position = mainBounds.center;
	}

	/// <summary>
	/// Initializes the objects on the map.
	/// </summary>
	public void ConfigureObjects() {
		MapObjectLayer mainObjectLayer = map.GetObjectLayer(mainObjectLayerName);
		List<MapObject> validObjects = mainObjectLayer.Objects.ToList();
		
		// iterate through all the map objects in the game
		// see if they are units or characters
		// then add them to the mainUnits or mainCharacters Lists
		// and initialize them properly into the scene
		foreach (MapObject o in validObjects) {
			Vector2 centerPos = o.Bounds.center;
			centerPos.y *= -1;

			int domainId = o.GetPropertyAsInt("node domain id");
			string name = o.GetPropertyAsString("node name");

			MapNode n = NodeBuilder.Instance.BuildNode(domainId, name, centerPos);

			// TODO: Fix this
			int stageId = o.GetPropertyAsInt("stage id");
			if(stageId != -1) {
				n.stageId = stageId;
			}
			
			int merchantId = o.GetPropertyAsInt("merchant id");
			if(merchantId != -1) {
				n.merchantId = merchantId;
			}

			nodes.Add(n);
		}
	}

	/// <summary>
	/// Security to ensure that the Game Map Singleton is never instantiated publicly.
	/// </summary>
	protected NodeMap () {}


	#endregion

	#region Interaction


	#endregion
}