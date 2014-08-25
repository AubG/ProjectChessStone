using UnityEngine;
using System.Collections;

public class Trigger2DRespawnSave : MonoBehaviour
{
	public string[] tagsCanCollide = {"Player"};
	private string respawnToSave = "Start";
	
	void Start() {
		if(PlayerPrefs.GetString("PlayerRespawn","").Equals("")) {
			PlayerPrefs.SetString("PlayerRespawn",respawnToSave);
		}
	}
	
	void OnTriggerEnter2D(Collider2D other) {
		foreach(string currentTag in tagsCanCollide) {
			if(currentTag.Equals(other.tag)) {
				// Save the next respawn point
				PlayerPrefs.SetString("PlayerRespawn",respawnToSave);
				return;
			}
		}
	}

	public void SaveRespawn(string respawn) {
		respawnToSave = respawn;
	}
}

