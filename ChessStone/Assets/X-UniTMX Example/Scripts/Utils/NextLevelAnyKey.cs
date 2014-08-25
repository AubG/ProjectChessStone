using UnityEngine;
using System.Collections;

public class NextLevelAnyKey : MonoBehaviour {

	public string nextLevelToLoad = "Menu";

	void Update () {
		if(Input.anyKeyDown && !Input.GetMouseButtonDown(0)) {
			Application.LoadLevel(nextLevelToLoad);
		}
	}
}
