using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// User interface card slot.
/// </summary>

public abstract class UICardSlot : MonoBehaviour
{
	public UISprite icon;
	public UIWidget background;
	public UILabel label;

	public AudioClip grabSound;
	public AudioClip placeSound;
	public AudioClip errorSound;

	InvGameCard mCard;
	string mText = "";

	/// <summary>
	/// This function should return the card observed by this UI class.
	/// </summary>

	abstract protected InvGameCard observedCard { get; }

	/// <summary>
	/// Replace the observed card with the specified value. Should return the card that was replaced.
	/// </summary>

	abstract protected InvGameCard Replace (InvGameCard card);

	/// <summary>
	/// Show a tooltip for the card.
	/// </summary>

	void OnHover (bool show)
	{
		InvGameCard card = show ? mCard : null;

		if (card != null)
		{
			InvBaseCard bi = card.baseCard;

			if (bi != null)
			{
				string t = "[" + NGUITools.EncodeColor(bi.color) + "]" + card.name + "[-]\n";

				if (!string.IsNullOrEmpty(bi.description)) t += "\n[FF9900]" + bi.description;
				UITooltip.ShowText(t);
				return;
			}
		}
		UITooltip.ShowText(null);
	}

	/// <summary>
	/// Keep an eye on the card and update the icon when it changes.
	/// </summary>

	void Update ()
	{
		InvGameCard i = observedCard;

		if (mCard != i)
		{
			mCard = i;

			InvBaseCard baseCard = (i != null) ? i.baseCard : null;

			if (label != null)
			{
				string cardName = (i != null) ? i.name : null;
				if (string.IsNullOrEmpty(mText)) mText = label.text;
				label.text = (cardName != null) ? cardName : mText;
			}
			
			if (icon != null)
			{
				if (baseCard == null || baseCard.iconAtlas == null)
				{
					icon.enabled = false;
				}
				else
				{
					icon.atlas = baseCard.iconAtlas;
					icon.spriteName = baseCard.iconName;
					icon.enabled = true;
					icon.MakePixelPerfect();
				}
			}

			if (background != null)
			{
				background.color = (i != null) ? baseCard.color : Color.white;
			}
		}
	}
}