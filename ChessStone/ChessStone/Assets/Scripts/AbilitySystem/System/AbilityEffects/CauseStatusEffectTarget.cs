using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Puts a status effect on a target.
/// REQUIRES: target must be a GameCharacter
/// </summary>
[System.Serializable]
public class CauseStatusEffectTarget : AbilityEffect
{
	/// <summary>
	/// The status effect to be put on the target.
	/// </summary>
	public StatusEffect statusEffect;

	public override void OnCastEffect(GameCharacter self, Tile target = null) {
		GameCharacter targetChar = target.currUnit as GameCharacter;
		targetChar.AddStatusEffect(statusEffect);
	}

	public override AbilityEffect Clone() {
		CauseStatusEffectTarget temp = new CauseStatusEffectTarget();
		temp.id = id;
		temp.statusEffect = statusEffect;
		return temp;
	}
}