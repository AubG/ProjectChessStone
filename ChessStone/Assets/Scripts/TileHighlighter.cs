using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using X_UniTMX;

public enum HighlightStyle {
	None,
	Border,
	BorderFillCenter
}

public delegate bool ComputeTileFilterDelegate(Tile filterTile);

public class TileHighlighter : MonoBehaviour {

	#region Style Data


	[SerializeField]
	private Sprite[] styleSprites = new Sprite[2];


	#endregion

	// how to fill up the array of sprites to be used with the styles?

	private List<Tile> highlightedTiles = new List<Tile>();

	#region Interaction
	

	/// <summary>
	/// Shows tiles in range and adjacent tiles.
	/// </summary>
	public void HighlightTilesInRange(Tile origin, int range, Color color, bool adj = false) {
		if(range > 0) {
			List<Tile> rangeTiles = GameMap.Instance.ComputeTilesInRange(origin, range);

			int i = 0;

			if(adj) {
				int adjLimit = (rangeTiles.Count >= 4) ? 4 : rangeTiles.Count;
				for(; i < adjLimit; i++) {
					HighlightTile(rangeTiles[i], HighlightStyle.BorderFillCenter, color);
				}
			}

			for(; i < rangeTiles.Count; i++) {
				HighlightTile(rangeTiles[i], HighlightStyle.Border, color);
			}
		}
	}

	/// <summary>
	/// Shows tiles in range and adjacent tiles.
	/// </summary>
	public void HighlightTilesInPathingRange(Tile origin, int range, Color color, bool adj = false) {
		if(range > 0) {
			List<Tile> rangeTiles = GameMap.Instance.ComputeTilesInRange(origin, range, GameMap.Instance.IsTileValid);
			
			int i = 0;
			
			if(adj) {
				int adjLimit = (rangeTiles.Count >= 4) ? 4 : rangeTiles.Count;
				for(; i < adjLimit; i++) {
					HighlightTile(rangeTiles[i], HighlightStyle.BorderFillCenter, color);
				}
			}
			
			for(; i < rangeTiles.Count; i++) {
				HighlightTile(rangeTiles[i], HighlightStyle.Border, color);
			}
		}
	}
	
	/// <summary>
	/// Clears the shown in-range and adjacent tiles.
	/// </summary>
	public void ClearHighlightedTiles() {
		for(int i = 0; i < highlightedTiles.Count; i++) {
			ClearTileHighlight(highlightedTiles[i]);
			i--;
		}
	}

	public void HighlightTile(Tile t, HighlightStyle style, Color color) {
		SpriteRenderer renderer = t.TileHighlightObject.GetComponent<SpriteRenderer>();
		
		if(style == HighlightStyle.None) {
			Debug.LogError("Tile Highlight: Should not use this style, use ClearTileHighlight(Tile) if trying to clear the highlight!");
			return;
		}
		
		renderer.sprite = styleSprites[(int)style - 1];
		
		renderer.color = color;
		
		highlightedTiles.Add(t);
	}
	
	public void ClearTileHighlight(Tile t) {
		SpriteRenderer renderer = t.TileHighlightObject.GetComponent<SpriteRenderer>();
		
		renderer.sprite = null;
		
		highlightedTiles.Remove(t);
	}
	
	
	#endregion
}