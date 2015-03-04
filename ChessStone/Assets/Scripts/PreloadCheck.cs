using UnityEngine;
using System.Collections;

public class PreloadCheck : MonoBehaviour
{
	void Awake() {
		if(GameObject.FindGameObjectWithTag("Builder") == null) {
			Application.LoadLevel("Preloader");
		}

		Destroy(this.gameObject);
	}
}