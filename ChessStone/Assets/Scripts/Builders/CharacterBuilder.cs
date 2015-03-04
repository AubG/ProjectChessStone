using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using X_UniTMX;

public class CharacterBuilder : PersistentSingleton<CharacterBuilder>
{
	private Dictionary<int, CharacterData> _mappedCharacterData = new Dictionary<int, CharacterData>();
	private Dictionary<int, int> _mappedCharacterCounts = new Dictionary<int, int>();

	[SerializeField]
	private GameCharacter baseCharacter;

	[SerializeField]
	private GameUnitLink baseLink;

	public override void Awake() {
		base.Awake();

		Load ();
	}

	public void Load() {
		DataService ds = new DataService("characters.bytes");
		
		IEnumerable<CharacterData> loaded = ds.GetItems<CharacterData>();
		if(loaded != null && loaded.Count() > 0) {
			foreach(CharacterData c in loaded) {
				_mappedCharacterData.Add(c.id, c);
				_mappedCharacterCounts.Add(c.id, 0);
			}
		} else {
			Debug.LogError("Loading characters failed!");
		}
	}

	public GameUnitLink CreateLink(Color c)
	{
		GameUnitLink link = (GameUnitLink)Instantiate(baseLink);
		
		SpriteRenderer renderer = link.GetComponent<SpriteRenderer>();

		renderer.color = c;

		GameMap.Instance.RegisterUnit(link);

		return link;
	}

	public GameCharacter BuildCharacter(int id, Player owner, Tile t) {
		CharacterData characterData = GetCharacterData(id);
		
		if(characterData == null) return null;
		
		GameCharacter spawnCharacter = (GameCharacter)Instantiate(baseCharacter);

		spawnCharacter.id = id;
		
		spawnCharacter.SetOwningPlayer(owner);
		
		// translate base character to game character by setting attributes
		spawnCharacter.gameName = characterData.name;
		spawnCharacter.name = _mappedCharacterCounts[id] + "-" + spawnCharacter.gameName.ToLower();

		spawnCharacter.gameDescription = characterData.description;
		
		// prefab specific data
		spawnCharacter.GetComponent<SpriteRenderer>().color = characterData.linkColor;
		spawnCharacter.avatarRenderer.sprite = characterData.sprite;

		spawnCharacter.health.linkColor = characterData.linkColor;
		
		// attributes
		Move move = spawnCharacter.move;
		move.SetBaseMoveRange(characterData.moveRange);

		Health health = spawnCharacter.health;
		health.maxSize = characterData.maxSize;
		
		// spells
		SpellBox spellBox = spawnCharacter.spellBox;

		int[] spellIds = { 
			characterData.spellIdA, 
			characterData.spellIdB,
			characterData.spellIdC,
			characterData.spellIdD,
		};

		int i = 0;
		for(; i < spellIds.Length; i++) {
			spellBox.SetSpellSlotIndex((spellIds[i] > -1) ? SpellBuilder.Instance.GetSpellData(spellIds[i]) : null, i);
		}

		spawnCharacter.Reset();
		spawnCharacter.SetTile(t);

		_mappedCharacterCounts[id]++;

		GameMap.Instance.RegisterCharacter(spawnCharacter);
		
		return spawnCharacter;
	}

	public GameCharacter BuildCharacter(int id, Player owner, int tileX, int tileY) {
		return BuildCharacter(id, owner, GameMap.Instance.mainGrid[tileX, tileY]);
	}

	public CharacterData GetCharacterData(int id) {
		return _mappedCharacterData[id];
	}

	public int GetCharacterCount(int id) {
		return _mappedCharacterCounts[id];
	}
}

