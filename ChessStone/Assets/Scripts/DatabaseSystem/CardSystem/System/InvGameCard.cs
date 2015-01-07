using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Since it would be incredibly tedious to create thousands of unique items by hand, a simple solution is needed.
/// Separating items into 2 parts is that solution. Base item contains stats that the item would have if it was max
/// level. All base items are created with their stats at max level. Game item, the second item class, has an
/// effective item level which is used to calculate effective item stats. Game items can be generated with a random
/// level (clamped within base item's min/max level range), and with random quality affecting the item's stats.
/// </summary>

[System.Serializable]
public class InvGameCard
{
	// The base Id
	[SerializeField] int mBaseID = 0;

	/// <summary>
	/// The quality/rarity.
	/// </summary>

	public InvBaseCard.Quality quality = InvBaseCard.Quality.Normal;

	// Cached for speed
	InvBaseCard mBaseCard;

	/// <summary>
	/// The base Id property.
	/// </summary>

	public int baseID { get { return mBaseID; } }

	/// <summary>
	/// The base card.
	/// </summary>

	public InvBaseCard baseCard
	{
		get
		{
			if (mBaseCard == null) {
				mBaseCard = InvCardDatabase.FindByID(baseID);
			}

			return mBaseCard;
		}
	}

	/// <summary>
	/// Name should prefix the quality
	/// </summary>

	public string name
	{
		get
		{
			if (baseCard == null) return null;
			return quality.ToString() + " " + baseCard.name;
		}
	}

	/// <summary>
	/// Create a game card with the specified ID.
	/// </summary>

	public InvGameCard (int id) { mBaseID = id; }

	/// <summary>
	/// Create a game card with the specified ID and base card.
	/// </summary>

	public InvGameCard (int id, InvBaseCard bi) { mBaseID = id; mBaseCard = bi; }
}