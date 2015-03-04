using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class SpellBuilder : PersistentSingleton<SpellBuilder>
{
	//public SimpleSQL.SimpleSQLManager dbManager;
	
	private Dictionary<int, SpellData> _mappedSpellData = new Dictionary<int, SpellData>();
	
	private Dictionary<string, Type> _cachedSpellEffectTypes = new Dictionary<string, Type>();

	public override void Awake() {
		base.Awake();

		Load ();
	}

	public void Load() {
		DataService ds = new DataService("abilities.bytes");
		
		IEnumerable<SpellData> loaded = ds.GetItems<SpellData>();
		if(loaded != null && loaded.Count() > 0) {
			foreach(SpellData s in loaded) {
				_mappedSpellData.Add(s.id, s);
			}
		} else {
			Debug.LogError("Loading characters failed!");
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