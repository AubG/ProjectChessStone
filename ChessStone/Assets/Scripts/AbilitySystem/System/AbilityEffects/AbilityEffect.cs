using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// Effects for abilities; what happens when they are cast.
/// </summary>
[System.Serializable]
public abstract class AbilityEffect
{
	public enum Identifier {
		None,
		DamageTarget
	}

	public Identifier id;

	public virtual void OnCastEffect(GameUnit target) {}

	public abstract AbilityEffect Clone();
}