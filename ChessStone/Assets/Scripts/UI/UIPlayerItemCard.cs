using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIPlayerItemCard : MonoBehaviour {

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
	private Button sellButton;

	
	#endregion

	#region Game Data


	public ItemTransactionCallback focusCallback { get; set; }
	public ItemTransactionCallback sellCallback { get; set; }

	public CharacterData focusItem;


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

		sellButton.interactable = false;
	}

	public void UpdateLabels(CharacterData focusItem, int amount) {
		ClearLabels();

		this.focusItem = focusItem;

		CharacterData focusData = focusItem;
		UpdatePortrait(focusData.sprite, focusData.cost);
		UpdateHeader(focusData.name, amount, focusData.description);
		UpdateButtons();
	}

	public void UpdatePortrait(Sprite sprite, int cost) {
		portrait.sprite = sprite;
		costText.text = "" + cost;
	}

	public void UpdateHeader(string name, int amount, string description) {
		nameText.text = name;
		amountText.text = "" + amount;
		descriptionText.text = description;
	}

	public void UpdateButtons() {
		Button cardButton = GetComponent<Button>();

		sellButton.interactable = true;

		cardButton.onClick.RemoveAllListeners();
		cardButton.onClick.AddListener(() => focusCallback(focusItem.id));
		sellButton.onClick.RemoveAllListeners();
		sellButton.onClick.AddListener(() => sellCallback(focusItem.id));
	}

	
	#endregion
}