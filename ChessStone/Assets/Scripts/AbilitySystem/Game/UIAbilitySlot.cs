//----------------------------------------------
//            NGUI: Next-Gen UI kit
// Copyright © 2011-2012 Tasharen Entertainment
//----------------------------------------------

using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Abstract UI component observing an spell somewhere in the inventory. This spell can be equipped on
/// the character, it can be lying in a chest, or it can be hot-linked by another player. Either way,
/// all the common behavior is in this class. What the observed spell actually is...
/// that's up to the derived class to determine.
/// </summary>

public abstract class UIAbilitySlot : MonoBehaviour
{
	public UISprite icon;
	public UIWidget background;
	public UILabel label;

	public AudioClip grabSound;
	public AudioClip placeSound;
	public AudioClip errorSound;

	GameSpell mSpell;
	string mText = "";

	/// <summary>
	/// This function should return the spell observed by this UI class.
	/// </summary>

	abstract protected GameSpell observedSpell { get; }

	/// <summary>
	/// Show a tooltip for the spell.
	/// </summary>
	void OnTooltip (bool show)
	{
		GameSpell spell = show ? mSpell : null;

		if (spell != null)
		{
			BaseSpell bs = spell.baseSpell;

			if (bs != null)
			{
				string t = spell.name + "\n";
				if (!string.IsNullOrEmpty(bs.description)) t += "\n[FF9900]" + bs.description;
				UITooltip.ShowText(t);
				return;
			}
		}
		UITooltip.ShowText(null);
	}
}