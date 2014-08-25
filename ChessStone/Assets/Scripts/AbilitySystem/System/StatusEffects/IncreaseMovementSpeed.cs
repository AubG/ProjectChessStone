using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Increase the movement speed of the target.
/// </summary>
[System.Serializable]
public class IncreaseMovementSpeed : StatusEffect
{
	/// <summary>
	/// The increase amount; can be negative to reduce the movement speed.
	/// </summary>
	public int amount;

	public override void Activate(GameCharacter target)
	{
		base.Activate(target);

		// increase the target's movement speed
	}

	protected override void Expire() {
		base.Expire();

		// reset the target's movement speed
	}

	public override StatusEffect Clone()
	{
		IncreaseMovementSpeed temp = new IncreaseMovementSpeed();
		temp.id = id;
		temp.amount = amount;
		temp.turnLifeTime = turnLifeTime;
		return temp;
	}
}