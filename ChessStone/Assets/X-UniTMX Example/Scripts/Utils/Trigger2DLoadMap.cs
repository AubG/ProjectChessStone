using UnityEngine;
using System.Collections;

public class Trigger2DLoadMap : MonoBehaviour {

	private string nextMapToLoad = "Missing Level Configuration";
	public string[] tagsCanCollide = {"Player"};

	void OnTriggerEnter2D(Collider2D other) {
		foreach(string currentTag in tagsCanCollide) {
			if(currentTag.Equals(other.tag)) {
				// Find the first instance of TiledMapComponent and delete!
				Destroy((GameObject.FindObjectOfType<TiledMapComponent>()).gameObject);
				GameObject newMapPrefab = Resources.Load<GameObject>(nextMapToLoad);

				if(newMapPrefab != null) {
					GameObject map = Instantiate(newMapPrefab) as GameObject;
					map.name = newMapPrefab.name;
				} else {
					Debug.LogError("Error load map: " + nextMapToLoad);
				}
				return;
			}
		}
	}
	
	public void SetNextMap(string map) {
		nextMapToLoad = map;
	}
}
