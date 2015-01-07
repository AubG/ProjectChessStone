using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;
using SimpleJSON;

/// <summary>
/// Damages the target for the amount.
/// REQUIRES: target must be a GameCharacter
/// </summary>
public class DamageTarget : SpellEffect
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
		switch(target.currUnit.unitType) {
		case UnitType.Character:
			GameCharacter targetChar = (GameCharacter)target.currUnit;
			targetChar.Damage(amount, self, () => { finishedCallback(); });
			break;
		case UnitType.Link:
			GameUnitLink targetLink = (GameUnitLink)target.currUnit;
			targetLink.head.Damage(amount, self, () => { finishedCallback(); });
			break;
		}
	}
}