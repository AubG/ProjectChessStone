using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;
using SimpleJSON;

/// <summary>
/// Damages the caster for the amount.
/// </summary>
public class DamageSelf : SpellEffect
{
	/// <summary>
	/// The amount of damage.
	/// </summary>
	public int amount;

	public override void Populate(JSONClass info) {
		amount = info["amount"].AsInt;
	}

	public override void Activate(GameCharacter self, Tile target = null, SpellEffectFinishedDelegate finishedCallback = null)
	{
		self.Damage(amount, self, () => { finishedCallback(); });
	}
}