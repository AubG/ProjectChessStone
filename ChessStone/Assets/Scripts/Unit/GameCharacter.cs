using UnityEngine;
using System.Collections;

public class GameCharacter : GameUnit
{
	[SerializeField]
	private PathScript _pathing;
	public PathScript pathing { 
		get { return _pathing; } 
		set { _pathing = value; }
	}
	

	#region Initialization


	void Start() {
		if(!pathing) {
			pathing = GetComponent<PathScript>();
			if(!pathing) {
				Debug.LogError("Path script has not been assigned to " + this.gameObject.name + "!");
				return;
			}
		}
	}


	#endregion

	public void OnDamage() {
	}
}