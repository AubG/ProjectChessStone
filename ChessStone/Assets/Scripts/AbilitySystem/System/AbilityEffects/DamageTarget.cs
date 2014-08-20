using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
<<<<<<< HEAD
using X_UniTMX;

/// <summary>
/// Damages the target for the amount.
=======

/// <summary>
/// Effects for abilities; what happens when they are cast.
>>>>>>> origin/master
/// REQUIRES: target must be a GameCharacter
/// </summary>
[System.Serializable]
public class DamageTarget : AbilityEffect
{
	/// <summary>
	/// The amount of damage.
	/// </summary>
<<<<<<< HEAD
	public float amount;

	public override void OnCastEffect(GameCharacter self, Tile target = null) {
		GameCharacter targetChar = target.currUnit as GameCharacter;
		DamageInfo damage = new DamageInfo(amount, DamageType.True);
=======
	public float amount { get; set; }

	/// <summary>
	/// The type of damage.
	/// </summary>
	public DamageType type { get; set; }

	public override void OnCastEffect(GameUnit target) {
		GameCharacter targetChar = target as GameCharacter;
		DamageInfo damage = new DamageInfo(amount, type);
>>>>>>> origin/master
		targetChar.health.OnDamage(damage);
	}

	public override AbilityEffect Clone() {
		DamageTarget temp = new DamageTarget();
<<<<<<< HEAD
		temp.id = id;
		temp.amount = amount;
=======
		temp.amount = amount;
		temp.type = type;
>>>>>>> origin/master
		return temp;
	}
}