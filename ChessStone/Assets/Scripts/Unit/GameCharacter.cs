using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCharacter : GameUnit
{
	#region Stats Data


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
<<<<<<< HEAD
	}

	[SerializeField]
	private ManaManager _mana;
	public ManaManager mana {
		get { return _mana; }
		private set { _mana = value; }
	}

	[SerializeField]
	private SpellBox _spellBox;
	public SpellBox spellBox {
		get { return _spellBox; }
		private set { _spellBox = value; }
	}


	#endregion
=======
	}

	[SerializeField]
	private ManaManager _mana;
	public ManaManager mana {
		get { return _mana; }
		private set { _mana = value; }
	}

	[SerializeField]
	private List<GameSpell> _spells;

>>>>>>> origin/master
	
	#region Initialization


	void Start() {
<<<<<<< HEAD
		if(!pathing || !health || !mana || !spellBox) {
			pathing = GetComponent<PathScript>();
			health = GetComponent<HealthManager>();
			mana = GetComponent<ManaManager>();
			spellBox = GetComponent<SpellBox>();
			if(!pathing || !health || !mana || !spellBox) {
=======
		if(!pathing || !health || !mana) {
			pathing = GetComponent<PathScript>();
			health = GetComponent<HealthManager>();
			mana = GetComponent<ManaManager>();
			if(!pathing || !health || !mana) {
>>>>>>> origin/master
				Debug.LogError("Critical component missing from " + this.gameObject.name + "!");
				return;
			}
		}
	}


	#endregion

	public void OnDamage() {
	}
}