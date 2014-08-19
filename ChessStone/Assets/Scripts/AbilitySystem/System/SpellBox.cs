using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Inventory system -- Equipment class works with InvAttachmentPoints and allows to visually equip and remove items.
/// </summary>

[AddComponentMenu("Spells/Box")]
public class SpellBox : MonoBehaviour
{
	#region Data


	[SerializeField]
	private GameSpell[] mSpells;
	public GameSpell[] spells {
		get { return mSpells; }
	}


	#endregion

	#region Public Interaction


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


	#endregion
}