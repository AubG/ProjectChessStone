using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameCharacter : GameUnit
{
	#region Modular Data


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
	private SpellBox _spellBox;
	public SpellBox spellBox {
		get { return _spellBox; }
		private set { _spellBox = value; }
	}


	#endregion

	#region Game Data


	private List<StatusEffect> statusEffects;


	#endregion
	
	#region Initialization


	void Start()
	{
		if(!pathing || !health || !mana || !spellBox)
		{
			pathing = GetComponent<PathScript>();
			health = GetComponent<HealthManager>();
			mana = GetComponent<ManaManager>();
			spellBox = GetComponent<SpellBox>();
			if(!pathing || !health || !mana || !spellBox)
			{
				Debug.LogError("Critical component missing from " + this.gameObject.name + "!");
				return;
			}
		}
	}


	#endregion

	#region Interaction


	public void OnDamage()
	{
	}

	public void AddStatusEffect(StatusEffect newEffect) {
		statusEffects.Add(newEffect);
		newEffect.Activate(this);
	}


	#endregion
}