using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

/// <summary>
/// This script adds the NGUI menu options to the Unity Editor.
/// </summary>

static public class TilingMenu
{
	/// <summary>
	/// Same as SelectedRoot(), but with a log message if nothing was found.
	/// </summary>
	
	static public GameObject SelectedRoot ()
	{
		GameObject go = NGUIEditorTools.SelectedRoot();
		
		if (go == null)
		{
			Debug.Log("No UI found. You can create a new one easily by using the UI creation wizard.\nOpening it for your convenience.");
			CreateUIWizard();
		}
		return go;
	}
	
	[MenuItem("NGUI/Open the UI Wizard")]
	static public void CreateUIWizard ()
	{
		EditorWindow.GetWindow<UICreateNewUIWizard>(false, "UI Tool", true);
	}
	
	[MenuItem("NGUI/Open the Panel Tool")]
	static public void OpenPanelWizard ()
	{
		EditorWindow.GetWindow<UIPanelTool>(false, "Panel Tool", true);
	}
	
	[MenuItem("NGUI/Open the Camera Tool")]
	static public void OpenCameraWizard ()
	{
		EditorWindow.GetWindow<UICameraTool>(false, "Camera Tool", true);
	}
	
	[MenuItem("NGUI/Open the Font Maker #&f")]
	static public void OpenFontMaker ()
	{
		EditorWindow.GetWindow<UIFontMaker>(false, "Font Maker", true);
	}
	
	[MenuItem("NGUI/Open the Atlas Maker #&m")]
	static public void OpenAtlasMaker ()
	{
		EditorWindow.GetWindow<UIAtlasMaker>(false, "Atlas Maker", true);
	}
	
	[MenuItem("NGUI/Toggle Draggable Handles")]
	static public void ToggleNewGUI ()
	{
		UIWidget.showHandlesWithMoveTool = !UIWidget.showHandlesWithMoveTool;
		
		if (UIWidget.showHandlesWithMoveTool)
		{
			Debug.Log("Simple Mode: Draggable Handles will show up with the Move Tool selected (W).");
		}
		else
		{
			Debug.Log("Classic Mode: Draggable Handles will show up only with the View Tool selected (Q).");
		}
	}
}
