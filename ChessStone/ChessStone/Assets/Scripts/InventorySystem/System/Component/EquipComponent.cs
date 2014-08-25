using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

/// <summary>
/// The possible equipment slots.
/// </summary>

public enum EquipSlot
{
	None,			// First element MUST be 'None'
	Weapon,			// All the following elements are yours to customize -- edit, add or remove as you desire
	Shield,
	Body,
	Shoulders,
	Bracers,
	Boots,
	_LastDoNotUse,	// Flash export doesn't support Enum.GetNames :(
}

[System.Serializable]
public class EquipComponent : InvComponent
{
	/// <summary>
	/// Slot that this item belongs to.
	/// </summary>

	public EquipSlot slot = EquipSlot.None;

	/// <summary>
	/// And and all base stats this item may have at a maximum level (50).
	/// Actual object's stats are calculated based on item's level and quality.
	/// </summary>

	public List<InvStat> stats = new List<InvStat>();

	/// <summary>
	/// Game Object that will be created and attached to the specified slot on the body.
	/// This should typically be a prefab with a renderer component, such as a sword,
	/// a bracer, shield, etc.
	/// </summary>

	public GameObject attachment;
}