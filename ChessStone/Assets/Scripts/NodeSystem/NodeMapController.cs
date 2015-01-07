using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;

public class NodeMapController : MonoBehaviour
{
	#region State Data


	public enum State {
		Neutral,
		End
	}
	
	public State currState { get; protected set; }


	#endregion

	#region Graphics Data


	[SerializeField]
	private UIUnitData unitData;
	

	#endregion

	#region Initialization


	void Start() {
		Begin ();
	}

	public void Begin()
	{
		currState = State.Neutral;

		NodeSelector.Instance.clickCallback = OnNodeClick;
		NodeSelector.Instance.StartSelect();
	}


	#endregion

	#region Update Methods
	

	public void HandleEnd() {
		currState = State.End;
		NodeSelector.Instance.StopSelect();
	}


	#endregion

	#region Interaction



	#endregion

	#region Event Handlers


	private void OnNodeClick(MapNode n) {
		if(n.stageId != -1) {
			PlayerData.Instance.lastLevel = Application.loadedLevel;
			PlayerData.Instance.currStage = n.stageId;
			Application.LoadLevel("Main");
		} else if(n.merchantId != -1) {
		
		}
	}


	#endregion
}