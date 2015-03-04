using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using SimpleJSON;
using SQLite4Unity3d;

[System.Flags]
public enum ValidTargets {
	None = 0,
	Tile = 1,
	Self = 1 << 1,
	Enemy = 1 << 2,
	Ally = 1 << 3
}

public class SpellData
{
	#region Data


	[PrimaryKey]
	public int id { get; set; }
	
	public string name { get; set; }
	
	public string description { get; set; }

	public string iconPath { get; set; }

	public int targetRange { get; set; }

	public string validTargetsMask { get; set; }

	public string spellEffectsPath { get; set; }


	#endregion

	#region Cached Data


	private Sprite _icon;
	public Sprite icon {
		get {
			if(_icon == null) LoadSprite(iconPath);

			return _icon;
		}
	}
	
	private ValidTargets _validTargets;
	public ValidTargets validTargets { 
		get {
			if(_validTargets == ValidTargets.None) {
				int temp = System.Convert.ToInt32(validTargetsMask, 2);
				_validTargets = (ValidTargets)temp;
			}

			return _validTargets;
		}
	}

	private List<SpellEffect> _spellEffects = new List<SpellEffect>();
	public List<SpellEffect> spellEffects {
		get {
			if(_spellEffects.Count == 0) LoadSpellEffects(spellEffectsPath);
			return _spellEffects;
		}
	}

	
	#endregion

	#region Init


	public void LoadSprite(string resourcePath) {
		Sprite resourceIcon = Resources.Load<Sprite>(resourcePath);
		Resources.UnloadUnusedAssets();
		if (resourceIcon != null) {
			_icon = resourceIcon;
		} else {
			Debug.LogError("Spell icon doesn't exist at: Resources/" + resourcePath);
		}
	}

	public void LoadSpellEffects(string resourcePath) {
		TextAsset resourceText = Resources.Load<TextAsset>(resourcePath);
		Resources.UnloadUnusedAssets();
		if (resourceText != null) {
			JSONClass N = JSON.Parse(resourceText.text).AsObject;
			JSONArray effectsArray = N["effects"].AsArray;

			SpellEffect newEffect;

			for(int i = 0; i < effectsArray.Count; i++) {
				JSONClass temp = effectsArray[i].AsObject;

				Type effectType = SpellBuilder.Instance.GetSpellEffectType(temp["name"]);
				newEffect = (SpellEffect)Activator.CreateInstance(effectType);
				newEffect.Populate(temp);
				_spellEffects.Add(newEffect);
			}
		} else {
			Debug.LogError("Spell effect data doesn't exist at: Resources/" + resourcePath);
		}
	}


	#endregion

	#region Helpers


	#endregion
}