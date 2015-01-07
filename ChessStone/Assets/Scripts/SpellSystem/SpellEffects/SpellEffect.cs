using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;

using SimpleJSON;

public delegate void SpellEffectFinishedDelegate();

/// <summary>
/// Effects for abilities; what happens when they are cast.
/// </summary>
public class SpellEffect
{
	public virtual void Populate(JSONClass info) {}

	public virtual void Activate(GameCharacter self, Tile target = null, SpellEffectFinishedDelegate finishedCallback = null) {}
}