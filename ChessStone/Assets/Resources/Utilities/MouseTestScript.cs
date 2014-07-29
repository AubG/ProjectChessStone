using UnityEngine;
using System.Collections;

public class MouseTestScript : MonoBehaviour {

	public TiledMapComponent TMComponent;
	//public float PosX = 0;
	//public float PosY = 0;

	public GameObject SelectedTile = null;
	Vector3 mouseWorldPos;

	Vector2 offset = Vector2.zero;

	void Start()
	{
		if (TMComponent.TiledMap.Orientation == X_UniTMX.Orientation.Isometric)
			offset.x = -0.5f;
	}

	// Update is called once per frame
	void Update () {
		if (TMComponent != null)
		{
			//if(Input.GetMouseButtonDown(0)) 
			//{
			//	Debug.Log(TMComponent.TiledMap.WorldPointToTileIndex(new Vector2(PosX, PosY)));//Camera.main.ScreenToWorldPoint(Input.mousePosition))
			//	Debug.Log(TMComponent.TiledMap.TiledPositionToWorldPoint(PosX, PosY));
			//}
			if (SelectedTile != null)
			{
				mouseWorldPos = Input.mousePosition;
				mouseWorldPos.z = 10;
				mouseWorldPos = Camera.main.ScreenToWorldPoint(mouseWorldPos);
				//Debug.Log(mouseWorldPos);
				SelectedTile.transform.position = TMComponent.TiledMap.TiledPositionToWorldPoint(TMComponent.TiledMap.WorldPointToTileIndex((Vector2)mouseWorldPos)) + offset;
				//Debug.Log(TMComponent.TiledMap.WorldPointToTileIndex(new Vector2(PosX, -PosY)));
				//SelectedTile.transform.position = TMComponent.TiledMap.TiledPositionToWorldPoint(TMComponent.TiledMap.WorldPointToTileIndex(new Vector2(PosX, -PosY)));
			}
		}
	}
}

