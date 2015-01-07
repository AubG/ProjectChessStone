using UnityEngine;
using System.Collections;
using X_UniTMX;

public class GameManager : MonoBehaviour {

	#region State Data


	public enum State {
		Init,
		Active,
		Finish,
		End
	}

	private State currState;
	
	
	#endregion

	#region Graphics Data


	[SerializeField]
	private GameObject announcementParent;

	[SerializeField]
	private UILabel announcementLabel;

	[SerializeField]
	private UILabel timeLeftLabel;


	#endregion

	#region Modular Data


	[SerializeField]
	private CharacterSelectionSystem characterSelectionSystem;

	[SerializeField]
	private TurnSystem turnSystem;
	
	
	#endregion

	#region Game Data

	
	#endregion

	#region Time Data
	

	[SerializeField]
	private float timeBetweenTurns = 150f;

	private float timeLeft = 0f;


	#endregion
	

	#region Initialization
	

	void Start() {
		Begin ();
	}

	public void Begin() {
		StartCoroutine(UpdateTime());
		StartCoroutine(UpdateGame());
	}


	#endregion

	#region Update
	

	private IEnumerator UpdateTime() {
		while(timeLeft >= 0f) {
			timeLeft -= Time.deltaTime;
			UpdateTimeDisplay();
			yield return null;
		}
	}

	private void UpdateTimeDisplay() {
		//timeLeftLabel.text = "Time Left: " + (int)timeLeft;
	}


	#endregion

	#region States


	private IEnumerator UpdateGame() {
		currState = State.Init;
		timeLeft = timeBetweenTurns;
		
		while(currState != State.End) {
			switch(currState)
			{
			case State.Init:
				yield return StartCoroutine(HandleSelect());
				break;
			case State.Active:
				yield return StartCoroutine(HandleActive());
				break;
			case State.Finish:
				yield return StartCoroutine(HandleFinish());
				break;
			}
		}
	}

	private IEnumerator HandleSelect() {

		characterSelectionSystem.Begin();

		while(characterSelectionSystem.currState != CharacterSelectionSystem.State.End) {
			yield return null;
		}

		currState = State.Active;
	}

	private IEnumerator HandleActive() {
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

	#region Transitions


	private IEnumerator TransitionState(State newState) {
		yield return null;
	}


	#endregion

	#region Helpers


	private void DisplayAnnouncement() {
		announcementLabel.text = "YOUR TURN, NOOB";
		
		announcementParent.BroadcastMessage("OnActivate", true, SendMessageOptions.DontRequireReceiver);
	}


	#endregion
}