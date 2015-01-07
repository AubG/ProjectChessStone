using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;

public delegate void CastFinishedDelegate();

[AddComponentMenu("Spells/Box")]
public class SpellBox : MonoBehaviour
{
	#region Data


	[SerializeField]
	private SpellData[] mSpells = new SpellData[4];
	public SpellData[] spells {
		get { return mSpells; }
	}

	public int numSpells {
		get { return mSpells.Length; }
	}


	#endregion

	#region Game Data


	private GameCharacter caster;


	#endregion

	#region Init


	void Start() {
		if(!caster) caster = this.GetComponent<GameCharacter>();
	}


	#endregion

	#region Interaction


	public void SetSpellSlotIndex(SpellData spell, int index) {
		mSpells[index] = spell;
	}

	public SpellData GetSpell (int spellId) {
		return SpellManager.Instance.GetSpellData(spellId);
	}

	public SpellData GetSpellBySlotIndex(int index) {
		if (mSpells != null)
			return mSpells[index];

		return null;
	}

	public bool CanCastOnTarget(SpellData spell, Tile target) {
		ValidTargets validTargets = spell.validTargets;

		if(target.currUnit != null) {
			UnitType type = target.currUnit.unitType;
			switch(type) {
			case UnitType.Character:
				GameCharacter targetCharacter = (GameCharacter)target.currUnit;
				if(targetCharacter != caster) {
					if(targetCharacter.owningPlayer != caster.owningPlayer) {
						if((validTargets & ValidTargets.Enemy) == ValidTargets.Enemy) {
							return true;
						}
					} else if((validTargets & ValidTargets.Ally) == ValidTargets.Ally) {
						return true;
					}
				} else if((validTargets & ValidTargets.Self) == ValidTargets.Self) {
					return true;
				}
				break;
			case UnitType.Link:
				GameUnitLink targetLink = (GameUnitLink)target.currUnit;
				if(targetLink.head.owningPlayer != caster.owningPlayer) {
					if((validTargets & ValidTargets.Enemy) == ValidTargets.Enemy) {
						return true;
					}
				} else if((validTargets & ValidTargets.Ally) == ValidTargets.Ally) {
					return true;
				}
				break;
			}
		} else if((validTargets & ValidTargets.Tile) == ValidTargets.Tile) {
			return true;
		}

		return false;
	}

	public void Cast(SpellData spell, Tile target, CastFinishedDelegate finishedCallback = null) {
		StartCoroutine(ProcessCast(spell, target, finishedCallback));
	}

	public void Cast(int spellId, Tile target, CastFinishedDelegate finishedCallback = null) {
		SpellData spell = GetSpell(spellId);
		Cast(spell, target, finishedCallback);
	}

	public void CastBySlotIndex(int index, Tile target, CastFinishedDelegate finishedCallback = null) {
		SpellData spell = GetSpellBySlotIndex(index);
		Cast(spell, target, finishedCallback);
	}


	#endregion

	#region Helpers


	private IEnumerator ProcessCast(SpellData spell, Tile target, CastFinishedDelegate finishedCallback = null) {
		for(int i = 0, il = spell.spellEffects.Count; i < il; i++) {
			bool effectFinished = false;

			SpellEffect effect = spell.spellEffects[i];
			effect.Activate(caster, target, () => effectFinished = true);

			while(!effectFinished) yield return null;
		}

		if(finishedCallback != null) finishedCallback();
	}


	#endregion
}