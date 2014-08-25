using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Kills the caster.
/// </summary>
[System.Serializable]
public class KillSelf : AbilityEffect
{
	public override void OnCastEffect(GameCharacter self, Tile target = null) {
		DamageInfo damage = new DamageInfo(99, DamageType.True);
		self.health.OnDamage(damage);
	}

	public override AbilityEffect Clone() {
		KillSelf temp = new KillSelf();
		temp.id = id;
		return temp;
	}
}