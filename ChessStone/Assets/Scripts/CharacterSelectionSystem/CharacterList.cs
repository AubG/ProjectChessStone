using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public struct CharacterRecord {
	public int id;
	public int count;
}

[DoNotSerializePublic]
public class CharacterList
{
	#region Game Data
	
		
	[SerializeThis]
	private List<int> _characterIds = new List<int>();

	[SerializeThis]
	private List<int> _characterCounts = new List<int>();
		

	#endregion

	#region Initialization


	#endregion

	#region Interaction


	public int GetNumSets(int setCount) {
		return Mathf.CeilToInt(_characterIds.Count / (float)setCount);
	}

	public CharacterRecord[] TakeSet(int setIndex, int setCount) {
		int[] setIds = _characterIds.Skip (setIndex * setCount).Take(setCount).ToArray();
		int[] setCounts = _characterCounts.Skip (setIndex * setCount).Take(setCount).ToArray();
		CharacterRecord[] result = new CharacterRecord[setIds.Length];
		for(int i = 0; i < setIds.Length; i++) {
			result[i] = new CharacterRecord() {
				id = setIds[i],
				count = setCounts[i]
			};
		}

		return result;
	}

	public void AddCharacter(int id) {
		int i = 0;
		for(; i < _characterIds.Count; i++) if(_characterIds[i] == id) break;

		if(i != _characterIds.Count) {
			_characterCounts[i]++;
		} else {
			_characterIds.Add(id);
			_characterCounts.Add(1);
		}
	}

	public void DepleteCharacter(int id) {
		int i = 0;
		for(; i < _characterIds.Count; i++) if(_characterIds[i] == id) break;

		if(i != _characterIds.Count) {
			_characterCounts[i]--;
			if(_characterCounts[i] <= 0) {
				_characterIds.RemoveAt(i);
				_characterCounts.RemoveAt(i);
			}
		} else {
			Debug.LogError("PlayerData: Tried to remove a nonexistant character from the Character List");
		}
	}

	public int GetCharacterCount(int id) {
		int result = 0;
		int i = 0;
		for(; i < _characterIds.Count; i++) if(_characterIds[i] == id) break;
		if(i < _characterIds.Count) result = _characterCounts[i];

		return result;
	}


	#endregion
}

