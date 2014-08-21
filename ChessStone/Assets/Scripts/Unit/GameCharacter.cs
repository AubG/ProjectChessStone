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
=======
<<<<<<< HEAD
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
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
<<<<<<< HEAD
=======
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
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
	
	#region Initialization


	void Start() {
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
		if(!pathing || !health || !mana || !spellBox) {
			pathing = GetComponent<PathScript>();
			health = GetComponent<HealthManager>();
			mana = GetComponent<ManaManager>();
			spellBox = GetComponent<SpellBox>();
			if(!pathing || !health || !mana || !spellBox) {
<<<<<<< HEAD
=======
=======
		if(!pathing || !health || !mana) {
			pathing = GetComponent<PathScript>();
			health = GetComponent<HealthManager>();
			mana = GetComponent<ManaManager>();
			if(!pathing || !health || !mana) {
>>>>>>> origin/master
>>>>>>> dbc9b9f45ca76778eed14be39ed942af27ad4bd7
				Debug.LogError("Critical component missing from " + this.gameObject.name + "!");
				return;
			}
		}
	}


	#endregion

	public void OnDamage() {
	}
}