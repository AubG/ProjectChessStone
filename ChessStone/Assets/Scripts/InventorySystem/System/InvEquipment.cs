using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Inventory system -- Equipment class works with InvAttachmentPoints and allows to visually equip and remove items.
/// </summary>

[AddComponentMenu("NGUI/Examples/Equipment")]
public class InvEquipment : MonoBehaviour
{
	InvGameItem[] mItems;
	InvAttachmentPoint[] mAttachments;

	/// <summary>
	/// List of equipped items (with a finite number of equipment slots).
	/// </summary>

	public InvGameItem[] equippedItems { get { return mItems; } }

	/// <summary>
	/// Equip the specified item automatically replacing an existing one.
	/// </summary>

	public InvGameItem Replace (EquipSlot slot, InvGameItem item)
	{
		InvBaseItem baseItem = (item != null) ? item.baseItem : null;
		EquipComponent baseItemEquip = (baseItem != null) ? baseItem.equip : null;
		if(baseItem != null && baseItemEquip == null) return item;

		if (slot != EquipSlot.None)
		{
			// If the item is not of appropriate type, we shouldn't do anything
			if (baseItem != null && baseItemEquip.slot != slot) return item;

			if (mItems == null)
			{
				// Automatically figure out how many item slots we need
				int count = (int)EquipSlot._LastDoNotUse;
				mItems = new InvGameItem[count];
			}

			// Equip this item
			InvGameItem prev = mItems[(int)slot - 1];
			mItems[(int)slot - 1] = item;
			/*
			if(prev != null) StatManager.Instance.HandleItemRemove(prev.baseItemID);
			if(item != null) StatManager.Instance.HandleItemAdd(item.baseItemID, item.CalculateStats());
			*/
			// Get the list of all attachment points
			if (mAttachments == null) mAttachments = GetComponentsInChildren<InvAttachmentPoint>();

			// Equip the item visually
			for (int i = 0, imax = mAttachments.Length; i < imax; ++i)
			{
				InvAttachmentPoint ip = mAttachments[i];

				if (ip.slot == slot)
				{
					GameObject go = ip.Attach(baseItemEquip != null ? baseItemEquip.attachment : null);
				}
			}
			return prev;
		}
		else if (item != null)
		{
			Debug.LogWarning("Can't equip \"" + item.name + "\" because it doesn't specify an item slot");
		}
		return item;
	}

	/// <summary>
	/// Equip the specified item and return the item that was replaced.
	/// </summary>

	public InvGameItem Equip (InvGameItem item)
	{
		if (item != null)
		{
			InvBaseItem baseItem = item.baseItem;
			EquipComponent equip = baseItem.equip;
			if (baseItem != null) return Replace(equip.slot, item);
			else Debug.LogWarning("Can't resolve the item ID of " + item.baseItemID);
		}
		return item;
	}

	/// <summary>
	/// Unequip the specified item, returning it if the operation was successful.
	/// </summary>

	public InvGameItem Unequip (InvGameItem item)
	{
		if (item != null)
		{
			InvBaseItem baseItem = item.baseItem;
			if (baseItem != null) return Replace(baseItem.equip.slot, null);
		}
		return item;
	}

	/// <summary>
	/// Unequip the item from the specified slot, returning the item that was unequipped.
	/// </summary>

	public InvGameItem Unequip (EquipSlot slot) { return Replace(slot, null); }

	/// <summary>
	/// Whether the specified item is currently equipped.
	/// </summary>

	public bool HasEquipped (InvGameItem item)
	{
		if (mItems != null)
		{
			for (int i = 0, imax = mItems.Length; i < imax; ++i)
			{
				if (mItems[i] == item) return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Whether the specified slot currently has an item equipped.
	/// </summary>

	public bool HasEquipped (EquipSlot slot)
	{
		if (mItems != null)
		{
			for (int i = 0, imax = mItems.Length; i < imax; ++i)
			{
				InvBaseItem baseItem = mItems[i].baseItem;
				if (baseItem != null && baseItem.equip.slot == slot) return true;
			}
		}
		return false;
	}

	/// <summary>
	/// Retrieves the item in the specified slot.
	/// </summary>

	public InvGameItem GetItem (EquipSlot slot)
	{
		if (slot != EquipSlot.None)
		{
			int index = (int)slot - 1;

			if (mItems != null && index < mItems.Length)
			{
				return mItems[index];
			}
		}
		return null;
	}
}