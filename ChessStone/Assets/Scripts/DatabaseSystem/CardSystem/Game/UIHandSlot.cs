using UnityEngine;

/// <summary>
/// A UI script that keeps an eye on the slot in hand.
/// </summary>

public class UIHandSlot : UICardSlot
{
	public UIHand hand;
	public int slot = 0;

	override protected InvGameCard observedCard
	{
		get
		{
			return (hand != null) ? hand.GetCard(slot) : null;
		}
	}

	/// <summary>
	/// Replace the observed card with the specified value. Should return the card that was replaced.
	/// </summary>

	override protected InvGameCard Replace (InvGameCard card)
	{
		if(hand != null) {
			InvGameCard replaced = hand.Replace(slot, card);
			return replaced;
		} else {
			return card;
		}
	}
}