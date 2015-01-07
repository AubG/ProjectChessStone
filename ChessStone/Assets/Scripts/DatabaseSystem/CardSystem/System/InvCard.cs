using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Inventory System statistic
/// </summary>

[System.Serializable]
public class InvCard
{
	/// <summary>
	/// Customize this enum with statistics appropriate for your own game
	/// </summary>

	public enum Identifier
	{
		iD,
		Attack,
		Health,
		ManaCost,
		Other,
	}

	/// <summary>
	/// Formula for final stats: [sum of raw amounts] * (1 + [sum of percent amounts])
	/// </summary>

	public enum Modifier
	{
		Added,
		Percent,
	}

	public Identifier id;
	public Modifier modifier;
	public int amount;

	/// <summary>
	/// Get the localized name of the stat.
	/// </summary>

	static public string GetName (Identifier i)
	{
		return i.ToString();
	}

	/// <summary>
	/// Get the localized stat's description -- adjust this to fit your own stats.
	/// </summary>

	static public string GetDescription (Identifier i)
	{
		switch (i)
		{
			case Identifier.iD:				return "Add an identification number to the Card";
			case Identifier.Attack:			return "Attack Value of Card";
			case Identifier.Health:			return "Health of Card";
			case Identifier.ManaCost:		return "Mana Cost of Card";
		}
		return null;
	}

	/// <summary>
	/// Comparison function for sorting armor. Armor value will show up first, followed by damage.
	/// </summary>
	
}