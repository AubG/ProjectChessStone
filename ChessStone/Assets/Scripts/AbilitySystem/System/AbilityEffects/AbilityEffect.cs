using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
<<<<<<< HEAD
using X_UniTMX;
=======
>>>>>>> origin/master

/// <summary>
/// Effects for abilities; what happens when they are cast.
/// </summary>
[System.Serializable]
<<<<<<< HEAD
public class AbilityEffect : ScriptableObject
{
	public enum Identifier {
		None,
		DamageTarget,
		DamageSelf
=======
public abstract class AbilityEffect
{
	public enum Identifier {
		None,
		DamageTarget
>>>>>>> origin/master
	}

	public Identifier id;

<<<<<<< HEAD
	public virtual void OnCastEffect(GameCharacter self, Tile target = null) {}

	public virtual AbilityEffect Clone() {
		AbilityEffect newEffect = new AbilityEffect();
		newEffect.id = id;
		return newEffect;
	}
=======
	public virtual void OnCastEffect(GameUnit target) {}

	public abstract AbilityEffect Clone();
>>>>>>> origin/master
}