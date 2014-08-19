using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Damages the target for the amount.
/// REQUIRES: target must be a GameCharacter
/// </summary>
[System.Serializable]
public class DamageTarget : AbilityEffect
{
	/// <summary>
	/// The amount of damage.
	/// </summary>
	public float amount;

	public override void OnCastEffect(GameCharacter self, GameUnit target = null) {
		GameCharacter targetChar = target as GameCharacter;
		DamageInfo damage = new DamageInfo(amount, DamageType.True);
		targetChar.health.OnDamage(damage);
	}

	public override AbilityEffect Clone() {
		DamageTarget temp = new DamageTarget();
		temp.id = id;
		temp.amount = amount;
		return temp;
	}
}