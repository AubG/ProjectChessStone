using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public delegate void CharacterButtonCallback(int id);

public class UICharacterList : MonoBehaviour {

	#region Graphics Data


	[SerializeField]
	private UIUnitData unitData;

	// GUI Stuff
	[SerializeField]
	private Button[] characterButtons;

	[SerializeField]
	private CanvasGroup canvasGroup;

	
	#endregion

	#region Game Data

	
	private CharacterList characterList;

	private int currSetIndex = 0;
	private int numSets = 0;

	private List<CharacterButtonCallback> _callbacks = new List<CharacterButtonCallback>();
	public void AddCharacterButtonCallback(CharacterButtonCallback callback) {
		_callbacks.Add(callback);
		UpdateSet();
	}


	#endregion

	#region Init


	public void Start() {
		characterButtons = GetComponentsInChildren<Button>();

		ClearLabels();

		StartCoroutine(WaitUntilLoaded());
	}

	private IEnumerator WaitUntilLoaded() {
		PlayerData data = PlayerData.Instance;
		while(!data.loaded) {
			yield return null;
		}
		characterList = data.characterList;

		UpdateSet();
	}


	#endregion

	#region Interaction


	public void PreviousSet() {
		currSetIndex--; if(currSetIndex < 0) currSetIndex = numSets - 1;
		
		UpdateSet();
	}
	
	public void NextSet() {
		currSetIndex = (currSetIndex + 1) % numSets;
		
		UpdateSet();
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
	public void UpdateLabels(int[] characterIds) {
		ClearLabels();
		int j;
		for(int i = 0; i < characterButtons.Length; i++) {
			if(i < characterIds.Length) {
				int id = characterIds[i];

				//Debug.Log (CharacterManager.Instance);
				CharacterData character = CharacterManager.Instance.GetCharacterData(id);
				Button button = characterButtons[i];
				button.GetComponent<CanvasGroup>().alpha = 1;
				button.interactable = true;
				button.image.sprite = character.sprite;
				Text buttonName = button.GetComponentInChildren<Text>();
				buttonName.text = character.name;

				button.onClick.RemoveAllListeners();
				button.onClick.AddListener(() => unitData.UpdateLabels(id));
				
				for(j = 0; j < _callbacks.Count; j++) {
					CharacterButtonCallback callback = _callbacks[j];
					button.onClick.AddListener(() => callback(id));
				}
			}
		}
	}
	
	
	#endregion

	#region Helpers
	
	
	private void UpdateSet() {
		int[] idSet = characterList.TakeSet(currSetIndex, 4);
		UpdateLabels(idSet);
	}


	#endregion
}