using UnityEngine;
using System.Collections;

public class Preload : MonoBehaviour
{
	void Awake() {
		Application.LoadLevel("Map");
	}
}