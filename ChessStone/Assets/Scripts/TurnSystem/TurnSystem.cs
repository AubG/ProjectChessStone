using UnityEngine;
using System.Collections;
using X_UniTMX;

public delegate void TurnEnded(TurnInfo tI);


public class TurnSystem : MonoBehaviour {

	#region State Data


	public enum State {
		Begin,
		
		// select and action are really just two substates of the bigger 
		// "action state"
		Action,
		Finish,
		
		End
	}

	private State currState;
	
	
	#endregion

	#region External Data
	
	
	[SerializeField]
	private TileSelector tileSelector;
	
	
	#endregion

	#region Game Data


	//private Player[] players;


	#endregion

	#region Time Data


	[SerializeField]
	private UILabel timeLeftLabel;

	[SerializeField]
	private float timeBetweenTurns = 150f;

	private float timeLeft = 0f;


	#endregion

	public TurnInfo playerTurnInfo { get; private set; }
	public TurnInfo enemyTurnInfo { get; private set; }
	public event TurnEnded enemyTurnEnded;
	public event TurnEnded playerTurnEnded;
	private int turnNumber = 1;
	public int movesPerTurn = 3;
	

	#region Initialization


	// Use this for initialization
	void Start () {
		GameMap.Instance.InitializeObjects ();
		StartCoroutine(UpdateTime ());
		StartCoroutine(UpdateState());
		//Immediately start our loop

		BaseSpell spell = SpellDatabase.FindByName("Blood Cleave");
		Debug.Log (spell.abilityEffects.Count);
	}


	#endregion
	
	// Update is called once per frame
	private IEnumerator UpdateState() {
		while(true) {
			//This is short hand for infinity loop.  Same as while(true).   
			yield return StartCoroutine(PlayerTurn());
			//Start our player loop, and wait for it to finish
			//Before we continue.
		}
	}

	private IEnumerator UpdateTime() {
		while(timeLeft >= 0f) {
			timeLeft -= Time.deltaTime;
			UpdateTimeDisplay();
			yield return null;
		}
	}
	
	private IEnumerator PlayerTurn() {
		currState = State.Begin;
		timeLeft = timeBetweenTurns;

		while(currState != State.End) {
			switch(currState) {
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

		yield return StartCoroutine(HandleEnd ());
	}

	private void UpdateTimeDisplay() {
		timeLeftLabel.text = "Time Left: " + (int)timeLeft;
	}

	private IEnumerator HandleBegin() {
		Debug.Log ("Begin");

		currState = State.Action;

		yield return null;
	}

	private IEnumerator HandleAction() {
		tileSelector.StartSelect();

		while(tileSelector.currState != TileSelector.State.End) {
			yield return null;
		}

		currState = State.Finish;

		Debug.Log ("finished player move");
	}

	private IEnumerator HandleFinish() {

		currState = State.End;

		yield return null;
	}

	private IEnumerator HandleEnd() {
		TurnInfo tI = new TurnInfo();
		turnNumber++;
		
		if (turnNumber % 2 == 1) {
			
			if (playerTurnEnded != null) 
				playerTurnEnded (tI);
		} else {
			if(enemyTurnEnded != null)
				enemyTurnEnded(tI);
		}

		currState = State.Begin;

		yield return null;
	}

	#region Helpers


	#endregion
}

public struct TurnInfo {
	//custom Turn info struct.  
	//Feel free add or remove any fields.
	
	private float m_DamageDone;
	private string m_MoveUsed;
	private float m_HealthLeft;             
	private bool m_TurnOver;
	
	public float DamageDone
	{
		get {
			return m_DamageDone;
		}
		
		set {
			m_DamageDone = value;
		}
	}
	//How much damage did we do.
	
	public string MoveUsed
	{
		get {
			return m_MoveUsed;
		}
		
		set {
			m_MoveUsed = value;
		}
	}
	//What move did we use.
	
	public float HealthLeft
	{
		get {
			return m_HealthLeft;
		}
		
		set {
			m_HealthLeft = value;
		}
	}
	//How much health do we have left.
	
	public bool TurnOver
	{
		get {
			return m_TurnOver;
			//read only
		}
	}
	//Should the turn end?
	
	//Constructor.
	public TurnInfo(float damage, string moveUsed, float healthLeft, bool turnOver) {   
		m_DamageDone = damage;
		m_MoveUsed = moveUsed;
		m_HealthLeft = healthLeft;
		m_TurnOver = turnOver;
	}
}