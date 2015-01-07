using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// The deck container.
/// </summary>

public class UIHand : MonoBehaviour
{
	/// <summary>
	/// Maximum size of the container. Adding more cards than this number will not work.
	/// </summary>

	public int maxCardCount = 8;

	/// <summary>
	/// Template used to create inventory icons.
	/// </summary>

	public GameObject template;

	/// <summary>
	/// Background widget to scale after the card slots have been created.
	/// </summary>

	public UIWidget background;

	/// <summary>
	/// Spacing between icons.
	/// </summary>

	public int spacing = 128;

	/// <summary>
	/// Padding around the border.
	/// </summary>

	public int padding = 10;

	List<InvGameCard> mCards = new List<InvGameCard>();

	/// <summary>
	/// List of cards in the container.
	/// </summary>

	public List<InvGameCard> cards { get { while (mCards.Count < maxCardCount) mCards.Add(null); return mCards; } }

	/// <summary>
	/// Convenience function that returns an card at the specified slot.
	/// </summary>

	public InvGameCard GetCard (int slot) { return (slot < cards.Count) ? mCards[slot] : null; }

	/// <summary>
	/// Replace an card in the container with the specified one.
	/// </summary>
	/// <returns>An card that was replaced.</returns>

	public InvGameCard Replace (int slot, InvGameCard card)
	{
		if (slot < maxCardCount)
		{
			InvGameCard prev = cards[slot];
			mCards[slot] = card;
			return prev;
		}
		return card;
	}

	/// <summary>
	/// Initialize the container and create an appropriate number of UI slots.
	/// </summary>

	void Start ()
	{
		if (template != null)
		{
			int count = 0;
			Bounds b = new Bounds();

			for (int x = 0; x < maxCardCount; ++x)
			{
				GameObject go = NGUITools.AddChild(gameObject, template);
				Transform t = go.transform;
				t.localPosition = new Vector3(padding + (x + 0.5f) * spacing, -padding - 0.5f * spacing, 0f);

				UIHandSlot slot = go.GetComponent<UIHandSlot>();
				
				if (slot != null)
				{
					slot.hand = this;
					slot.slot = count;
				}

				b.Encapsulate(new Vector3(padding * 2f + (x + 1) * spacing, -padding * 2f - 1 * spacing, 0f));

				if (++count >= maxCardCount)
				{
					if (background != null)
					{
						background.transform.localScale = b.size;
					}
					return;
				}
			}

			if (background != null) background.transform.localScale = b.size;
		}
	}
}