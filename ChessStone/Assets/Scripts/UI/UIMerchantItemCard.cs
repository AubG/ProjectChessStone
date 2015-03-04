using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIMerchantItemCard : MonoBehaviour {

	#region Graphics Data


	[SerializeField]
	private Image portrait;

	[SerializeField]
	private Text costText;
	
	[SerializeField]
	private Text nameText;

	[SerializeField]
	private Text amountText;

	[SerializeField]
	private Text descriptionText;

	[SerializeField]
	private Button purchaseButton;

	
	#endregion

	#region Game Data


	public ItemTransactionCallback focusCallback { get; set; }
	public ItemTransactionCallback purchaseCallback { get; set; }

	public MerchantItemData focusItem;


	#endregion

	#region Initialization
	
	
	void Start() {
		ClearLabels();
	}
	

	#endregion
	
	#region Interaction


	public void ClearLabels() {
		portrait.sprite = null;
		nameText.text = "";
		amountText.text = "";
		descriptionText.text = "";
		costText.text = "";

		purchaseButton.interactable = false;
	}

	public void UpdateLabels(MerchantItemData focusData, int ownedCount) {
		ClearLabels();

		focusItem = focusData;

		CharacterData characterData = CharacterBuilder.Instance.GetCharacterData(focusData.characterId);
		int coins = PlayerData.Instance.coins;
		bool canPurchase = coins >= characterData.cost;

		UpdatePortrait(characterData.sprite, characterData.cost);
		UpdateHeader(characterData.name, ownedCount, characterData.description);
		UpdateButtons(canPurchase);
	}

	public void UpdatePortrait(Sprite sprite, int cost) {
		portrait.sprite = sprite;
		costText.text = "" + cost;
	}

	public void UpdateHeader(string name, int ownedCount, string description) {
		nameText.text = name;
		amountText.text = "" + ownedCount;
		descriptionText.text = description;
	}

	public void UpdateButtons(bool canPurchase) {
		Button cardButton = GetComponent<Button>();

		purchaseButton.interactable = canPurchase;

		cardButton.onClick.RemoveAllListeners();
		cardButton.onClick.AddListener(() => focusCallback(focusItem.characterId));
		purchaseButton.onClick.RemoveAllListeners();
		purchaseButton.onClick.AddListener(() => purchaseCallback(focusItem.characterId));
	}

	
	#endregion
}