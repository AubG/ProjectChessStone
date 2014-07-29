using UnityEngine;
using System.Collections;

public class RespawnPoint : MonoBehaviour {

	public string spawnPointName = "";

	public void SetRespawn(string respawn) {
		spawnPointName = respawn;
	}
}
