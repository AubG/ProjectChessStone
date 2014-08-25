using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Damages the target periodically.
/// </summary>
[System.Serializable]
public class PeriodicDamage : StatusEffect
{
	/// <summary>
	/// The amount of damage.
	/// </summary>
	public float amount;

	/// <summary>
	/// How often the damage should be inflicted.
	/// </summary>
	public int turnRateTime;

	public override void OnTurnUpdate()
	{
		base.OnTurnUpdate();

		DamageInfo damage = new DamageInfo(amount, DamageType.True);
		target.health.OnDamage(damage);
	}

	public override StatusEffect Clone()
	{
		PeriodicDamage temp = new PeriodicDamage();
		temp.id = id;
		temp.amount = amount;
		temp.turnLifeTime = turnLifeTime;
		temp.turnRateTime = turnRateTime;
		return temp;
	}
}