using UnityEngine;
using System.Collections;
using X_UniTMX;

public class GameStateManager : Singleton<GameStateManager> {

	#region State Data
	
	
	public enum State {
		CharacterSelection,
		Battle,
		Finish,
		End
	}
	
	private State currState;
	
	
	#endregion
	
	#region Modular Data
	
	
	[SerializeField]
	private CharacterSelectionSystem characterSelectionSystem;
	
	[SerializeField]
	private TurnSystem turnSystem;
	
	
	#endregion

	#region Init


	public void Begin() {
		StartCoroutine(UpdateState());
	}


	#endregion

	#region States


	private IEnumerator UpdateState() {
		currState = State.CharacterSelection;

		while(currState != State.End) {
			switch(currState)
			{
			case State.CharacterSelection:
				yield return StartCoroutine(HandleCharacterSelection());
				break;
			case State.Battle:
				yield return StartCoroutine(HandleBattle());
				break;
			case State.Finish:
				yield return StartCoroutine(HandleFinish());
				break;
			}
		}
	}

	private IEnumerator HandleCharacterSelection() {

		characterSelectionSystem.Begin();

		while(characterSelectionSystem.currState != CharacterSelectionSystem.State.End) {
			yield return null;
		}

		currState = State.Battle;
	}

	private IEnumerator HandleBattle() {
		turnSystem.Begin();

		while(turnSystem.currState != TurnSystem.State.End) {
			yield return null;
		}
		
		currState = State.Finish;
	}

	private IEnumerator HandleFinish() {
		yield return new WaitForSeconds(1.0f);

		currState = State.End;
	}


	#endregion

	#region Interaction


	public void HandleEnd() {
		PlayerData.Instance.lastLevel = Application.loadedLevel;
		Application.LoadLevel("Map");
	}


	#endregion
}