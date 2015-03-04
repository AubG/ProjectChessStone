using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public delegate void CharacterButtonCallback(int id);

public class UICharacterListCard : MonoBehaviour {

	#region Graphics Data


	[SerializeField]
	private UICharacterCard characterCard;

	// GUI Stuff
	private Button[] characterButtons;

	[SerializeField]
	private CanvasGroup canvasGroup;

	
	#endregion

	#region Game Data


	private int currSetIndex = 0;
	private int numSets = 0;

	private CharacterButtonCallback _characterCallback;
	public void AddCharacterButtonCallback(CharacterButtonCallback callback) {
		_characterCallback += callback;
	}


	#endregion

	#region Init


	public void Begin() {
		characterButtons = GetComponentsInChildren<Button>();

		ClearLabels();

		StartCoroutine(WaitUntilLoaded());
	}

	private IEnumerator WaitUntilLoaded() {
		while(PlayerData.Instance == null || !PlayerData.Instance.loaded) {
			yield return null;
		}
		UpdateLabels();
	}


	#endregion

	#region Interaction


	public void PreviousSet() {
		currSetIndex--; if(currSetIndex < 0) currSetIndex = numSets - 1;
		
		UpdateLabels();
	}
	
	public void NextSet() {
		currSetIndex = (currSetIndex + 1) % numSets;
		
		UpdateLabels();
	}

	public void Hide() {
		canvasGroup.alpha = 0;
		canvasGroup.interactable = false;
	}

	public void Show() {
		canvasGroup.alpha = 1;
		canvasGroup.interactable = true;
	}

	public void ClearLabels() {
		for(int i = 0, il = characterButtons.Length; i < il; i++) {
			characterButtons[i].GetComponent<CanvasGroup>().alpha = 0;
			characterButtons[i].interactable = false;
		}
	}

	/// <summary>
	/// Assumes that the characters array is the same length as the selectButtons arrays.
	/// </summary>
	public void UpdateLabels() {
		ClearLabels();

		CharacterRecord[] records = PlayerData.Instance.characterList.TakeSet(currSetIndex, 4);

		int counter = 0;
		for(int i = 0; i < records.Length; i++) {
			if(counter < characterButtons.Length) {
				CharacterRecord record = records[i];
				CharacterData characterData = CharacterBuilder.Instance.GetCharacterData(record.id);
				Button button = characterButtons[counter];
				button.GetComponent<CanvasGroup>().alpha = 1;
				button.interactable = true;
				button.image.sprite = characterData.sprite;
				Text buttonName = button.GetComponentInChildren<Text>();
				buttonName.text = characterData.name;

				button.onClick.RemoveAllListeners();
				if(characterCard != null) button.onClick.AddListener(() => characterCard.UpdateLabels(record.id));

				button.onClick.AddListener(() => _characterCallback(record.id));
			}

			counter++;
		}
	}
	
	
	#endregion
}