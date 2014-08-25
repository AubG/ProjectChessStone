using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Kills the target.
/// REQUIRES: target must be a GameCharacter
/// </summary>
[System.Serializable]
public class KillTarget : AbilityEffect
{
	public override void OnCastEffect(GameCharacter self, Tile target = null) {
		GameCharacter targetChar = target.currUnit as GameCharacter;
		DamageInfo damage = new DamageInfo(99, DamageType.True);
		targetChar.health.OnDamage(damage);
	}

	public override AbilityEffect Clone() {
		KillTarget temp = new KillTarget();
		temp.id = id;
		return temp;
	}
}