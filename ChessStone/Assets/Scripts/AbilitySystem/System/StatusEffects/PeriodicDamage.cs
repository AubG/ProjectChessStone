using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Damages the caster every turn for the specified amount.
/// </summary>
[System.Serializable]
public class PeriodicDamage : AbilityEffect
{
	/// <summary>
	/// The amount of damage.
	/// </summary>
	public float amount;

	public override void OnCastEffect(GameCharacter self, Tile target = null) {
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