using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

/// <summary>
/// Inspector class used to edit Inventory Databases.
/// </summary>
/*
[CustomEditor(typeof(CharacterDatabase))]
public class CharacterDatabaseInspector : DatabaseEditor
{
	StatusEffect.Identifier mOldStatIden = StatusEffect.Identifier.None;
	StatusEffect.Identifier mSetStatIden = StatusEffect.Identifier.None;
	AbilityEffect.Identifier mSetIden = AbilityEffect.Identifier.None;

	bool mConfirmChangeStatIden = false;

	/// <summary>
	/// Draw the inspector widget.
	/// </summary>

	public override void OnInspectorGUI ()
	{
		EditorGUIUtility.LookLikeControls(80f);
		CharacterDatabase db = target as CharacterDatabase;
		NGUIEditorTools.DrawSeparator();

		BaseCharacter character = null;

		if (db.items == null || db.items.Count == 0)
		{
			mIndex = 0;
		}
		else
		{
			mIndex = Mathf.Clamp(mIndex, 0, db.items.Count - 1);
			character = db.items[mIndex];
		}

		if (mConfirmDelete)
		{
			// Show the confirmation dialog
			GUILayout.Label("Are you sure you want to delete '" + character.name + "'?");
			NGUIEditorTools.DrawSeparator();

			GUILayout.BeginHorizontal();
			{
				GUI.backgroundColor = Color.green;
				if (GUILayout.Button("Cancel")) mConfirmDelete = false;
				GUI.backgroundColor = Color.red;

				if (GUILayout.Button("Delete"))
				{
					NGUIEditorTools.RegisterUndo("Delete Character", db);
					db.items.RemoveAt(mIndex);
					mConfirmDelete = false;
				}
				GUI.backgroundColor = Color.white;
			}
			GUILayout.EndHorizontal();
		}
		else
		{
			// Database icon atlas
			UIAtlas atlas = EditorGUILayout.ObjectField("Icon Atlas", db.iconAtlas, typeof(UIAtlas), false) as UIAtlas;

			if (atlas != db.iconAtlas)
			{
				NGUIEditorTools.RegisterUndo("Database Atlas change", db);
				db.iconAtlas = atlas;
				foreach (BaseCharacter s in db.items) s.iconAtlas = atlas;
			}

			// Database ID
			int dbID = EditorGUILayout.IntField("Database ID", db.databaseID);

			if (dbID != db.databaseID)
			{
				NGUIEditorTools.RegisterUndo("Database ID change", db);
				db.databaseID = dbID;
			}

			// "New" button
			GUI.backgroundColor = Color.green;
			
			if (GUILayout.Button("New Character"))
			{
				NGUIEditorTools.RegisterUndo("Add Character", db);

				BaseCharacter bs = new BaseCharacter();
				bs.iconAtlas = db.iconAtlas;

				bs.id16 = (db.items.Count > 0) ? db.items[db.items.Count - 1].id16 + 1 : 0;
				db.items.Add(bs);
				mIndex = db.items.Count - 1;

				if (character != null)
				{
					bs.name = "Copy of " + character.name;
					bs.description = character.description;
					bs.iconName = character.iconName;
					bs.sprite = character.sprite;
				}
				else
				{
					bs.name = "New Character";
					bs.description = "Description";
				}

				character = bs;
			}
			
			GUI.backgroundColor = Color.white;

			if (character != null)
			{
				NGUIEditorTools.DrawSeparator();

				// Navigation section
				GUILayout.BeginHorizontal();
				{
					if (mIndex == 0) GUI.color = Color.grey;
					if (GUILayout.Button("<<")) { mConfirmDelete = false; --mIndex; }
					GUI.color = Color.white;
					mIndex = EditorGUILayout.IntField(mIndex + 1, GUILayout.Width(40f)) - 1;
					GUILayout.Label("/ " + db.items.Count, GUILayout.Width(40f));
					if (mIndex + 1 == db.items.Count) GUI.color = Color.grey;
					if (GUILayout.Button(">>")) { mConfirmDelete = false; ++mIndex; }
					GUI.color = Color.white;
				}
				GUILayout.EndHorizontal();

				NGUIEditorTools.DrawSeparator();

				// Name and delete button
				GUILayout.BeginHorizontal();
				{
					string characterName = EditorGUILayout.TextField("Name", character.name, GUILayout.Width (165f));

					EditorGUILayout.LabelField("(ID: " + character.id16 + ")");

					GUI.backgroundColor = Color.red;

					if (GUILayout.Button("Delete", GUILayout.Width(55f)))
					{
						mConfirmDelete = true;
					}
					GUI.backgroundColor = Color.white;

					if (!characterName.Equals(character.name))
					{
						NGUIEditorTools.RegisterUndo("Rename Character", db);
						character.name = characterName;
					}
				}
				GUILayout.EndHorizontal();
				
				// Character Description
				EditorGUILayout.LabelField("Description");
				string characterDesc = GUILayout.TextArea(character.description, 200, GUILayout.Height(100f));
				
				// Character Icon
				string iconName = "";
				float iconSize = 64f;
				bool drawIcon = false;
				float extraSpace = 0f;
					
				Sprite sprite = null;

				// Draw the sprite selection popup
				sprite = EditorGUILayout.ObjectField("Sprite", character.sprite, typeof(Sprite)) as Sprite;

				GUILayout.Space(4f);
				
				// Save all values
				if (!characterDesc.Equals(character.description) ||
					!sprite.Equals(character.sprite))
				{
					NGUIEditorTools.RegisterUndo("Character Properties", db);
					character.description = characterDesc;
					character.sprite = sprite;
				}
				
				NGUIEditorTools.DrawSeparator();

				EditorGUILayout.LabelField("Stats", EditorStyles.boldLabel);

				int health, mana,
					moveRange;

				// health and mana
				GUILayout.BeginHorizontal();
				{
					health = EditorGUILayout.IntField(new GUIContent("Health", "The base health of the character."), character.health, GUILayout.Width(120f));
					mana = EditorGUILayout.IntField(new GUIContent("Mana", "The base mana of the character."), character.mana, GUILayout.Width(120f));
				}
				GUILayout.EndHorizontal();

				// other stuff
				GUILayout.BeginHorizontal();
				{
					moveRange = EditorGUILayout.IntField(new GUIContent("Move Range", "The base tile range that the character can move each turn."), character.moveRange, GUILayout.Width(120f));
				}
				GUILayout.EndHorizontal();

				if(health != character.health ||
				   mana != character.mana ||
				   moveRange != character.moveRange) {
					NGUIEditorTools.RegisterUndo("Character Stats", db);

					character.health = health;
					character.mana = mana;

					character.moveRange = moveRange;
				}
				
				NGUIEditorTools.DrawSeparator();

				EditorGUILayout.LabelField("Abilities", EditorStyles.boldLabel);

				NGUIEditorTools.DrawSeparator();
			}
		}
	}
}
*/