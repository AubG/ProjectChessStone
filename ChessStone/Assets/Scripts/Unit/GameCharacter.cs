using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCharacter : GameUnit
{
	[SerializeField]
	private PathScript _pathing;
	public PathScript pathing { 
		get { return _pathing; } 
		private set { _pathing = value; }
	}

	[SerializeField]
	private HealthManager _health;
	public HealthManager health {
		get { return _health; }
		private set { _health = value; }
	}

	[SerializeField]
	private ManaManager _mana;
	public ManaManager mana {
		get { return _mana; }
		private set { _mana = value; }
	}

	[SerializeField]
	private List<GameSpell> _spells;

	
	#region Initialization


	void Start() {
		if(!pathing || !health || !mana) {
			pathing = GetComponent<PathScript>();
			health = GetComponent<HealthManager>();
			mana = GetComponent<ManaManager>();
			if(!pathing || !health || !mana) {
				Debug.LogError("Critical component missing from " + this.gameObject.name + "!");
				return;
			}
		}
	}


	#endregion

	public void OnDamage() {
	}
}