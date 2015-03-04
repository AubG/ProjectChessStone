using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;

public class MerchantController : MonoBehaviour
{
	#region State Data


	public enum State {
		None,
		Buy,
		Sell,
		End
	}
	
	public State currState { get; protected set; }


	#endregion

	#region Graphics Data


	[SerializeField]
	private UIMerchantCard merchantCard;

	[SerializeField]
	private UICharacterCard characterCard;


	#endregion

	#region Game Data


	private int currPlayerSetIndex = 0;
	private int currMerchantSetIndex = 0;
	private int numMerchantSets = 0;
	private int numPlayerSets = 0;

	private MerchantData merchantData;


	#endregion

	#region Initialization


	void Awake() {
		merchantCard.AddFocusCallback(characterCard.UpdateLabels);
		merchantCard.AddSellCallback(OnSell);
		merchantCard.AddPurchaseCallback(OnPurchase);
		merchantCard.Begin();
	}

	public void Begin(MerchantData merchant) {
		merchantData = merchant;

		currState = State.Buy;
		numPlayerSets = PlayerData.Instance.characterList.GetNumSets(3);
		numMerchantSets = Mathf.CeilToInt(merchantData.numItems / 3f);

		currPlayerSetIndex = 0;
		currMerchantSetIndex = 0;

		UpdateMerchantCard();

		merchantCard.Show(true);
		characterCard.Show(true);
	}

	public void UpdateMerchantCard() {
		merchantCard.UpdateHeader(merchantData.name);
		merchantCard.UpdateMerchantItemCards(merchantData.TakeSet(currMerchantSetIndex, 3));
		merchantCard.UpdatePlayerItemCards(PlayerData.Instance.characterList.TakeSet(currPlayerSetIndex, 3));
	}

	public void End() {
		currState = State.End;
		merchantCard.Show(false);
		characterCard.Show(false);
	}


	#endregion

	#region Event Handlers


	public void OnClose() {
		End ();
	}

	public void OnBuyMode() {
		currState = State.Buy;
		merchantCard.ShowMerchantItems(true);
		merchantCard.ShowPlayerItems(false);
	}

	public void OnSellMode() {
		currState = State.Sell;
		merchantCard.ShowMerchantItems(false);
		merchantCard.ShowPlayerItems(true);
	}

	public void OnUp() {
		switch(currState) {
		case State.Buy:
			currMerchantSetIndex--; if(currMerchantSetIndex < 0) currMerchantSetIndex = numMerchantSets - 1;
			merchantCard.UpdateMerchantItemCards(merchantData.TakeSet(currMerchantSetIndex, 3));
			break;
		case State.Sell:
			currPlayerSetIndex--; if(currPlayerSetIndex < 0) currPlayerSetIndex = numPlayerSets - 1;
			merchantCard.UpdatePlayerItemCards(PlayerData.Instance.characterList.TakeSet(currPlayerSetIndex, 3));
			break;
		}
	}
	
	public void OnDown() {
		switch(currState) {
		case State.Buy:
			currMerchantSetIndex = (currMerchantSetIndex + 1) % numMerchantSets;
			merchantCard.UpdateMerchantItemCards(merchantData.TakeSet(currMerchantSetIndex, 3));
			break;
		case State.Sell:
			currPlayerSetIndex = (currPlayerSetIndex + 1) % numPlayerSets;
			merchantCard.UpdatePlayerItemCards(PlayerData.Instance.characterList.TakeSet(currPlayerSetIndex, 3));
			break;
		}
	}
	
	private void OnPurchase(int characterId) {
		CharacterData characterData = CharacterBuilder.Instance.GetCharacterData(characterId);
		int cost = characterData.cost;
		PlayerData.Instance.AddCoins(-cost);
		PlayerData.Instance.AddCharacter(characterId);
		numPlayerSets = PlayerData.Instance.characterList.GetNumSets(3);
		merchantCard.UpdateMerchantItemCards(merchantData.TakeSet(currMerchantSetIndex, 3));
		merchantCard.UpdatePlayerItemCards(PlayerData.Instance.characterList.TakeSet(currPlayerSetIndex, 3));
	}

	private void OnSell(int characterId) {
		CharacterData characterData = CharacterBuilder.Instance.GetCharacterData(characterId);
		int cost = characterData.cost;
		PlayerData.Instance.AddCoins(cost);
		PlayerData.Instance.DepleteCharacter(characterId);
		numPlayerSets = PlayerData.Instance.characterList.GetNumSets(3);
		merchantCard.UpdateMerchantItemCards(merchantData.TakeSet(currMerchantSetIndex, 3));
		merchantCard.UpdatePlayerItemCards(PlayerData.Instance.characterList.TakeSet(currPlayerSetIndex, 3));
	}


	#endregion

	#region Helpers

	
	#endregion
}