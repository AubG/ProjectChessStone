using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Inspector class used to edit Inventory Databases.
/// </summary>

public class DatabaseEditor : Editor
{
	protected static int mIndex = 0;

	protected bool mConfirmDelete = false;

	/// <summary>
	/// Draw an enlarged sprite within the specified texture atlas.
	/// </summary>

	public Rect DrawSprite (Texture2D tex, Rect sprite, Material mat) { return DrawSprite(tex, sprite, mat, true, 0); }

	/// <summary>
	/// Draw an enlarged sprite within the specified texture atlas.
	/// </summary>

	public Rect DrawSprite (Texture2D tex, Rect sprite, Material mat, bool addPadding)
	{
		return DrawSprite(tex, sprite, mat, addPadding, 0);
	}

	/// <summary>
	/// Draw an enlarged sprite within the specified texture atlas.
	/// </summary>

	public Rect DrawSprite (Texture2D tex, Rect sprite, Material mat, bool addPadding, int maxSize)
	{
		float paddingX = addPadding ? 4f / tex.width : 0f;
		float paddingY = addPadding ? 4f / tex.height : 0f;
		float ratio = (sprite.height + paddingY) / (sprite.width + paddingX);

		ratio *= (float)tex.height / tex.width;

		// Draw the checkered background
		Color c = GUI.color;
		Rect rect = NGUIEditorTools.DrawBackground(tex, ratio);
		GUI.color = c;

		if (maxSize > 0)
		{
			float dim = maxSize / Mathf.Max(rect.width, rect.height);
			rect.width *= dim;
			rect.height *= dim;
		}

		// We only want to draw into this rectangle
		if (Event.current.type == EventType.Repaint)
		{
			if (mat == null)
			{
				GUI.DrawTextureWithTexCoords(rect, tex, sprite);
			}
			else
			{
				// NOTE: DrawPreviewTexture doesn't seem to support BeginGroup-based clipping
				// when a custom material is specified. It seems to be a bug in Unity.
				// Passing 'null' for the material or omitting the parameter clips as expected.
				UnityEditor.EditorGUI.DrawPreviewTexture(sprite, tex, mat);
				//UnityEditor.EditorGUI.DrawPreviewTexture(drawRect, tex);
				//GUI.DrawTexture(drawRect, tex);
			}
			rect = new Rect(sprite.x + rect.x, sprite.y + rect.y, sprite.width, sprite.height);
		}
		return rect;
	}

	/// <summary>
	/// Draw the inspector widget.
	/// </summary>

	public override void OnInspectorGUI ()
	{}
}
