using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerData : PersistentSingleton<PlayerData>
{
	#region Meta Data


	[SerializeField]
	private string _savePath;
	public string savePath {
		get { return _savePath; }
		private set { _savePath = value; }
	}

	public int lastLevel { get; set; }


	#endregion

	#region Game Data


	// stage
	public int currStage { get; set; }
	public List<int> completedStages { get; private set; }

	// characters
	public CharacterList characterList { get; private set; }

	// stats
	[SerializeField]
	private int _coins;
	public int coins {
		get { return _coins; }
	}

	public bool loaded { get; private set; }


	#endregion

	#region Init


	public override void Awake() {
		base.Awake();
		
		loaded = false;

		Load ();
	}

	void OnLevelWasLoaded(int level) {

	}

	public void New() {
		lastLevel = -1;

		currStage = -1;
		completedStages = new List<int>();

		characterList = new CharacterList();
		characterList.AddCharacter(0);

		loaded = true;
	}

	public void Load() {
		if(_savePath == "") {
			New();

			// TODO: Save at the end
		} else {
			// TODO: load using the save path
			StartCoroutine(LoadProgress());
		}
	}


	#endregion

	#region Interaction


	public void RegisterCompletedStage(int stageId) {
		completedStages.Add(stageId);
	}

	public void AddCoins(int amount) {
		_coins += amount;
	}

	public void AddCharacter(int characterId) {
		characterList.AddCharacter(characterId);
	}

	public void DepleteCharacter(int characterId) {
		characterList.DepleteCharacter(characterId);
	}


	#endregion

	#region Helpers


	private IEnumerator LoadProgress() {
		while(!loaded) {
			yield return null;
		}
	}


	#endregion
}