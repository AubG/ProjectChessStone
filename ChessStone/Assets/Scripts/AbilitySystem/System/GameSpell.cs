using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class GameSpell
{
	#region Spell Data


	// ID of the base spell used to create this game spell
	[SerializeField] int mBaseSpellID = 0;

	// Cached for speed
	BaseSpell mBaseSpell;

	/// <summary>
	/// ID of the base spell used to create this one.
	/// </summary>
	public int baseSpellID { get { return mBaseSpellID; } }

	/// <summary>
	/// Base spell used by this game spell.
	/// </summary>
	public BaseSpell baseSpell
	{
		get
		{
			if (mBaseSpell == null)
			{
				mBaseSpell = SpellDatabase.FindByID(baseSpellID);
			}
			return mBaseSpell;
		}
	}

	/// <summary>
	/// Game spell's name.
	/// </summary>
	public string name
	{
		get
		{
			if (baseSpell == null) return null;
			return baseSpell.name;
		}
	}


	#endregion

	#region Game Data





	#endregion

	#region Initialization


	/// <summary>
	/// Create a game spell with the specified ID.
	/// </summary>
	public GameSpell (int id) { mBaseSpellID = id; }

	/// <summary>
	/// Create a game spell with the specified ID and base spell.
	/// </summary>
	public GameSpell (int id, BaseSpell bi) { mBaseSpellID = id; mBaseSpell = bi; }


	#endregion

	#region Public Interaction


	public void OnCastEffect(GameUnit target) {
		for(int i = 0, il = baseSpell.abilityEffects.Count; i < il; i++) {
			AbilityEffect effect = baseSpell.abilityEffects[i];
			effect.OnCastEffect(target);
		}
	}


	#endregion
}