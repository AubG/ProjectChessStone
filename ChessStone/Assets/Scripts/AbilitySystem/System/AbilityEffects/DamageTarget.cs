using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Effects for abilities; what happens when they are cast.
/// REQUIRES: target must be a GameCharacter
/// </summary>
[System.Serializable]
public class DamageTarget : AbilityEffect
{
	/// <summary>
	/// The amount of damage.
	/// </summary>
	public float amount { get; set; }

	/// <summary>
	/// The type of damage.
	/// </summary>
	public DamageType type { get; set; }

	public override void OnCastEffect(GameUnit target) {
		GameCharacter targetChar = target as GameCharacter;
		DamageInfo damage = new DamageInfo(amount, type);
		targetChar.health.OnDamage(damage);
	}

	public override AbilityEffect Clone() {
		DamageTarget temp = new DamageTarget();
		temp.amount = amount;
		temp.type = type;
		return temp;
	}
}