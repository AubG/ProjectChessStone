using UnityEngine;
using System.Collections;
using X_UniTMX;

public class TurnSystem : MonoBehaviour {

	#region State Data


	public enum State {
		Begin,

		Action,
		Finish,
		Next,
		
		End
	}

	public State currState { get; protected set; }
	
	
	#endregion

	#region Graphics Data
	

	[SerializeField]
	private UITurnIndicator turnIndicator;


	#endregion

	#region Modular Data
	
	
	[SerializeField]
	private UserController localUserController;

	[SerializeField]
	private ComputerController computerController;
	
	
	#endregion

	#region Game Data
	
	
	private int focusPlayerIndex = 0;



	#endregion

	#region Time Data
	

	[SerializeField]
	private float timeBetweenTurns = 150f;

	private float timeLeft = 0f;


	#endregion

	#region Initialization


	public void Begin() {
		StartCoroutine(UpdateTime());
		StartCoroutine(UpdateTurn());
	}

	public void EndTurn() {
		currState = State.End;
	}


	#endregion

	#region Update

	
	private IEnumerator UpdateTurn() {
		turnIndicator.ShowIndicator(true);

		while(true) {
			//Debug.Log ("Turn: " + focusPlayerIndex);
			bool isLocal = focusPlayerIndex == PlayerManager.Instance.localPlayerIndex;
			bool interactable = isLocal ? true : (PlayerManager.Instance.GetPlayer(focusPlayerIndex).controllerType == Player.ControllerType.Computer);

			turnIndicator.ToggleIndicator(isLocal, interactable);

			yield return StartCoroutine(PlayerTurn());

			focusPlayerIndex = (focusPlayerIndex + 1) % PlayerManager.Instance.numPlayers;
			TurnTime.time++;
		}

		turnIndicator.ShowIndicator(false);
	}

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


	private IEnumerator PlayerTurn() {
		currState = State.Begin;
		timeLeft = timeBetweenTurns;
		
		while(currState != State.Next) {
			switch(currState)
			{
			case State.Begin:
				yield return StartCoroutine(HandleBegin ());
				break;
			case State.Action:
				yield return StartCoroutine(HandleAction());
				break;
			case State.Finish:
				yield return StartCoroutine(HandleFinish());
				break;
			}
		}
	}

	private IEnumerator HandleBegin() {
		currState = State.Action;

		yield return null;
	}

	private IEnumerator HandleAction() {

		Player focusPlayer = PlayerManager.Instance.GetPlayer(focusPlayerIndex);
		if(focusPlayerIndex == PlayerManager.Instance.localPlayerIndex) {
			localUserController.Begin(focusPlayer);

			while(localUserController.currState != UserController.State.End) {
				yield return null;
			}
		} else {
			switch(focusPlayer.controllerType)
			{
			case Player.ControllerType.Computer:
				computerController.Begin(focusPlayer);

				while(computerController.currState != ComputerController.State.End) {
					yield return null;
				}

				break;
			case Player.ControllerType.User:
				break;
			}
		}

		currState = State.Finish;
	}

	private IEnumerator HandleFinish() {
		yield return new WaitForSeconds(1.0f);

		currState = State.Next;
	}


	#endregion

	#region Helpers


	private void DisplayAnnouncement() {
		/*
		if(focusPlayerIndex == PlayerManager.Instance.localPlayerIndex) {
			announcementLabel.text = "YOUR TURN";
		} else {
			announcementLabel.text = "ENEMY TURN";
		}
		
		announcementParent.BroadcastMessage("OnActivate", true, SendMessageOptions.DontRequireReceiver);
		*/
	}


	#endregion
}