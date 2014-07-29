using UnityEngine;
using System.Collections;

public class Trigger2DNextScene : MonoBehaviour {

	public string nextScene = "";
	public string[] tagsCanCollide = {"Player"};

	void OnTriggerEnter2D(Collider2D other) {
		foreach(string currentTag in tagsCanCollide) {
			if(currentTag.Equals(other.tag)) {
				Application.LoadLevel(nextScene);
				return;
			}
		}
	}
}