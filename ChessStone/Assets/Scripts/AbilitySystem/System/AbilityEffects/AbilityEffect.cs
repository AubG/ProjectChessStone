using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using X_UniTMX;

/// <summary>
/// Effects for abilities; what happens when they are cast.
/// </summary>
[System.Serializable]
public class AbilityEffect : ScriptableObject
{
	public enum Identifier {
		None,
		DamageTarget,
		DamageSelf
	}

	public Identifier id;

	public virtual void OnCastEffect(GameCharacter self, Tile target = null) {}

	public virtual AbilityEffect Clone() {
		AbilityEffect newEffect = new AbilityEffect();
		newEffect.id = id;
		return newEffect;
	}
}