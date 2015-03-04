using UnityEngine;
using System.Collections;

using SQLite4Unity3d;

public class NodeDomainData
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


	#endregion

	#region Cached Data


	private Sprite _sprite;
	public Sprite sprite {
		get {
			if(_sprite == null) LoadSprite(spritePath);

			return _sprite;
		}
	}

	
	#endregion

	#region Init


	public void LoadSprite(string resourcePath) {
		Sprite resourceSprite = Resources.Load<Sprite>(resourcePath);
		Resources.UnloadUnusedAssets();
		if (resourceSprite != null) {
			_sprite = resourceSprite;
			Debug.Log ("Loaded sprite: " + _sprite);
		} else {
			Debug.LogError("Node sprite doesn't exist at: Resources/" + resourcePath);
		}
	}


	#endregion
}