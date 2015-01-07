using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Kills the caster.
/// </summary>
public class KillSelf : SpellEffect
{
	public override void Activate(GameCharacter self, Tile target = null, SpellEffectFinishedDelegate finishedCallback = null)
	{
		self.Damage(99, self, () => { finishedCallback(); });
	}
}