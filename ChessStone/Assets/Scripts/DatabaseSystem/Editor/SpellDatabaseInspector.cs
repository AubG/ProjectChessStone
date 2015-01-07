using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;

/// <summary>
/// Inspector class used to edit Inventory Databases.
/// </summary>
/*
[CustomEditor(typeof(SpellDatabase))]
public class SpellDatabaseInspector : DatabaseEditor
{
	StatusEffect.Identifier mOldStatIden = StatusEffect.Identifier.None;
	StatusEffect.Identifier mSetStatIden = StatusEffect.Identifier.None;
	AbilityEffect.Identifier mSetIden = AbilityEffect.Identifier.None;
	BaseSpell.CastType mSetType = BaseSpell.CastType.Passive;
	
	bool mConfirmChangeType = false;
	bool mConfirmChangeStatIden = false;

	/// <summary>
	/// Draw the inspector widget.
	/// </summary>

	public override void OnInspectorGUI ()
	{
		EditorGUIUtility.LookLikeControls(80f);
		SpellDatabase db = target as SpellDatabase;
		NGUIEditorTools.DrawSeparator();

		BaseSpell spell = null;

		if (db.items == null || db.items.Count == 0)
		{
			mIndex = 0;
		}
		else
		{
			mIndex = Mathf.Clamp(mIndex, 0, db.items.Count - 1);
			spell = db.items[mIndex];
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
				foreach (BaseSpell s in db.items) s.iconAtlas = atlas;
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

				bs.id16 = (db.items.Count > 0) ? db.items[db.items.Count - 1].id16 + 1 : 0;
				db.items.Add(bs);
				mIndex = db.items.Count - 1;

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
					bs.description = "Description";
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
					GUILayout.Label("/ " + db.items.Count, GUILayout.Width(40f));
					if (mIndex + 1 == db.items.Count) GUI.color = Color.grey;
					if (GUILayout.Button(">>")) { mConfirmDelete = false; ++mIndex; }
					GUI.color = Color.white;
				}
				GUILayout.EndHorizontal();

				NGUIEditorTools.DrawSeparator();

				// Spell name and delete spell button
				GUILayout.BeginHorizontal();
				{
					string spellName = EditorGUILayout.TextField("Name", spell.name, GUILayout.Width (165f));

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
				int range = Math.Max(1, EditorGUILayout.IntField("Tile Range", spell.tileRange, GUILayout.Width(120f)));

				// Spell Targeting Data
				EditorGUILayout.LabelField("Allowed Targets", EditorStyles.boldLabel);
				bool allowEnemies = EditorGUILayout.Toggle("Enemies", spell.targetingData.allowEnemies);
				bool allowFriends = EditorGUILayout.Toggle("Friends", spell.targetingData.allowFriends);

				GUILayout.Space(iconSize);
				
				// Save all values
				if (!spellDesc.Equals(spell.description) ||
					!iconName.Equals(spell.iconName) ||
				   	range != spell.tileRange ||
				    allowEnemies != spell.targetingData.allowEnemies ||
				    allowFriends != spell.targetingData.allowFriends)
				{
					NGUIEditorTools.RegisterUndo("Spell Properties", db);
					spell.description = spellDesc;
					spell.iconName = iconName;
					spell.tileRange = range;
					spell.targetingData.allowEnemies = allowEnemies;
					spell.targetingData.allowFriends = allowFriends;
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
					AbilityEffect effect = null;
					for (int i = 0; i < spell.abilityEffects.Count; ++i)
					{
						effect = spell.abilityEffects[i];

						if(effect == null) {
							spell.abilityEffects.RemoveAt(i);
							i--;
							continue;
						}
						
						GUILayout.BeginHorizontal();
						{
							EditorGUILayout.LabelField("Ability Effect: " + effect.id, EditorStyles.whiteLargeLabel);
							
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

						// show different variables based on which AbilityEffect child is being edited
						switch(effect.id)
						{
						case AbilityEffect.Identifier.DamageTarget:
							DamageTarget dt = effect as DamageTarget;
							int dt_amount = EditorGUILayout.IntField("Amount", dt.amount, GUILayout.Width(120f));
							if(dt_amount != dt.amount) {
								NGUIEditorTools.RegisterUndo("Ability Effects", db);
								dt.amount = dt_amount;
							}
							break;
						case AbilityEffect.Identifier.DamageSelf:
							DamageSelf ds = effect as DamageSelf;
							int ds_amount = EditorGUILayout.IntField("Amount", ds.amount, GUILayout.Width(120f));
							if(ds_amount != ds.amount) {
								NGUIEditorTools.RegisterUndo("Ability Effects", db);
								ds.amount = ds_amount;
							}
							break;
						case AbilityEffect.Identifier.CauseStatusEffectTarget:
							CauseStatusEffectTarget cset = effect as CauseStatusEffectTarget;

							if (mConfirmChangeStatIden && (cset.statusEffect == null || cset.statusEffect.id == mOldStatIden)) {
								bool changeConfirmed = false;

								if(cset.statusEffect != null) {
									// Show the confirmation dialog
									GUILayout.Label("Really change status effect? All previous status effect data will be erased.");
									
									GUILayout.BeginHorizontal();
									{
										GUI.backgroundColor = Color.green;
										if (GUILayout.Button("Cancel")) mConfirmChangeStatIden = false;

										GUI.backgroundColor = Color.red;
										if (GUILayout.Button("Change")) changeConfirmed = true;
									}
									GUILayout.EndHorizontal();

									GUI.backgroundColor = Color.white;
								} else {
									changeConfirmed = true;
								}

								if(changeConfirmed) {
									NGUIEditorTools.RegisterUndo("Change Status Effect", db);

									StatusEffect newEffect = null;
											
									// create the appropriate StatusEffect child
									switch(mSetStatIden)
									{
									case StatusEffect.Identifier.PeriodicDamage:
										newEffect = new PeriodicDamage();
										break;
									case StatusEffect.Identifier.FreezeMovement:
										newEffect = new FreezeMovement();
										break;
									case StatusEffect.Identifier.IncreaseMovementSpeed:
										newEffect = new IncreaseMovementSpeed();
										break;
									}
											
									cset.statusEffect = newEffect;
									newEffect.id = mSetStatIden;

									mSetStatIden = StatusEffect.Identifier.None;
									mConfirmChangeStatIden = false;
								}
							}
							else
							{
								StatusEffect statusEffect = cset.statusEffect;

								if(statusEffect != null) mSetStatIden = cset.statusEffect.id;
								
								GUILayout.BeginHorizontal();
								{
									EditorGUILayout.LabelField("Status Effect: ", GUILayout.Width(80f));

									StatusEffect.Identifier temp = (statusEffect != null) ? statusEffect.id : StatusEffect.Identifier.None;
									temp = (StatusEffect.Identifier)EditorGUILayout.EnumPopup(temp, GUILayout.Width(120f));
								
									if (temp != StatusEffect.Identifier.None && temp != mSetStatIden)
									{
										mOldStatIden = mSetStatIden;
										mSetStatIden = temp;
										mConfirmChangeStatIden = true;
										continue;
									}
								}
								GUILayout.EndHorizontal();
								
								if(statusEffect != null) {
									switch(statusEffect.id)
									{
									case StatusEffect.Identifier.PeriodicDamage:
										PeriodicDamage pd = statusEffect as PeriodicDamage;
										
										int pd_duration = EditorGUILayout.IntField("Duration", pd.turnLifeTime, GUILayout.Width(120f));
										int pd_rate = EditorGUILayout.IntField("Rate", pd.turnRateTime, GUILayout.Width(120f));
										int pd_amount = EditorGUILayout.IntField("Amount", pd.amount, GUILayout.Width(120f));
										
										if(pd_duration != pd.turnLifeTime || 
										   pd_rate != pd.turnRateTime ||
										   pd_amount != pd.amount) {
											NGUIEditorTools.RegisterUndo("Status Effects", db);
											pd.turnLifeTime = pd_duration;
											pd.turnRateTime = pd_rate;
											pd.amount = pd_amount;
										}
										break;
									case StatusEffect.Identifier.FreezeMovement:
										break;
									case StatusEffect.Identifier.IncreaseMovementSpeed:
										IncreaseMovementSpeed ims = statusEffect as IncreaseMovementSpeed;
										
										int ims_duration = EditorGUILayout.IntField("Duration", ims.turnLifeTime, GUILayout.Width(120f));
										int ims_amount = EditorGUILayout.IntField("Amount", ims.amount, GUILayout.Width(120f));
										
										if(ims_duration != ims.turnLifeTime || 
										   ims_amount != ims.amount) {
											NGUIEditorTools.RegisterUndo("Status Effects", db);
											ims.turnLifeTime = ims_duration;
											ims.amount = ims_amount;
										}
										break;
									}
								}
							}

							break;
						}
					}
					
					GUILayout.BeginHorizontal();
					{
						mSetIden = (AbilityEffect.Identifier)EditorGUILayout.EnumPopup(mSetIden, GUILayout.Width(232f));
						
						if (GUILayout.Button("Add Ability Effect", GUILayout.Width(120f)) && mSetIden != AbilityEffect.Identifier.None)
						{
							NGUIEditorTools.RegisterUndo("Add Ability Effect", db);
							
							AbilityEffect newEffect = null;

							// create the appropriate AbilityEffect child
							switch(mSetIden)
							{
							case AbilityEffect.Identifier.DamageTarget:
								newEffect = new DamageTarget();
								break;
							case AbilityEffect.Identifier.DamageSelf:
								newEffect = new DamageSelf();
								break;
							case AbilityEffect.Identifier.KillTarget:
								newEffect = new KillTarget();
								break;
							case AbilityEffect.Identifier.KillSelf:
								newEffect = new KillSelf();
								break;
							case AbilityEffect.Identifier.CauseStatusEffectTarget:
								newEffect = new CauseStatusEffectTarget();
								break;
							}

							spell.abilityEffects.Add(newEffect);
							newEffect.id = mSetIden;
						}
					}
					GUILayout.EndHorizontal();
				}

				NGUIEditorTools.DrawSeparator();
			}
		}
	}
}
*/