using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour
{
	#region Controller Data
	
	
	public enum ControllerType {
		User,
		Computer
	}
	
	[SerializeField]
	private ControllerType _controllerType;
	public ControllerType controllerType {
		get { return _controllerType; }
		private set { _controllerType = value; }
	}

	
	#endregion
	
	#region Game Data


	private List<GameCharacter> _characters = new List<GameCharacter>();
	public List<GameCharacter> characters {
		get { return _characters; }
	}

	#endregion

	#region Interaction


	public void AddCharacter(GameCharacter c) {
		if(!_characters.Contains(c))
		   _characters.Add(c);
		else
			Debug.LogError("Attempt to reregister a character under a player!");
	}

	public void RemoveCharacter(GameCharacter c) {
		if(_characters.Contains(c)) _characters.Remove(c);
	}

	public void ResetCharacters() {
		for(int i = 0, il = _characters.Count; i < il; i++) {
			_characters[i].Reset();
		}
	}

	public GameCharacter GetNextUnfinishedCharacter() {
		return _characters.Where(x => !x.finished).FirstOrDefault();
	}

	public bool CheckFinishedCharacters() {
		return GetNextUnfinishedCharacter() == null;
	}

	public int NumCharacters() {
		return characters.Count;
	}
	
	
	#endregion
}