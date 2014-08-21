using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
<<<<<<< HEAD
using X_UniTMX;
=======
<<<<<<< HEAD
using X_UniTMX;
=======
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7

/// <summary>
/// Effects for abilities; what happens when they are cast.
/// </summary>
[System.Serializable]
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
public class AbilityEffect : ScriptableObject
{
	public enum Identifier {
		None,
		DamageTarget,
		DamageSelf
<<<<<<< HEAD
=======
=======
public abstract class AbilityEffect
{
	public enum Identifier {
		None,
		DamageTarget
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
	}

	public Identifier id;

<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
	public virtual void OnCastEffect(GameCharacter self, Tile target = null) {}

	public virtual AbilityEffect Clone() {
		AbilityEffect newEffect = new AbilityEffect();
		newEffect.id = id;
		return newEffect;
	}
<<<<<<< HEAD
=======
=======
	public virtual void OnCastEffect(GameUnit target) {}

	public abstract AbilityEffect Clone();
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
}