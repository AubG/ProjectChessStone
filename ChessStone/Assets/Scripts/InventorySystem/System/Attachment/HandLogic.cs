using UnityEngine;
using System.Collections;

public class HandLogic : MonoBehaviour
{
	#region Data


	// keep this so we can test whether the hand logic is working
	//public bool active;
	public bool logicActive { get; set; }


	#endregion

	#region Initialization


	protected virtual void Start() {
		logicActive = false;
	}


	#endregion

	#region Interaction

	
	protected virtual void OnTriggerEnter(Collider other) {
		if(!logicActive) return;
	}


	#endregion
}