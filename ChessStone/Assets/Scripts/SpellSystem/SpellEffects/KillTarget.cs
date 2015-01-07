using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Kills the target.
/// REQUIRES: target must be a GameCharacter
/// </summary>
public class KillTarget : SpellEffect
{
	public override void Activate(GameCharacter self, Tile target = null, SpellEffectFinishedDelegate finishedCallback = null)
	{
		switch(target.currUnit.unitType) {
		case UnitType.Character:
			GameCharacter targetChar = (GameCharacter)target.currUnit;
			targetChar.Damage(99, self, () => { finishedCallback(); });
			break;
		case UnitType.Link:
			GameUnitLink targetLink = (GameUnitLink)target.currUnit;
			targetLink.head.Damage(99, self, () => { finishedCallback(); });
			break;
		}
	}
}