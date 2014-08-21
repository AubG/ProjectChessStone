using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
using X_UniTMX;

/// <summary>
/// Damages the target for the amount.
<<<<<<< HEAD
=======
=======

/// <summary>
/// Effects for abilities; what happens when they are cast.
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
/// REQUIRES: target must be a GameCharacter
/// </summary>
[System.Serializable]
public class DamageTarget : AbilityEffect
{
	/// <summary>
	/// The amount of damage.
	/// </summary>
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
	public float amount;

	public override void OnCastEffect(GameCharacter self, Tile target = null) {
		GameCharacter targetChar = target.currUnit as GameCharacter;
		DamageInfo damage = new DamageInfo(amount, DamageType.True);
<<<<<<< HEAD
		targetChar.health.OnDamage(damage);
		Debug.Log ("Damaged " + targetChar.gameName + ": " + targetChar.health.currentValue + "/" + targetChar.health.adjustedMaxValue);
=======
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
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
	}

	public override AbilityEffect Clone() {
		DamageTarget temp = new DamageTarget();
<<<<<<< HEAD
		temp.id = id;
		temp.amount = amount;
=======
<<<<<<< HEAD
		temp.id = id;
		temp.amount = amount;
=======
		temp.amount = amount;
		temp.type = type;
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
		return temp;
	}
}