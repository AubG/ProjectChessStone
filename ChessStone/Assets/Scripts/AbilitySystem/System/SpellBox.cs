using UnityEngine;
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
/// Inventory system -- Equipment class works with InvAttachmentPoints and allows to visually equip and remove items.
/// </summary>

[AddComponentMenu("Spells/Box")]
public class SpellBox : MonoBehaviour
{
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
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


<<<<<<< HEAD
=======
=======
	[SerializeField]
	private GameSpell[] mSpells;

	/// <summary>
	/// Whether the box contains the spell.
	/// </summary>
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
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
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7

	public void CastSpell(string name, Tile target = null) {
		GameSpell theSpell = GetSpell(name);
		if(theSpell != null) theSpell.Cast(caster, target);
	}


	#endregion
<<<<<<< HEAD
=======
=======
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
}