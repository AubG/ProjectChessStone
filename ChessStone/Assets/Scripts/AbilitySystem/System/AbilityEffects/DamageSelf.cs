using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Damages the caster for the amount.
/// </summary>
[System.Serializable]
public class DamageSelf : AbilityEffect
{
	/// <summary>
	/// The amount of damage.
	/// </summary>
	public float amount;

	public override void OnCastEffect(GameCharacter self, GameUnit target = null) {
		DamageInfo damage = new DamageInfo(amount, DamageType.True);
		self.health.OnDamage(damage);
	}

	public override AbilityEffect Clone() {
		DamageSelf temp = new DamageSelf();
		temp.id = id;
		temp.amount = amount;
		return temp;
	}
}