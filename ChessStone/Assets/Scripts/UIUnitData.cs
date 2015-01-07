using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

using X_UniTMX;

public delegate void SpellButtonVoidCallback();
public delegate void SpellButtonCallback(int spellId);

public class UIUnitData : MonoBehaviour {

	#region Graphics Data


	[SerializeField]
	private Image portrait;
	
	[SerializeField]
	private Text nameText;
	
	[SerializeField]
	private Text descriptionText;
	
	[SerializeField]
	private Text moveText;

	[SerializeField]
	private Text sizeText;

	[SerializeField]
	private CanvasGroup statsCanvasGroup;

	[SerializeField]
	private CanvasGroup spellCanvasGroup;

	[SerializeField]
	private Text spellNameText;

	[SerializeField]
	private Text spellDescriptionText;
	
	[SerializeField]
	private Button[] spellButtons;

	[SerializeField]
	private Sprite noActionIcon;

	
	#endregion

	#region Game Data


	private List<SpellButtonCallback> _callbacks = new List<SpellButtonCallback>();
	public void AddSpellButtonCallback(SpellButtonCallback callback) {
		_callbacks.Add(callback);
	}

	public SpellButtonVoidCallback noActionCallback { get; set; }


	#endregion

	#region Initialization
	
	
	void Start() {
		spellButtons = GetComponentsInChildren<Button>();

		ClearLabels();
	}
	

	#endregion
	
	#region Interaction


	public void ClearLabels() {
		statsCanvasGroup.alpha = 0;
		spellCanvasGroup.alpha = 0;

		for(int i = 0, il = spellButtons.Length; i < il; i++) {
			spellButtons[i].GetComponent<CanvasGroup>().alpha = 0;
			spellButtons[i].interactable = false;
		}

		portrait.sprite = null;
		nameText.text = "";
		descriptionText.text = "";
		spellNameText.text = "";
		spellDescriptionText.text = "";
		moveText.text = "";
		sizeText.text = "";
	}

	public void UpdateLabels(GameUnit focusUnit, bool showExtras) {
		ClearLabels();

		statsCanvasGroup.alpha = 1;

		UpdatePortrait(focusUnit.GetComponent<SpriteRenderer>().sprite);
		UpdateHeader(focusUnit.gameName, focusUnit.gameDescription);
		
		if(focusUnit is GameCharacter) {
			GameCharacter focusCharacter = focusUnit as GameCharacter;
			UpdateStats(focusCharacter.move.adjustedMoveRange, focusCharacter.maxSize);
			UpdateSpells(focusCharacter.spellBox.spells);
			if(showExtras) UpdateExtras();
		}
	}

	public void UpdateLabels(int characterId) {
		ClearLabels();

		statsCanvasGroup.alpha = 1;

		CharacterData data = CharacterManager.Instance.GetCharacterData(characterId);
	
		UpdatePortrait(data.sprite);
		UpdateHeader(data.name, data.description);
		UpdateStats(data.moveRange, data.maxSize);

		SpellData[] spells = {
			SpellManager.Instance.GetSpellData(data.spellIdSlotA),
			SpellManager.Instance.GetSpellData(data.spellIdSlotB),
			SpellManager.Instance.GetSpellData(data.spellIdSlotC),
			SpellManager.Instance.GetSpellData(data.spellIdSlotD)
		};
		UpdateSpells(spells);
	}

	public void UpdatePortrait(Sprite sprite) {
		portrait.sprite = sprite;
	}

	public void UpdateHeader(string name, string description) {
		nameText.text = name;
		descriptionText.text = description;
	}

	public void UpdateStats(int moveRange, int maxSize) {
		moveText.text = "Move Range: " + moveRange;
		sizeText.text = "Max Size: " + maxSize;
	}

	public void UpdateSpells(SpellData[] spells) {
		spellCanvasGroup.alpha = 1;

		int i, j, il, jl;
		SpellData spell;
		Button spellButton;
		Text spellText;

		for(i = 0, il = spells.Length; i < il; i++) {
			spell = spells[i];
			if(spell == null) break;
			spellButton = spellButtons[i];
			spellButton.GetComponent<CanvasGroup>().alpha = 1;
			spellButton.interactable = true;
			spellButton.image.sprite = spell.icon;

			spellButton.onClick.RemoveAllListeners();
			int id = spell.id;
			spellButton.onClick.AddListener(() => UpdateSpellInfo(id));
			
			for(j = 0, jl = _callbacks.Count; j < jl; j++) {
				SpellButtonCallback callback = _callbacks[j];
				spellButton.onClick.AddListener(() => callback(id));
			}
		}
	}

	public void UpdateExtras() {
		// add the end button
		Button spellButton = spellButtons[spellButtons.Length - 1];
		spellButton.GetComponent<CanvasGroup>().alpha = 1;
		spellButton.interactable = true;
		spellButton.image.sprite = noActionIcon;

		spellButton.onClick.RemoveAllListeners();

		spellButton.onClick.AddListener(() => noActionCallback());
	}

	public void UpdateSpellInfo(int spellId) {
		SpellData spell = SpellManager.Instance.GetSpellData(spellId);
		spellNameText.text = spell.name;
		spellDescriptionText.text = spell.description;
	}
	
	
	#endregion
}