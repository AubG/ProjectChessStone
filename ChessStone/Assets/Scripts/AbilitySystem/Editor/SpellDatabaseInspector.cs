using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

/// <summary>
/// Inspector class used to edit Inventory Databases.
/// </summary>

[CustomEditor(typeof(SpellDatabase))]
public class SpellDatabaseInspector : Editor
{
	static int mIndex = 0;

	AbilityEffect.Identifier mSetIden = AbilityEffect.Identifier.None;
	BaseSpell.CastType mSetType = BaseSpell.CastType.Passive;

	bool mConfirmDelete = false;
	bool mConfirmChangeType = false;

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
	/// Helper function that sets the index to the index of the specified spell.
	/// </summary>

	public static void SelectIndex (SpellDatabase db, BaseSpell spell)
	{
		mIndex = 0;

		foreach (BaseSpell s in db.spells)
		{
			if (s == spell) break;
			++mIndex;
		}
	}

	/// <summary>
	/// Draw the inspector widget.
	/// </summary>

	public override void OnInspectorGUI ()
	{
		EditorGUIUtility.LookLikeControls(80f);
		SpellDatabase db = target as SpellDatabase;
		NGUIEditorTools.DrawSeparator();

		BaseSpell spell = null;

		if (db.spells == null || db.spells.Count == 0)
		{
			mIndex = 0;
		}
		else
		{
			mIndex = Mathf.Clamp(mIndex, 0, db.spells.Count - 1);
			spell = db.spells[mIndex];
		}

		if (mConfirmDelete)
		{
			// Show the confirmation dialog
			GUILayout.Label("Are you sure you want to delete '" + spell.name + "'?");
			NGUIEditorTools.DrawSeparator();

			GUILayout.BeginHorizontal();
			{
				GUI.backgroundColor = Color.green;
				if (GUILayout.Button("Cancel")) mConfirmDelete = false;
				GUI.backgroundColor = Color.red;

				if (GUILayout.Button("Delete"))
				{
					NGUIEditorTools.RegisterUndo("Delete Spell", db);
					db.spells.RemoveAt(mIndex);
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
				foreach (BaseSpell s in db.spells) s.iconAtlas = atlas;
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
			
			if (GUILayout.Button("New Spell"))
			{
				NGUIEditorTools.RegisterUndo("Add Spell", db);

				BaseSpell bs = new BaseSpell();
				bs.iconAtlas = db.iconAtlas;
				bs.id16 = (db.spells.Count > 0) ? db.spells[db.spells.Count - 1].id16 + 1 : 0;
				db.spells.Add(bs);
				mIndex = db.spells.Count - 1;

				if (spell != null)
				{
					bs.name = "Copy of " + spell.name;
					bs.description = spell.description;
					bs.iconName = spell.iconName;

					foreach (AbilityEffect effect in spell.abilityEffects)
					{
						bs.abilityEffects.Add(effect);
					}
				}
				else
				{
					bs.name = "New Spell";
					bs.description = "Spell Description";
				}

				spell = bs;
			}
			
			GUI.backgroundColor = Color.white;

			if (spell != null)
			{
				NGUIEditorTools.DrawSeparator();

				// Navigation section
				GUILayout.BeginHorizontal();
				{
					if (mIndex == 0) GUI.color = Color.grey;
					if (GUILayout.Button("<<")) { mConfirmDelete = false; --mIndex; }
					GUI.color = Color.white;
					mIndex = EditorGUILayout.IntField(mIndex + 1, GUILayout.Width(40f)) - 1;
					GUILayout.Label("/ " + db.spells.Count, GUILayout.Width(40f));
					if (mIndex + 1 == db.spells.Count) GUI.color = Color.grey;
					if (GUILayout.Button(">>")) { mConfirmDelete = false; ++mIndex; }
					GUI.color = Color.white;
				}
				GUILayout.EndHorizontal();

				NGUIEditorTools.DrawSeparator();

				// Spell name and delete spell button
				GUILayout.BeginHorizontal();
				{
					string spellName = EditorGUILayout.TextField("Spell Name", spell.name, GUILayout.Width (165f));

					EditorGUILayout.LabelField("(ID: " + spell.id16 + ")");

					GUI.backgroundColor = Color.red;

					if (GUILayout.Button("Delete", GUILayout.Width(55f)))
					{
						mConfirmDelete = true;
					}
					GUI.backgroundColor = Color.white;

					if (!spellName.Equals(spell.name))
					{
						NGUIEditorTools.RegisterUndo("Rename Spell", db);
						spell.name = spellName;
					}
				}
				GUILayout.EndHorizontal();
				
				// Spell Description
				EditorGUILayout.LabelField("Description");
				string spellDesc = GUILayout.TextArea(spell.description, 200, GUILayout.Height(100f));
				
				// Spell Icon
				string iconName = "";
				float iconSize = 64f;
				bool drawIcon = false;
				float extraSpace = 0f;

				if (spell.iconAtlas != null)
				{
					BetterList<string> sprites = spell.iconAtlas.GetListOfSprites();
					sprites.Insert(0, "<None>");
					
					int index = 0;
					string spriteName = (spell.iconName != null) ? spell.iconName : sprites[0];

					// We need to find the sprite in order to have it selected
					if (!string.IsNullOrEmpty(spriteName))
					{
						for (int i = 1; i < sprites.size; ++i)
						{
							if (spriteName.Equals(sprites[i], System.StringComparison.OrdinalIgnoreCase))
							{
								index = i;
								break;
							}
						}
					}

					// Draw the sprite selection popup
					index = EditorGUILayout.Popup("Icon", index, sprites.ToArray());
					UIAtlas.Sprite sprite = (index > 0) ? spell.iconAtlas.GetSprite(sprites[index]) : null;
					
					GUILayout.Space(4f);

					if (sprite != null)
					{
						iconName = sprite.name;

						Material mat = spell.iconAtlas.spriteMaterial;

						if (mat != null)
						{
							Texture2D tex = mat.mainTexture as Texture2D;

							if (tex != null)
							{
								drawIcon = true;
								Rect rect = sprite.outer;

								if (spell.iconAtlas.coordinates == UIAtlas.Coordinates.Pixels)
								{
									rect = NGUIMath.ConvertToTexCoords(rect, tex.width, tex.height);
								}

								GUILayout.BeginHorizontal();
								{
									GUILayout.Space(Screen.width - iconSize);
									DrawSprite(tex, rect, null, false);
								}
								GUILayout.EndHorizontal();

								extraSpace = iconSize * (float)sprite.outer.height / sprite.outer.width;
							}
						}
					}
				}

				// Spell Tile Range
				int range = EditorGUILayout.IntField("Tile Range", spell.tileRange, GUILayout.Width(120f));

				// Spell Targeting Data
				EditorGUILayout.LabelField("Allowed Targets", EditorStyles.boldLabel);
				bool characterTargetsAllowed = EditorGUILayout.Toggle("Characters", spell.targetingData.canTargetCharacters);
				
				GUILayout.Space(iconSize);
				
				// Save all values
				if (!spellDesc.Equals(spell.description) ||
					!iconName.Equals(spell.iconName) ||
				   	range != spell.tileRange ||
				    characterTargetsAllowed != spell.targetingData.canTargetCharacters)
				{
					NGUIEditorTools.RegisterUndo("Spell Properties", db);
					spell.description = spellDesc;
					spell.iconName = iconName;
					spell.tileRange = range;
					spell.targetingData.canTargetCharacters = characterTargetsAllowed;
				}
				
				NGUIEditorTools.DrawSeparator();
				
				if (mConfirmChangeType)
				{
					// Show the confirmation dialog
					GUILayout.Label("Change cast type?");

					GUILayout.BeginHorizontal();
					{
						GUI.backgroundColor = Color.green;
						if (GUILayout.Button("Cancel")) mConfirmChangeType = false;
						GUI.backgroundColor = Color.red;
		
						if (GUILayout.Button("Change"))
						{
							NGUIEditorTools.RegisterUndo("Change Cast Type", db);
							spell.castType = mSetType;
							mConfirmChangeType = false;
						}
						GUI.backgroundColor = Color.white;
					}
					GUILayout.EndHorizontal();
				}
				else
				{
					BaseSpell.CastType type = spell.castType;
					
					GUILayout.BeginHorizontal();
					{
						EditorGUILayout.LabelField("Cast Type", EditorStyles.boldLabel);
						
						bool alreadySet = false;
						foreach(BaseSpell.CastType typeVal in Enum.GetValues(typeof(BaseSpell.CastType))) {
							alreadySet = spell.castType == typeVal;
							if (alreadySet) GUI.color = Color.grey;
							if (GUILayout.Button(typeVal.ToString()) && !alreadySet) {
								mConfirmChangeType = true;
								mSetType = typeVal;
							}
							GUI.color = Color.white;
						}
					}
					GUILayout.EndHorizontal();
				}
				
				NGUIEditorTools.DrawSeparator();

				EditorGUILayout.LabelField("Effects", EditorStyles.boldLabel);

				if (spell.abilityEffects != null)
				{
					for (int i = 0; i < spell.abilityEffects.Count; ++i)
					{
						AbilityEffect effect = spell.abilityEffects[i];
						
						GUILayout.BeginHorizontal();
						{
							EditorGUILayout.LabelField("Effect: " + effect.id, EditorStyles.whiteLargeLabel);
							
							GUI.backgroundColor = Color.red;
							if (GUILayout.Button("X", GUILayout.Width(20f)))
							{
								NGUIEditorTools.RegisterUndo("Delete Ability Effect", db);
								spell.abilityEffects.RemoveAt(i);
								--i;
								continue;
							}
							GUI.backgroundColor = Color.white;
						}
						GUILayout.EndHorizontal();
						
						switch(effect.id) {
							case AbilityEffect.Identifier.DamageTarget:
								DamageTarget damageTarget = effect as DamageTarget;
								float dt_amount = EditorGUILayout.FloatField("Amount", damageTarget.amount, GUILayout.Width(120f));
								if(dt_amount != damageTarget.amount) {
									NGUIEditorTools.RegisterUndo("Ability Effects", db);
									((DamageTarget)effect).amount = dt_amount;
								}
								break;
							case AbilityEffect.Identifier.DamageSelf:
								DamageSelf damageSelf = effect as DamageSelf;
								float ds_amount = EditorGUILayout.FloatField("Amount", damageSelf.amount, GUILayout.Width(120f));
								if(ds_amount != damageSelf.amount) {
									NGUIEditorTools.RegisterUndo("Ability Effects", db);
									((DamageSelf)effect).amount = ds_amount;
								}
								break;
						}
					}
					
					GUILayout.BeginHorizontal();
					{
						mSetIden = (AbilityEffect.Identifier)EditorGUILayout.EnumPopup(mSetIden, GUILayout.Width(240f));
						
						if (GUILayout.Button("Add Effect", GUILayout.Width(80f)) && mSetIden != AbilityEffect.Identifier.None)
						{
							NGUIEditorTools.RegisterUndo("Add Effect", db);
							
							AbilityEffect effect = null;
							
							switch(mSetIden) {
								case AbilityEffect.Identifier.DamageTarget:
									effect = new DamageTarget();
									break;
								case AbilityEffect.Identifier.DamageSelf:
									effect = new DamageSelf();
									break;
							}

							spell.abilityEffects.Add(effect);
							effect.id = mSetIden;
						}
					}
					GUILayout.EndHorizontal();
				}

				NGUIEditorTools.DrawSeparator();
			}
		}
	}
}