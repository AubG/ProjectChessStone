//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2012 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;

/// <summary>
/// A UI script that keeps an eye on the slot in character equipment.
/// </summary>

[AddComponentMenu("Spells/UI Spell Slot")]
public class UISpellSlot : UIAbilitySlot
{
	public string name;
	public SpellBox box;

	override protected GameSpell observedSpell
	{
		get
		{
			return (box != null) ? box.GetSpell(name) : null;
		}
	}
}