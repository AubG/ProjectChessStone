using UnityEngine;
using System.Collections;
using X_UniTMX;

public class GameUnitLink : GameUnit
{
	#region Game Data


	public GameCharacter head { get; set; }


	#endregion

	#region Init


	void Start() {
		unitType = UnitType.Link;
	}


	#endregion
}