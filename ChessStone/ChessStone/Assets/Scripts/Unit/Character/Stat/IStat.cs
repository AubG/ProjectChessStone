using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Inventory System statistic
/// </summary>

[System.Serializable]
public abstract class IStat
{
	/// <summary>
	/// Customize this enum with statistics appropriate for your own game
	/// </summary>

	public enum Identifier
	{
		Power,
		Constitution,
		Agility,
		Intelligence,
		Damage,
		Crit,
		Armor,
		Health,
		Mana,
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
			case Identifier.Power:			return "Power increases weapon damage";
			case Identifier.Constitution:	return "Constitution increases health";
			case Identifier.Agility:		return "Agility increases armor";
			case Identifier.Intelligence:	return "Intelligence increases mana";
			case Identifier.Damage:			return "Damage adds to the amount of damage done in combat";
			case Identifier.Crit:			return "Crit increases the chance of landing a critical strike";
			case Identifier.Armor:			return "Armor protects from damage";
			case Identifier.Health:			return "Health prolongs life";
			case Identifier.Mana:			return "Mana increases the number of spells that can be cast";
		}
		return null;
	}
}