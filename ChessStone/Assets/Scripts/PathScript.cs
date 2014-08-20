using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Each GameCharacter must have a pathing script associated with it that
/// handles tile movement and interaction.
/// </summary>
public class PathScript : MonoBehaviour
{
<<<<<<< HEAD
=======
	#region Graphics Data


	[SerializeField]
	private GameObject rangePrefab;

	[SerializeField]
	private GameObject adjPrefab;


	#endregion
>>>>>>> origin/master
	
	#region Unit Data
	
	
	private GameCharacter character;
	
	
	#endregion

	#region Tile Data


	/// <summary>
	/// The tile that is being stood upon.
	/// </summary>
	public Tile currTile { get; private set; }
	public int currTileX { get; private set; }
	public int currTileY { get; private set; }

	/// <summary>
	/// A list of the adjacent tiles to the current tile.
	/// </summary>
	/// <value>The adj tiles.</value>
	public List<Tile> adjTiles { get; private set; }

	/// <summary>
	/// A list of the tiles in range of the current tile.
	/// </summary>
	public List<Tile> rangeTiles { get; private set; }

<<<<<<< HEAD
=======
	private List<GameObject> adjTileObjs;
	private List<GameObject> rangeTileObjs;

>>>>>>> origin/master

	#endregion

	#region Range Data

	
	public int tileRange { get; private set; }

	[SerializeField]
	private int moveRange;

<<<<<<< HEAD
=======
	/// <summary>
	/// Gets the moves left for this path.
	/// </summary>
	public int movesLeft { get; private set; }

>>>>>>> origin/master

	#endregion

	#region Initialization


	void Start() {
		if(!character) {
			character = GetComponent<GameCharacter>();
			if(!character) {
				Debug.LogError("Unit script has not been assigned to " + this.gameObject.name + "!");
				return;
			}
		}

		rangeTiles = new List<Tile>();
		adjTiles = new List<Tile>();

		ResetMoves();
	}


	#endregion

	#region Public Interaction
	

	/// <summary>
	/// Immediately moves to a new tile defined by its x & y indices 
	/// on the main tile grid.
	/// </summary>
	public void SetTile(int tileX, int tileY) {
		currTile = GameMap.Instance.mainGrid[tileX, tileY];
		currTileX = tileX; currTileY = tileY;
		
		currTile.currUnit = character;
		
		// set the position to the tile's position
		Transform tileTransform = currTile.TileObject.transform;
		transform.position = tileTransform.position + new Vector3(currTile.TileSet.WorldDims.x * 0.5f, currTile.TileSet.WorldDims.y * 0.5f, 0);
			
		ComputeTiles(tileRange);
	}

	/// <summary>
	/// Immediately moves to a new tile.
	/// </summary>
	public void SetTile(Tile moveTile) {
		currTile = moveTile;
		currTile.currUnit = character;

		Map map = GameMap.Instance.map; 
		// set the position to the tile's position
		Transform tileTransform = currTile.TileObject.transform;
		Vector3 tilePos = tileTransform.position + new Vector3(currTile.TileSet.WorldDims.x * 0.5f, currTile.TileSet.WorldDims.y * 0.5f, 0);
		transform.position = tilePos;

		Vector2 tileIndices = map.WorldPointToTileIndex(tilePos);
		currTileX = (int)tileIndices.x; currTileY = (int)tileIndices.y;
		
		ComputeTiles(tileRange);
	}

	/// <summary>
	/// Immediately moves in a horizontal direction with a specified range.
	/// </summary>
	public void SetTile(int range, Vector2 moveDir) {
		//Tile moveTile = GameMap.In
	}

	public void IssueTileMoveOrder(Vector2 targetIndices) {
		if(tileRange <= 0) return;

		Tile moveTile = GameMap.Instance.mainGrid[(int)targetIndices.x, (int)targetIndices.y];
		if(!adjTiles.Contains(moveTile)) return;

		tileRange--;

		SetTile ((int)targetIndices.x, (int)targetIndices.y);
	}

	/// <summary>
	/// Issues a move order to that moves to the specified tile.
	/// </summary>
<<<<<<< HEAD
	public void IssueTileMoveOrder(Tile actionTile) {
		if(tileRange <= 0) return;
=======
	public void IssueTileMoveOrder(Tile moveTile) {
		if(movesLeft <= 0) return;
>>>>>>> origin/master

		if(!adjTiles.Contains(actionTile)) return;

		tileRange--;

		SetTile (actionTile);
	}

	private IEnumerator MoveToTile(Vector2 targetIndices) {
		Tile moveTile = GameMap.Instance.mainGrid[(int)targetIndices.x, (int)targetIndices.y];
		Vector3 startPos = transform.position;
		Vector3 endPos = moveTile.TileObject.transform.position + (Vector3)moveTile.TileSet.WorldDims * 0.5f;
		float currTime = 0f;
		while(currTime < 1) {
			currTime = Mathf.Min(currTime + Time.deltaTime, 1);
			transform.position = Vector2.Lerp(startPos, endPos, currTime);
			yield return null;
		}

		Debug.Log ("finished");
	}

	/// <summary>
	/// Resets the moves for this pathing.
	/// </summary>
	public void ResetMoves() {
		tileRange = moveRange;
	}

<<<<<<< HEAD

	#endregion

	#region Public Interaction


	public void ForceRecomputeTiles(int range) {
		ComputeTiles(range);
=======
	/// <summary>
	/// Shows the tiles in range and adjacent tiles.
	/// </summary>
	public void ShowTiles() {
		foreach(Tile t in rangeTiles) {
			Transform tileTransform = t.TileObject.transform;
			Vector3 newPos = tileTransform.position + new Vector3(t.TileSet.WorldDims.x * 0.5f, t.TileSet.WorldDims.y * 0.5f, 0);
			
			if(adjTiles.Contains(t))
				adjTileObjs.Add(Instantiate(adjPrefab, newPos, Quaternion.identity) as GameObject);
			else
				rangeTileObjs.Add(Instantiate(rangePrefab, newPos, Quaternion.identity) as GameObject);
		}
>>>>>>> origin/master
	}


	#endregion

	#region Public Interaction


	/// <summary>
	/// Clears the shown in-range and adjacent tiles.
	/// </summary>
	public void ClearShownTiles() {
		foreach(GameObject g in rangeTileObjs) Destroy (g);
		foreach(GameObject g in adjTileObjs) Destroy (g);
		
		rangeTileObjs.Clear();
		adjTileObjs.Clear();
	}


	#endregion

	#region Helpers


	private void ComputeTiles(int range) {
		rangeTiles.Clear();
		adjTiles.Clear();

		int i, j, 
			modTileX, modTileY,
			gridTilesLenX = GameMap.Instance.mainGrid.Width, gridTilesLenY = GameMap.Instance.mainGrid.Height,
			jCount = -range;


		for(i = -range; i <= range; i++) {
			int jCountTemp = range - Mathf.Abs(jCount);

			for(j = jCountTemp; j >= -jCountTemp; j--) {
				modTileX = currTileX + i; modTileY = currTileY + j;
				
				if(!(i == 0 && j == 0) && modTileX >= 0 && modTileX < gridTilesLenX && modTileY >= 0 && modTileY < gridTilesLenY) { 
					Tile temp = GameMap.Instance.mainGrid[modTileX, modTileY];
					rangeTiles.Add(temp);

					if((Mathf.Abs(i) == 0 && Mathf.Abs (j) == 1)|| (Mathf.Abs(j) == 0 && Mathf.Abs(i) == 1)) {
						adjTiles.Add(temp);
					}
				}
			}

			jCount++;
		}
	}


	#endregion
}