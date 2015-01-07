using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Puts a status effect on a target.
/// REQUIRES: target must be a GameCharacter
/// </summary>
public class CauseStatusEffectTarget : SpellEffect
{
	/// <summary>
	/// The status effect to be put on the target.
	/// </summary>
	public StatusEffect statusEffect;

	public override void Activate(GameCharacter self, Tile target = null, SpellEffectFinishedDelegate finishedCallback = null)
	{
		GameCharacter targetChar = target.currUnit as GameCharacter;
		targetChar.AddStatusEffect(statusEffect, self);

		finishedCallback();
	}
}