using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using X_UniTMX;

public class CharacterManager : PersistentSingleton<CharacterManager>
{
	//public SimpleSQL.SimpleSQLManager dbManager;
	
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
		/*List<CharacterData> _characters = new List<CharacterData> (from w dbManager.Table<Weapon> ()
		                  select w);*/

		List<CharacterData> _characters = new List<CharacterData> {
			new CharacterData() {
				id = 0,
				name = "Demon",
				description = "Eradicator of the weak",
				spritePath = "Sprites/Characters/warlock",
				linkColorHex = "FF0000",
				moveRange = 2,
				maxSize = 4,
				spellIdSlotA = 0,
				spellIdSlotB = -1,
				spellIdSlotC = -1,
				spellIdSlotD = -1
			},
			new CharacterData() {
				id = 1,
				name = "Wizard",
				description = "Pay no attention to the man behind the curtain",
				spritePath = "Sprites/Characters/wizard",
				linkColorHex = "00CCFF",
				moveRange = 3,
				maxSize = 3,
				spellIdSlotA = 1,
				spellIdSlotB = -1,
				spellIdSlotC = -1,
				spellIdSlotD = -1
			},
			new CharacterData() {
				id = 2,
				name = "Scorpion",
				description = "Fast with a deadly sting",
				spritePath = "Sprites/Characters/scorpion",
				linkColorHex = "FFCC00",
				moveRange = 5,
				maxSize = 1,
				spellIdSlotA = 2,
				spellIdSlotB = -1,
				spellIdSlotC = -1,
				spellIdSlotD = -1
			}
		};

		foreach(CharacterData c in _characters) {
			_mappedCharacterData.Add(c.id, c);
			_mappedCharacterCounts.Add(c.id, 0);
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
			characterData.spellIdSlotA, 
			characterData.spellIdSlotB,
			characterData.spellIdSlotC,
			characterData.spellIdSlotD,
		};

		int i = 0;
		for(; i < spellIds.Length; i++) {
			spellBox.SetSpellSlotIndex((spellIds[i] > -1) ? SpellManager.Instance.GetSpellData(spellIds[i]) : null, i);
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

