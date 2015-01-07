using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;
using Pathfinding;

/// <summary>
/// Singleton that describes a game map that has all of the main tiles and units on the map.
/// </summary>
public class GameMap : Singleton<GameMap>
{
	#region Serialized Data


	public string MapTMXPath = "GameMaps/";
	public Material materialDefaultFile;
	public int DefaultSortingOrder = 0;
	public bool MakeUniqueTiles = true;

	public TextAsset MapTMX;


	#endregion

	#region Graphics Data

	[SerializeField]
	private GameObject tileHighlightPrefab;

	[SerializeField]
	private GameObject tileHighlightFillCenterPrefab;


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

	public GridGraph mainGraph { get; private set; }

	public Bounds mainBounds { get; private set; }

	/// <summary>
	/// Gets all the units on the map.
	/// </summary>
	/// <value>The main units.</value>
	private List<GameUnit> _mainUnits = new List<GameUnit>();
	public List<GameUnit> mainUnits {
		get { return _mainUnits; }
		private set { _mainUnits = value; }
	}

	/// <summary>
	/// Gets all the characters on the map.
	/// </summary>
	private List<GameUnit> _mainCharacters = new List<GameUnit>();
	public List<GameUnit> mainCharacters {
		get { return _mainCharacters; }
		private set { _mainCharacters = value; }
	}

	private List<Tile> _spawnTiles = new List<Tile>();
	public List<Tile> spawnTiles {
		get { return _spawnTiles; }
		private set { _spawnTiles = value; }
	}


	#endregion

	#region Initialization


	public void Awake() {
		StartCoroutine(Init());
	}

	/// <summary>
	/// Initialize the game map.
	/// </summary>
	public IEnumerator Init() {
		yield return StartCoroutine(WaitUntilLoaded());

		StageData stage = StageManager.Instance.LoadStage(PlayerData.Instance.currStage);
		LoadMap(stage.mapPath);

		map = new Map(MapTMX, MakeUniqueTiles, MapTMXPath, gameObject, materialDefaultFile, DefaultSortingOrder);
		Resources.UnloadUnusedAssets();
	
		InitializeTiles();
		InitializeCamera();
		InitializeObjects();
		InitializePathing();
	}

	private void LoadMap(string resourcePath) {
		TextAsset resourceXML = Resources.Load<TextAsset>(resourcePath);
		Resources.UnloadUnusedAssets();
		if (resourceXML != null) {
			MapTMX = resourceXML;
		} else {
			Debug.LogError("XML doesn't exist at: Resources/" + resourcePath);
		}
	}

	private IEnumerator WaitUntilLoaded() {
		while(!PlayerData.Instance.loaded) {
			yield return null;
		}
	}

	private void InitializeTiles() {
		mainGrid = map.GetTileLayer("Tiles").Tiles;

		Tile t = null;
		float maxHeight = 0f, sizeX = 0f, sizeY = 0f;
		
		// Initialize the center positions of the tiles
		for(int x = 0, y = 0; x < mainGrid.Width; x++) {
			maxHeight = 0f;
			for(y = 0; y < mainGrid.Height; y++) {
				t = mainGrid[x, y];

				t.centerPos = t.TileObject.transform.position + new Vector3(t.TileSet.WorldDims.x * 0.5f, t.TileSet.WorldDims.y * 0.5f, 0);
				CreateTileHighlight(t);

				sizeX += t.TileSet.WorldDims.x;
				maxHeight = Mathf.Max(maxHeight, t.TileSet.WorldDims.y);
			}
			
			sizeY += maxHeight;
		}

		mainBounds = new Bounds(map.MapObject.transform.position, new Vector3(sizeX, sizeY, 1f));
	}
	
	private void CreateTileHighlight(Tile t)
	{
		GameObject tileObject = t.TileObject;

		GameObject highlightObject = new GameObject(tileObject.name + "-highlight");
		highlightObject.transform.parent = tileObject.transform;
		
		SpriteRenderer highlightRenderer = highlightObject.AddComponent<SpriteRenderer>();
		
		highlightRenderer.sprite = null;
		// Use Layer's name as Sorting Layer
		highlightRenderer.sortingLayerName = tileObject.renderer.sortingLayerName;
		
		highlightObject.transform.position = t.centerPos;

		t.TileHighlightObject = highlightObject;
	}

	private void InitializeCamera() {
		// center the main camera
		Tile centerTile = mainGrid[(int)(map.Width * 0.5f), (int)(map.Height * 0.5f) + 1];
		Camera.main.transform.position = new Vector3(centerTile.centerPos.x, centerTile.centerPos.y, Camera.main.transform.position.z);
	}

	/// <summary>
	/// Initializes the objects on the map.
	/// </summary>
	private void InitializeObjects() {
		MapObjectLayer mainObjectLayer = map.GetObjectLayer("Objects");
		List<MapObject> validObjects = mainObjectLayer.Objects.ToList();

		// iterate through all the map objects in the game
		// see if they are units or characters
		// then add them to the mainUnits or mainCharacters Lists
		// and initialize them properly into the scene
		foreach (MapObject o in validObjects) {
			Vector2 tileIndices = map.WorldPointToTileIndex(o.Bounds.position);
			tileIndices.y *= -1;

			if(o.Type == "Character") {
		        int characterId = o.GetPropertyAsInt("character id");
				int characterOwner = o.GetPropertyAsInt("character owner");

				GameCharacter c = CharacterManager.Instance.BuildCharacter(characterId, PlayerManager.Instance.GetPlayer(characterOwner), 0, 0);

				if(c == null) {
					Debug.LogError("Character (ID: " + characterId + ") could not be built.");
					return;
				}

				c.SetTile((int)tileIndices.x, (int)tileIndices.y);
			} else if(o.Type == "SpawnPoint") {
				// make a spawn point unit at the location
				spawnTiles.Add(mainGrid[(int)tileIndices.x, (int)tileIndices.y]);
				Object resourceObject = Resources.Load ("Prefabs/SpawnPoint");
				GameObject newPrefab = Instantiate(resourceObject) as GameObject;
				GameUnit temp = newPrefab.GetComponent<GameUnit>();
				temp.name = (spawnTiles.Count - 1) + "-spawn";
				temp.SetTile((int)tileIndices.x, (int)tileIndices.y);
			}
		}
	}

	/// <summary>
	/// Security to ensure that the Game Map Singleton is never instantiated publicly.
	/// </summary>
	protected GameMap () {}


	#endregion

	#region Map


	private void InitializePathing() {
		AstarPath.active.UpdateGraphs(mainBounds);
		AstarPath.active.Scan();

		mainGraph = AstarPath.active.astarData.gridGraph;

		for(int x = 0; x < mainGrid.Width; x++) {
			for(int y = 0; y < mainGrid.Height; y++) {
				if(!mainGrid[x, y].walkable) {
					GridNode node = mainGraph.nodes[y * mainGraph.Width + x];
					node.Walkable = false;
				}
			}
		}
	}

	/// <summary>
	/// Updates the pathfinding graph used by the AI.
	/// </summary>
	public void UpdatePathing(GameUnit self, GameUnit target) {
		for(int i = 0; i < mainUnits.Count; i++) {
			int index = mainUnits[i].GetTileY() * mainGraph.Width + mainUnits[i].GetTileX();

			GridNode node = mainGraph.nodes[index];

			if(mainUnits[i] == self || mainUnits[i] == target) {
				node.Walkable = true;
			} else {
				node.Walkable = false;
			}
		}
	}

	public List<Tile> ComputeTilesInRange(Tile origin, int range, ComputeTileFilterDelegate filter = null) {
		TileGrid grid = GameMap.Instance.mainGrid;
		
		Queue<Tile> frontier = new Queue<Tile>();
		frontier.Enqueue(origin);
		
		List<Tile> visited = new List<Tile>();
		List<Tile> result = new List<Tile>();
		
		Tile node, neighbor;
		int x, y, i, j, il, jl;
		int depth = 0, depthIncreaseThreshold = 0;
		int[,] indices = new int[4, 2];
		
		while(frontier.Count > 0) {
			node = frontier.Dequeue();
			
			indices[0, 0] = node.x - 1; indices[0, 1] = node.y;
			indices[1, 0] = node.x; 	indices[1, 1] = node.y - 1;
			indices[2, 0] = node.x + 1; indices[2, 1] = node.y;
			indices[3, 0] = node.x; 	indices[3, 1] = node.y + 1;
			
			for(i = 0, il = indices.GetLength(0); i < il; i++) {
				x = indices[i, 0]; y = indices[i, 1];
				if(x >= 0 && y >= 0 && x < grid.Width && y < grid.Height && !visited.Contains(grid[x, y]) && !(grid[x, y] == origin)) {
					neighbor = grid[x, y];
					visited.Add(neighbor);
					
					if(filter == null || filter(neighbor)) {
						frontier.Enqueue(neighbor);
						result.Add(neighbor);
					}
				}
			}
			
			depthIncreaseThreshold--;
			if(depthIncreaseThreshold <= 0) {
				depthIncreaseThreshold = frontier.Count;
				if(++depth >= range) break;
			}
		}
		
		return result;
	}

	public bool IsTileWalkable(Tile filterTile) {
		return filterTile.walkable;
	}

	public bool IsTileValid(Tile filterTile) {
		return filterTile.walkable && filterTile.currUnit == null;
	}


	#endregion

	#region Units


	public void RegisterCharacter(GameCharacter character) {
		RegisterUnit(character);
		mainCharacters.Add(character);
	}

	public void RegisterUnit(GameUnit unit) {
		mainUnits.Add(unit);
	}

	public void RemoveUnit(GameUnit unit) {
		int index = unit.GetTileY() * mainGraph.Width + unit.GetTileX();
		mainGraph.nodes[index].Walkable = true;

		mainUnits.Remove(unit);

		switch(unit.unitType) {
		case UnitType.Character:
			GameCharacter character = (GameCharacter)unit;
			character.owningPlayer.RemoveCharacter(character);
			mainCharacters.Remove(unit);
			break;
		}

		unit.currTile.currUnit = null;
		Destroy(unit.gameObject);
	}


	#endregion
}