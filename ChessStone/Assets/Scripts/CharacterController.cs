using UnityEngine;
using System.Collections;
using X_UniTMX;

public enum EnumPlayerState {STAY=1,RIGHT=2,LEFT=3,DOWN=4,UP=5};

public class CharacterController : MonoBehaviour {

	#region Tile Data





	#endregion

	#region Game Data


	public float velocity = 5.0f;
	public float timeToBlink = 0.1f;
	public float timeMaxToBlink = 1.0f;

    private Vector3 velocityVector = Vector3.zero;
	private EnumPlayerState stateAnimation = EnumPlayerState.STAY; // 1 = Parado, 2 = Direita, 3 = Esquerda, 4 = Baixo, 5 = Cima
	private Animator scriptAnimator = null;

	private bool doBlink = false;
	private float countTimeBlink = 0.0f;
	private float countTimeMaxBlink = 0.0f;


	#endregion

	#region Initialization
	
	
	void Awake() {
		scriptAnimator = GetComponent<Animator>();
	}
	
	void Start() {
		// Locate the spawnPoint
		RespawnPoint[] objs = FindObjectsOfType<RespawnPoint>();
		foreach(RespawnPoint r in objs) {
			if(r.spawnPointName.Equals(PlayerPrefs.GetString("PlayerRespawn","Start"))) {
				transform.position = r.transform.position;
			}
		}
	}
	
	
	#endregion

	#region Update


	void Update() {
		CalculateShootTime ();

		HandlePlayerControls ();

		ApplyAnimation ();
		HandlePlayerBlink();
	}

	void FixedUpdate () {
		ApplyPlayerVelocity ();
	}

	private void ApplyAnimation() {
		if(scriptAnimator != null) {
			scriptAnimator.SetInteger("State",stateAnimation.GetHashCode());
		}
	}

	private void HandlePlayerControls ()
	{
		if(Input.GetButtonDown("Fire1")) {
			Vector2 mousePoint = Input.mousePosition;
			Vector2 worldPoint = Camera.main.ScreenToWorldPoint(mousePoint);
			Map gameMap = GameMap.Instance.map;
			Debug.Log (GameMap.Instance);
			Vector2 tileIndices = gameMap.WorldPointToTileIndex(worldPoint);
			Debug.Log("Clicked " + mousePoint + ":" + worldPoint + " (" + tileIndices + ")");
		}
	}
	
	private void ApplyPlayerVelocity ()
	{
		rigidbody2D.velocity = velocityVector;
	}

	private void CalculateShootTime ()
	{
		/*
		if (canShoot == false) {
			countTime = countTime + Time.deltaTime;
			if (countTime >= timeCanShoot) {
				countTime = 0.0f;
				canShoot = true;
			}
		}*/
	}

	private void HandlePlayerBlink() {
		if(doBlink) {
			countTimeBlink += Time.deltaTime;
			if(countTimeBlink >= timeToBlink) {
				countTimeBlink = 0.0f;
				GetComponent<SpriteRenderer>().enabled = !GetComponent<SpriteRenderer>().enabled;
				countTimeMaxBlink += timeToBlink;
				if(countTimeMaxBlink >= timeMaxToBlink) {
					doBlink = false;
					countTimeBlink = 0.0f;
					countTimeMaxBlink = 0.0f;
					GetComponent<SpriteRenderer>().enabled = true;
				}
			}
		}
	}


	#endregion

	#region Event Interaction


	void OnCollisionEnter2D(Collision2D other) {
		CheckEventTriggerCollision(other.gameObject);
	}

	void OnTriggerEnter2D(Collider2D other) {
		CheckEventTriggerCollision(other.gameObject);
	}

	void CheckEventTriggerCollision(GameObject other) {
		/*
		if(doBlink == false) {
			if("EnemyShoot".Equals(other.gameObject.tag)) {
				numberLife--;
				if(numberLife < 0) Application.LoadLevel("GameOver");
				doBlink = true;
			}
		}
		if("HealDrunks_Blue".Equals(other.gameObject.tag)) {
			numberShoots += 2;
		}
		if("HealDrunks_Yellow".Equals(other.gameObject.tag)) {
			numberShoots += 4;
		}
		if("HealDrunks_Green".Equals(other.gameObject.tag)) {
			numberShoots += 8;
		}
		PlayerPrefs.SetInt("PlayerShoots",numberShoots);
		if("HealDrunks_Red".Equals(other.gameObject.tag)) {
			numberLife += 1;
		}
		PlayerPrefs.SetInt("PlayerLife",numberLife);
		*/
	}


	#endregion

	#region Public Interaction


	public void SetStateAnimation( EnumPlayerState state ) {
		stateAnimation = state;
	}
	
	public EnumPlayerState GetPlayerState() {
		return stateAnimation;	
	}


	#endregion

	#region External Interaction


    // Call once at begin of the game
	void OnApplicationQuit() {
		PlayerPrefs.DeleteAll();
	}
	void OnLevelWasLoaded(int level) {
		PlayerPrefs.DeleteAll();
	}


	#endregion
}