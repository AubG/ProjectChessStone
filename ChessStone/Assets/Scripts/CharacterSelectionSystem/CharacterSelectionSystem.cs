using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using X_UniTMX;

public class CharacterSelectionSystem : MonoBehaviour {

	#region State Data


	public enum State {
		Begin,
		Select,
		Finish,
		
		End
	}

	public State currState { get; protected set; }
	
	
	#endregion

	#region Graphics Data
	

	[SerializeField]
	private UILabel announcementLabel;

	[SerializeField]
	private UILabel timeLeftLabel;


	#endregion

	#region Modular Data
	
	
	[SerializeField]
	private CharacterSelectionController selectionController;

	
	#endregion

	#region Initialization


	public void Begin() {
		StartCoroutine(ProcessState());
	}


	#endregion
	
	#region States


	private IEnumerator ProcessState() {
		currState = State.Begin;

		while(currState != State.End) {
			switch(currState)
			{
			case State.Begin:
				yield return StartCoroutine(HandleBegin());
				break;
			case State.Select:
				yield return StartCoroutine(HandleSelect());
				break;
			case State.Finish:
				yield return StartCoroutine(HandleFinish());
				break;
			}
		}
	}

	private IEnumerator HandleBegin() {
		DisplayAnnouncement();

		yield return null;

		currState = State.Select;
	}

	private IEnumerator HandleSelect() {

		Player focusPlayer = PlayerManager.Instance.GetLocalPlayer();
		selectionController.Begin(focusPlayer);

		while(selectionController.currState != CharacterSelectionController.State.End) {
			yield return null;
		}

		currState = State.Finish;
	}

	private IEnumerator HandleFinish() {
		yield return new WaitForSeconds(1.0f);

		currState = State.End;
	}


	#endregion

	#region Helpers


	private void DisplayAnnouncement() {
		/*
		announcementLabel.text = "WELCOME MESSAGE";

		announcementParent.BroadcastMessage("OnActivate", true, SendMessageOptions.DontRequireReceiver);
		*/
	}


	#endregion
}