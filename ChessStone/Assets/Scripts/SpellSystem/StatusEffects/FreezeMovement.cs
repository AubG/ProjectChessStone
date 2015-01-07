using UnityEngine;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Damages the caster every turn for the specified amount.
/// </summary>
[System.Serializable]
public class FreezeMovement : StatusEffect
{

	public override void OnTurnUpdate()
	{
		base.OnTurnUpdate();
	}

	public override StatusEffect Clone()
	{
		FreezeMovement temp = new FreezeMovement();
		temp.id = id;
		temp.turnLifeTime = turnLifeTime;
		return temp;
	}
}