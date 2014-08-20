using UnityEngine;
using System.Collections.Generic;
using X_UniTMX;

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

	#region Game Data


	private GameCharacter caster;


	#endregion

	#region Init


	void Start() {
		if(!caster) caster = this.GetComponent<GameCharacter>();
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

	public void CastSpell(string name, Tile target = null) {
		GameSpell theSpell = GetSpell(name);
		if(theSpell != null) theSpell.Cast(caster, target);
	}


	#endregion
}