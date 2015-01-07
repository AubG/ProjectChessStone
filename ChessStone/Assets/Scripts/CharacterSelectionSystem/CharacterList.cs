using UnityEngine;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

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


	public int[] TakeSet(int setIndex, int setCount) {
		return _characterIds.Skip (setIndex * setCount).Take(setCount).ToArray();
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

	public int GetCharacterCount(int id) {
		int result = 0;
		int i = 0;
		for(; i < _characterIds.Count; i++) if(_characterIds[i] == id) break;
		if(i < _characterIds.Count) result = _characterCounts[i];

		return result;
	}


	#endregion
}

