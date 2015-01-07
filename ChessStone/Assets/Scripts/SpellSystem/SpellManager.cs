using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SpellManager : PersistentSingleton<SpellManager>
{
	//public SimpleSQL.SimpleSQLManager dbManager;
	
	private Dictionary<int, SpellData> _mappedSpellData = new Dictionary<int, SpellData>();
	
	private Dictionary<string, Type> _cachedSpellEffectTypes = new Dictionary<string, Type>();

	public override void Awake() {
		base.Awake();

		Load ();
	}

	public void Load() {
		/*List<CharacterData> _characters = new List<CharacterData> (from w dbManager.Table<Weapon> ()
		                  select w);*/

		List<SpellData> _spells = new List<SpellData> {
			new SpellData() {
				id = 0,
				name = "Reap",
				description = "Deletes 2 sectors from the target enemy. Instantly destroys the target if it is below 20% of its maximum size.",
				iconPath = "Sprites/Characters/warlock",
				targetRange = 1,
				validTargetsMask = "0100",
				spellEffectsPath = "Spells/reap"
			},
			new SpellData() {
				id = 1,
				name = "Stretch",
				description = "Adds 1 sector to the target.",
				iconPath = "Sprites/Characters/wizard",
				targetRange = 1,
				validTargetsMask = "1100",
				spellEffectsPath = "Spells/stretch"
			},
			new SpellData() {
				id = 2,
				name = "Sting",
				description = "Deletes 3 sectors from the target enemy.",
				iconPath = "Sprites/Characters/scorpion",
				targetRange = 1,
				validTargetsMask = "0100",
				spellEffectsPath = "Spells/sting"
			}
		};

		foreach(SpellData s in _spells) {
			_mappedSpellData.Add(s.id, s);
		}
	}

	public SpellData GetSpellData(int id) {
		return (id > -1) ? _mappedSpellData[id] : null;
	}

	public Type GetSpellEffectType(string name) {
		string fullName = typeof(SpellEffect).Namespace + "." + name;
		
		if (_cachedSpellEffectTypes.ContainsKey(fullName))
			return _cachedSpellEffectTypes[fullName];
		
		Type type = Type.GetType(fullName);
		_cachedSpellEffectTypes.Add(fullName, type);
		
		return type;
	}
}