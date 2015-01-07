using UnityEngine;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Status effects that can be applied to GameCharacters.
/// </summary>
[System.Serializable]
public class StatusEffect : ScriptableObject
{
	#region Identification Data


	public enum Identifier
	{
		None,
		PeriodicDamage,
		FreezeMovement,
		IncreaseMovementSpeed
	}

	public Identifier id;


	#endregion

	#region Game Data


	protected GameCharacter source;
	protected GameCharacter target;

	/// <summary>
	/// Whether the buff has expired and should be culled.
	/// </summary>
	public bool expired { get; private set; }

	/// <summary>
	/// The life time of the status effects in terms of turns.
	/// </summary>
	public int turnLifeTime;

	/// <summary>
	/// The start time of the status effect since the StatusEffect was Activated.
	/// </summary>
	private int turnStartTime;


	#endregion

	#region Init


	public virtual void Activate(GameCharacter target, GameCharacter source)
	{
		target = this.target;

		turnStartTime = TurnTime.time;
	}


	#endregion

	#region Update


	public virtual void OnTurnUpdate()
	{
		if(TurnTime.time > turnStartTime + turnLifeTime)
		{
			Expire();
		}
	}


	#endregion

	#region Interaction


	public virtual StatusEffect Clone()
	{
		StatusEffect newEffect = new StatusEffect();
		newEffect.id = id;
		return newEffect;
	}


	#endregion

	#region Helpers

	
	protected virtual void Expire()
	{
		expired = true;
	}


	#endregion
}