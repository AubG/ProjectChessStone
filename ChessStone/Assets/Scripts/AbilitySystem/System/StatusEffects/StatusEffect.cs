using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Effects for abilities; what happens when they are cast.
/// </summary>
[System.Serializable]
public class StatusEffect : ScriptableObject
{
	public enum Identifier {
		None,
		PeriodicDamage
	}

	public Identifier id;

	/// <summary>
	/// The life time of the status effects in terms of turns.
	/// </summary>
	public int turnLifeTime;

	public virtual void OnCastEffect(GameCharacter self, Tile target = null) {}

	public virtual StatusEffect Clone() {
		StatusEffect newEffect = new StatusEffect();
		newEffect.id = id;
		return newEffect;
	}
}