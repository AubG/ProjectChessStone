using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public delegate void ItemTransactionCallback(int characterId);

public class UIMerchantCard : MonoBehaviour {

	#region Graphics Data


	[SerializeField]
	private Text titleText;

	[SerializeField]
	private CanvasGroup canvasGroup;

	[SerializeField]
	private CanvasGroup playerCanvasGroup;

	[SerializeField]
	private CanvasGroup merchantCanvasGroup;

	[SerializeField]
	private UICharacterCard characterCard;
	
	private UIPlayerItemCard[] playerItemCards;
	private UIMerchantItemCard[] merchantItemCards;

	
	#endregion

	#region Game Data
	

	private ItemTransactionCallback _focusCallback;
	public void AddFocusCallback(ItemTransactionCallback callback) {
		_focusCallback += callback;
	}

	private ItemTransactionCallback _sellCallback;
	public void AddSellCallback(ItemTransactionCallback callback) {
		_sellCallback += callback;
	}

	private ItemTransactionCallback _purchaseCallback;
	public void AddPurchaseCallback(ItemTransactionCallback callback) {
		_purchaseCallback += callback;
	}


	#endregion

	#region Init


	public void Begin() {
		playerItemCards = playerCanvasGroup.GetComponentsInChildren<UIPlayerItemCard>();
		merchantItemCards = merchantCanvasGroup.GetComponentsInChildren<UIMerchantItemCard>();

		ClearAll();
	}


	#endregion

	#region Interaction
	

	public void Show(bool flag) {
		canvasGroup.alpha = flag ? 1 : 0;
		canvasGroup.interactable = flag;

		ShowMerchantItems(true);
		ShowPlayerItems(false);
	}

	public void ShowMerchantItems(bool flag) {
		merchantCanvasGroup.alpha = flag ? 1 : 0;
		merchantCanvasGroup.blocksRaycasts = flag;
		merchantCanvasGroup.interactable = flag;
	}

	public void ShowPlayerItems(bool flag) {
		playerCanvasGroup.alpha = flag ? 1 : 0;
		playerCanvasGroup.blocksRaycasts = flag;
		playerCanvasGroup.interactable = flag;
	}

	public void ClearAll() {
		ClearHeader();
		ClearPlayerItemCards();
		ClearMerchantItemCards();
	}

	public void ClearHeader() {
		titleText.text = "";
	}

	public void ClearPlayerItemCards() {
		CanvasGroup playerItemCardGroup = null;
		for(int i = 0, il = playerItemCards.Length; i < il; i++) {
			playerItemCardGroup = playerItemCards[i].GetComponent<CanvasGroup>();
			playerItemCardGroup.alpha = 0;
			playerItemCardGroup.interactable = false;
		}
	}

	public void ClearMerchantItemCards() {
		CanvasGroup merchantItemCardGroup = null;
		for(int i = 0, il = merchantItemCards.Length; i < il; i++) {
			merchantItemCardGroup = merchantItemCards[i].GetComponent<CanvasGroup>();
			merchantItemCardGroup.alpha = 0;
			merchantItemCardGroup.interactable = false;
		}
	}

	public void UpdateHeader(string title) {
		titleText.text = title;
	}

	/// <summary>
	/// Assumes that the characters array is the same length as the selectButtons arrays.
	/// </summary>
	public void UpdatePlayerItemCards(CharacterRecord[] characterRecordSet) {
		ClearPlayerItemCards();

		int counter = 0;
		for(int i = 0; i < characterRecordSet.Length; i++) {
			if(counter < merchantItemCards.Length) {
				CharacterRecord record = characterRecordSet[i];
				CharacterData characterData = CharacterBuilder.Instance.GetCharacterData(record.id);
				UIPlayerItemCard card = playerItemCards[counter];
				
				CanvasGroup cardGroup = card.GetComponent<CanvasGroup>();
				cardGroup.alpha = 1;
				cardGroup.interactable = true;

				card.focusCallback = _focusCallback;
				card.sellCallback = _sellCallback;
				card.UpdateLabels(characterData, record.count);
			}
			
			counter++;
		}
	}

	/// <summary>
	/// Assumes that the merchant items array is the same length as the merchant item card array
	/// </summary>
	public void UpdateMerchantItemCards(IEnumerable<MerchantItemData> items) {
		ClearMerchantItemCards();

		int counter = 0;
		foreach(MerchantItemData item in items) {
			if(counter < merchantItemCards.Length) {
				int ownedCount = PlayerData.Instance.characterList.GetCharacterCount(item.characterId);
				UIMerchantItemCard card = merchantItemCards[counter];

				CanvasGroup cardGroup = card.GetComponent<CanvasGroup>();
				cardGroup.alpha = 1;
				cardGroup.interactable = true;

				card.focusCallback = _focusCallback;
				card.purchaseCallback = _purchaseCallback;
				card.UpdateLabels(item, ownedCount);
			}

			counter++;
		}
	}
	
	
	#endregion
}