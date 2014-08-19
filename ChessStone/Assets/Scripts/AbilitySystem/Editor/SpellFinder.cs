using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Spell System search functionality.
/// </summary>

public class SpellFinder : ScriptableWizard
{
	/// <summary>
	/// Private class used to return data from the Find function below.
	/// </summary>

	struct FindResult
	{
		public SpellDatabase db;
		public BaseSpell spell;
	}

	string mSpellName = "";
	List<FindResult> mResults = new List<FindResult>();

	/// <summary>
	/// Add a menu option to display this wizard.
	/// </summary>

	[MenuItem("Spells/Find Spell #&i")]
	static void FindSpell ()
	{
		ScriptableWizard.DisplayWizard<SpellFinder>("Find Spell");
	}

	/// <summary>
	/// Draw the custom wizard.
	/// </summary>
	void OnGUI ()
	{
		EditorGUIUtility.LookLikeControls(80f);
		string newSpellName = EditorGUILayout.TextField("Search for:", mSpellName);
		NGUIEditorTools.DrawSeparator();

		if (GUI.changed || newSpellName != mSpellName)
		{
			mSpellName = newSpellName;

			if (string.IsNullOrEmpty(mSpellName))
			{
				mResults.Clear();
			}
			else
			{
				FindAllByName(mSpellName);
			}
		}

		if (mResults.Count == 0)
		{
			if (!string.IsNullOrEmpty(mSpellName))
			{
				GUILayout.Label("No matches found");
			}
		}
		else
		{
			Print3("Spell ID", "Spell Name", "Path", false);
			NGUIEditorTools.DrawSeparator();

			foreach (FindResult fr in mResults)
			{
				if (Print3(SpellDatabase.FindSpellID(fr.spell).ToString(),
					fr.spell.name, NGUITools.GetHierarchy(fr.db.gameObject), true))
				{
					SpellDatabaseInspector.SelectIndex(fr.db, fr.spell);
					Selection.activeGameObject = fr.db.gameObject;
					EditorUtility.SetDirty(Selection.activeGameObject);
				}
			}
		}
	}

	/// <summary>
	/// Helper function used to print things in columns.
	/// </summary>

	bool Print3 (string a, string b, string c, bool button)
	{
		bool retVal = false;

		GUILayout.BeginHorizontal();
		{
			GUILayout.Label(a, GUILayout.Width(80f));
			GUILayout.Label(b, GUILayout.Width(160f));
			GUILayout.Label(c);

			if (button)
			{
				retVal = GUILayout.Button("Select", GUILayout.Width(60f));
			}
			else
			{
				GUILayout.Space(60f);
			}
		}
		GUILayout.EndHorizontal();
		return retVal;
	}

	/// <summary>
	/// Find spells by name.
	/// </summary>

	void FindAllByName (string partial)
	{
		partial = partial.ToLower();
		mResults.Clear();

		// Exact match comes first
		foreach (SpellDatabase db in SpellDatabase.list)
		{
			foreach (BaseSpell spell in db.spells)
			{
				if (spell.name.Equals(partial, System.StringComparison.OrdinalIgnoreCase))
				{
					FindResult fr = new FindResult();
					fr.db = db;
					fr.spell = spell;
					mResults.Add(fr);
				}
			}
		}

		// Next come partial matches that begin with the specified string
		foreach (SpellDatabase db in SpellDatabase.list)
		{
			foreach (BaseSpell spell in db.spells)
			{
				if (spell.name.StartsWith(partial, System.StringComparison.OrdinalIgnoreCase))
				{
					bool exists = false;

					foreach (FindResult res in mResults)
					{
						if (res.spell == spell)
						{
							exists = true;
							break;
						}
					}

					if (!exists)
					{
						FindResult fr = new FindResult();
						fr.db = db;
						fr.spell = spell;
						mResults.Add(fr);
					}
				}
			}
		}

		// Other partial matches come last
		foreach (SpellDatabase db in SpellDatabase.list)
		{
			foreach (BaseSpell spell in db.spells)
			{
				if (spell.name.ToLower().Contains(partial))
				{
					bool exists = false;

					foreach (FindResult res in mResults)
					{
						if (res.spell == spell)
						{
							exists = true;
							break;
						}
					}

					if (!exists)
					{
						FindResult fr = new FindResult();
						fr.db = db;
						fr.spell = spell;
						mResults.Add(fr);
					}
				}
			}
		}
	}
}