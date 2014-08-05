using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;

public class PathScript : MonoBehaviour
{
	public GameObject rangePrefab;
	public GameObject adjPrefab;

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

	public List<GameObject> adjTileObjs { get; private set; }
	public List<GameObject> rangeTileObjs { get; private set; }


	#endregion

	#region Move Data


	[SerializeField]
	private int moveRange;

	public int movesLeft { get; private set; }


	#endregion

	#region Initialization


	void Start() {
		rangeTiles = new List<Tile>();
		rangeTileObjs = new List<GameObject>();
		adjTiles = new List<Tile>();
		adjTileObjs = new List<GameObject>();

		ResetMoves();
		SetTile(21, 21);
	}


	#endregion

	#region Public Interaction
	

	/// <summary>
	/// Immediately moves to a new tile defined by its x & y indices.
	/// </summary>
	public void SetTile(int tileX, int tileY) {
		currTile = GameMap.Instance.mainGrid[tileX, tileY];
		currTileX = tileX; currTileY = tileY;
		Map map = GameMap.Instance.map; 
		Transform tileTransform = currTile.TileObject.transform;
		transform.position = tileTransform.position + new Vector3(currTile.TileSet.WorldDims.x * 0.5f, currTile.TileSet.WorldDims.y * 0.5f, 0);
			
		ComputeTiles();
	}

	/// <summary>
	/// Immediately moves in a horizontal direction with a specified range.
	/// </summary>
	public void SetTile(int range, Vector2 moveDir) {
		//Tile moveTile = GameMap.In
	}

	public void IssueTileMoveOrder(Vector2 targetIndices) {
		if(movesLeft <= 0) return;

		movesLeft--;

		Tile moveTile = GameMap.Instance.mainGrid[(int)targetIndices.x, (int)targetIndices.y];
		if(!adjTiles.Contains(moveTile)) return;

		//StartCoroutine("MoveToTile", targetIndices);
		SetTile ((int)targetIndices.x, (int)targetIndices.y);
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

	public void ResetMoves() {
		movesLeft = moveRange;
	}


	#endregion

	#region Helpers


	private void ComputeTiles() {
		rangeTiles.Clear();
		adjTiles.Clear();
		foreach(GameObject g in rangeTileObjs) Destroy (g);
		foreach(GameObject g in adjTileObjs) Destroy (g);
		rangeTileObjs.Clear();
		adjTileObjs.Clear();

		int i, j, 
			modTileX, modTileY,
			gridTilesLenX = GameMap.Instance.mainGrid.Width, gridTilesLenY = GameMap.Instance.mainGrid.Height,
			jCount = -movesLeft;


		for(i = -movesLeft; i <= movesLeft; i++) {
			int jCountTemp = movesLeft - Mathf.Abs(jCount);

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

		foreach(Tile t in rangeTiles) {
			Transform tileTransform = t.TileObject.transform;
			Vector3 newPos = tileTransform.position + new Vector3(t.TileSet.WorldDims.x * 0.5f, t.TileSet.WorldDims.y * 0.5f, 0);

			if(adjTiles.Contains(t))
				adjTileObjs.Add(Instantiate(adjPrefab, newPos, Quaternion.identity) as GameObject);
			else
				rangeTileObjs.Add(Instantiate(rangePrefab, newPos, Quaternion.identity) as GameObject);
		}
	}


	#endregion
}