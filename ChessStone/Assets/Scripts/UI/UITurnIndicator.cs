using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections;

public class UITurnIndicator : MonoBehaviour
{
	[SerializeField]
	private Button indicator;

	[SerializeField]
	private int localPersistentEventIndex;

	[SerializeField]
	private int enemyPersistentEventIndex;

	public void ShowIndicator(bool flag) {
		indicator.GetComponent<CanvasGroup>().alpha = 1;
	}

	public void ToggleIndicator(bool isLocal, bool interactable) {
		indicator.interactable = interactable;

		Text indicatorText = indicator.GetComponentInChildren<Text>();
		if(isLocal) {
			UnityAction<BaseEventData> newAction = new UnityAction<BaseEventData>(delegate {
			});

			indicator.onClick.SetPersistentListenerState(localPersistentEventIndex, UnityEventCallState.RuntimeOnly);
			indicator.onClick.SetPersistentListenerState(enemyPersistentEventIndex, UnityEventCallState.Off);
			indicatorText.text = "END TURN";
		} else {
			indicatorText.color = Color.black;
			indicatorText.text = "ENEMY TURN";

			indicator.onClick.SetPersistentListenerState(localPersistentEventIndex, UnityEventCallState.Off);
			indicator.onClick.SetPersistentListenerState(enemyPersistentEventIndex, UnityEventCallState.RuntimeOnly);
		}
	}
}