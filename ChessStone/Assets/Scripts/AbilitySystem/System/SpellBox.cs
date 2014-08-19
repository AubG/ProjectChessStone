using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Inventory system -- Equipment class works with InvAttachmentPoints and allows to visually equip and remove items.
/// </summary>

[AddComponentMenu("Spells/Box")]
public class SpellBox : MonoBehaviour
{
	[SerializeField]
	private GameSpell[] mSpells;

	/// <summary>
	/// Whether the box contains the spell.
	/// </summary>
	public GameSpell GetSpell (string name)
	{
		if (mSpells != null)
		{
			for (int i = 0, imax = mSpells.Length; i < imax; ++i)
			{
				if (mSpells[i].name == name) return mSpells[i];
			}
		}
		return null;
	}
}