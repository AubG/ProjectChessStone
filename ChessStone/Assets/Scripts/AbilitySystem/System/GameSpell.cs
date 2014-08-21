using UnityEngine;
<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;
=======
<<<<<<< HEAD
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;
=======
using System.Collections.Generic;
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7

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


<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
	private GameCharacter caster = null;
	private Tile targetTile = null;

	private int startTurnTime = 0;
<<<<<<< HEAD
=======
=======

>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7


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


<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
	public void Cast(GameCharacter self, Tile target = null) {
		caster = self;
		targetTile = target;

		self.StartCoroutine(WaitForCastEffect());
	}

	public void OnCastEffect() {
		for(int i = 0, il = baseSpell.abilityEffects.Count; i < il; i++) {
			AbilityEffect effect = baseSpell.abilityEffects[i];
<<<<<<< HEAD
=======
			Debug.Log (effect);
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
			effect.OnCastEffect(caster, targetTile);
		}
	}


	#endregion

	#region Helpers


	private IEnumerator WaitForCastEffect() {
		while(TurnTime.time < this.startTurnTime + this.baseSpell.turnCooldown) {
			yield return null;
		}

		OnCastEffect();
<<<<<<< HEAD
=======
=======
	public void OnCastEffect(GameUnit target) {
		for(int i = 0, il = baseSpell.abilityEffects.Count; i < il; i++) {
			AbilityEffect effect = baseSpell.abilityEffects[i];
			effect.OnCastEffect(target);
		}
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
	}


	#endregion
}