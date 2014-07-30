using UnityEngine;
using System.Collections;

public class TriggerCollision2DDestroy : MonoBehaviour {

	public string[] tagsCanCollide = {"Player"};
	
	void OnTriggerEnter2D(Collider2D other) {
		DestroyThisCollision(other.gameObject);
	}
	
	void OnCollisionEnter2D(Collision2D other) {
		DestroyThisCollision(other.gameObject);
	}
	
	private void DestroyThisCollision(GameObject other) {
		foreach(string currentTag in tagsCanCollide) {
			if(other.tag.Equals(currentTag)) {
				Destroy(gameObject);
				return;
			}
		}
	}
}
