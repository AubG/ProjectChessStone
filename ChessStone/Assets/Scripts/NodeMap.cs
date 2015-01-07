using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using X_UniTMX;
using Pathfinding;

/// <summary>
/// Singleton that describes a game map that has all of the main tiles and units on the map.
/// </summary>
public class NodeMap : Singleton<NodeMap>
{
	#region Serialized Data


	[SerializeField]
	private string mainTileLayerName = "Tiles";

	[SerializeField]
	private string mainObjectLayerName = "Objects";


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


	/// <summary>
	/// Initialize the game map.
	/// </summary>
	public void PostInit(Map map) {
		this.map = map;

		InitializeObjects();
	}

	/// <summary>
	/// Initializes the objects on the map.
	/// </summary>
	public void InitializeObjects() {
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

			MapNode n = NodeManager.Instance.BuildNode(domainId, name, centerPos);

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