using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Inventory System statistic
/// </summary>

[System.Serializable]
public class InvStat : IStat
{
	public Identifier id;
	public Modifier modifier;
	public int amount;

	/// <summary>
	/// Comparison function for sorting armor. Armor value will show up first, followed by damage.
	/// </summary>

	static public int CompareArmor (InvStat a, InvStat b)
	{
		int ia = (int)a.id;
		int ib = (int)b.id;
		
		if		(a.id == Identifier.Armor)	ia -= 10000;
		else if (a.id == Identifier.Damage) ia -= 5000;
		if		(b.id == Identifier.Armor)	ib -= 10000;
		else if (b.id == Identifier.Damage) ib -= 5000;
		
		if (a.amount < 0) ia += 1000;
		if (b.amount < 0) ib += 1000;
		
		if (a.modifier == Modifier.Percent) ia += 100;
		if (b.modifier == Modifier.Percent) ib += 100;
		
		// Not using ia.CompareTo(ib) here because Flash export doesn't understand that
		if (ia < ib) return -1;
		if (ia > ib) return 1;
		return 0;
	}

	/// <summary>
	/// Comparison function for sorting weapons. Damage value will show up first, followed by armor.
	/// </summary>

	static public int CompareWeapon (InvStat a, InvStat b)
	{
		int ia = (int)a.id;
		int ib = (int)b.id;

		if		(a.id == Identifier.Damage) ia -= 10000;
		else if (a.id == Identifier.Armor)  ia -= 5000;
		if		(b.id == Identifier.Damage) ib -= 10000;
		else if (b.id == Identifier.Armor)  ib -= 5000;

		if (a.amount < 0) ia += 1000;
		if (b.amount < 0) ib += 1000;
		
		if (a.modifier == Modifier.Percent) ia += 100;
		if (b.modifier == Modifier.Percent) ib += 100;
		
		if (ia < ib) return -1;
		if (ia > ib) return 1;
		return 0;
	}
}