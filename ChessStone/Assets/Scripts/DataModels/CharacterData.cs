using UnityEngine;
using System.Collections;

using SQLite4Unity3d;

public class CharacterData
{
	#region Data


	[PrimaryKey]
	public int id { get; set; }

	//[NotNull]
	public string name { get; set; }

	//[NotNull]
	public string description { get; set; }

	//[NotNull]
	public string spritePath { get; set; }

	//[NotNull]
	public string linkColorHex { get; set; }

	//[NotNull]
	public int moveRange { get; set; }

	//[NotNull]
	public int maxSize { get; set; }

	public int spellIdA { get; set; }
	public int spellIdB { get; set; }
	public int spellIdC { get; set; }
	public int spellIdD { get; set; }

	public int cost { get; set; }

	private bool linkColorLoaded = false;


	#endregion

	#region Cached Data


	private Sprite _sprite;
	public Sprite sprite {
		get {
			if(_sprite == null) LoadSprite(spritePath);

			return _sprite;
		}
	}

	private Color _linkColor;
	public Color linkColor {
		get {
			if(!linkColorLoaded) {
				_linkColor = ColorUtil.HexToColor(linkColorHex);
				linkColorLoaded = true;
			}

			return _linkColor;
		}
	}

	
	#endregion

	#region Init


	public void LoadSprite(string resourcePath) {
		Sprite resourceSprite = Resources.Load<Sprite>(resourcePath);
		Resources.UnloadUnusedAssets();
		if (resourceSprite != null) {
			_sprite = resourceSprite;
			//Debug.Log ("Loaded sprite: " + _sprite);
		} else {
			Debug.LogError("Character sprite doesn't exist at: Resources/" + resourcePath);
		}
	}


	#endregion
}