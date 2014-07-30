using UnityEngine;
using System.Collections;

public class StartingSceneScript : MonoBehaviour {

	int selGrid = -1;
	string[] TestScenesNames = new string[] { 
		"(Ortho) Simple Test", "(Ortho) Map Loading on Scene Start", "(Ortho) Map Loading from Script", "(Ortho) Animated Sprite",
		"(Iso) Simple Test", "(Iso) Map Loading on Scene Start", "(Iso) Simple Test with dif. perspective", "(Iso) Simple Test with dif. perspective",
		"(Staggered) Map Loading on Scene Start"
	};
	string[] TestScenes = new string[] { 
		"TestScene", "TestScene02", "TestScene03", "TestScene04",
		"TestScene_Isometric02", "TestScene_Isometric01", "TestScene_Isometric03", "TestScene_Isometric04",
		"TestScene_Staggered01"
	};

	void OnGUI()
	{
		GUILayout.BeginArea(new Rect(0.05f * Screen.width, 0.05f * Screen.height, 0.85f * Screen.width, 0.85f * Screen.height), "Test Scenes", "box");
		
		GUILayout.FlexibleSpace();
		selGrid = GUILayout.SelectionGrid(selGrid, TestScenesNames, 2);

		if (selGrid > -1)
			Application.LoadLevel(TestScenes[selGrid]);

		GUILayout.FlexibleSpace();

		if(GUILayout.Button("Game Example"))
			Application.LoadLevel("Menu");

		GUILayout.FlexibleSpace();
		GUILayout.EndArea();
	}
}
