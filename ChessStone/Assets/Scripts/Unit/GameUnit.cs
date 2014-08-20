using UnityEngine;
using System.Collections;

public class GameUnit : MonoBehaviour
{
	#region Game Data


	[SerializeField]
	private string _gameName;
	public string gameName {
		get { return _gameName; }
		private set { _gameName = value; }
	}


	#endregion

	#region Initialization


	void Start() {
	}


	#endregion
}