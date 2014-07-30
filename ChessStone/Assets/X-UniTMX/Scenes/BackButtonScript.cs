using UnityEngine;
using System.Collections;

public class BackButtonScript : MonoBehaviour {

	void OnGUI()
	{
		if (GUI.Button(new Rect(0.9f * Screen.width, 0.9f * Screen.height, 0.1f * Screen.width, 0.1f * Screen.height), "Back"))
			Application.LoadLevel("StartingScene");
	}
}
