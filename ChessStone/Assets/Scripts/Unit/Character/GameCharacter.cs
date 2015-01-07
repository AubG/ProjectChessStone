using UnityEngine;
using System.Globalization;
using System.Collections;
using System.Collections.Generic;
using X_UniTMX;

public delegate void DamageFinishedDelegate();

public class GameCharacter : GameUnit
{
	#region Modular Data
	

	[SerializeField]
	private Health _health;
	public Health health {
		get { return _health; }
		private set { _health = value; }
	}

	[SerializeField]
	private SpellBox _spellBox;
	public SpellBox spellBox {
		get { return _spellBox; }
		private set { _spellBox = value; }
	}

	[SerializeField]
	private Move _move;
	public Move move {
		get { return _move; }
		private set { _move = value; }
	}


	#endregion

	#region Game Data


	public int id { get; set; }

	public SpriteRenderer metaRenderer;

	/// <summary>
	/// The player that owns this character.
	/// </summary>
	private Player _owningPlayer;
	public Player owningPlayer {
		get { return _owningPlayer; }
	}

	private List<StatusEffect> statusEffects;

	public bool finished { get; private set; }

	public int currSize {
		get { return health.currSize; }
	}

	public int maxSize {
		get { return health.maxSize; }
	}

	public bool isDead {
		get { return health.isDead; }
	}


	#endregion

	#region Initialization


	void Start() {
		metaRenderer.enabled = false;
		unitType = UnitType.Character;
	}


	#endregion

	#region Interaction


	public void Damage(int amount, GameCharacter source, DamageFinishedDelegate finishedCallback = null)
	{
		health.Damage(amount, source, finishedCallback);
	}

	public void AddStatusEffect(StatusEffect newEffect, GameCharacter source)
	{
		statusEffects.Add(newEffect);
		newEffect.Activate(this, source);
	}

	public void SetOwningPlayer(Player p) {
		_owningPlayer = p;
		_owningPlayer.AddCharacter(this);
	}

	public void SetOwningPlayer(int id) {
		SetOwningPlayer(PlayerManager.Instance.GetPlayer(id));
	}

	public void MoveToTile(Tile t) {
		move.MoveToTile(t);
	}

	public void Finish() {
		finished = true;
		metaRenderer.enabled = true;
	}

	public void Reset() {
		finished = false;
		metaRenderer.enabled = false;
		move.ResetMoves();
	}


	#endregion

	#region Pathing Interaction


	public bool CanMove() {
		return !isDead && move.CanMove();
	}

	public bool CanMoveToTile(Tile t) {
		return !isDead && move.CanMoveToTile(t);
	}


	#endregion

	#region Spell Interaction


	public SpellData GetSpell (int abilityId) {
		return spellBox.GetSpell(abilityId);
	}
	
	public SpellData GetSpellBySlotIndex(int index) {
		return spellBox.GetSpellBySlotIndex(index);
	}

	public void Cast(SpellData spell, Tile target, CastFinishedDelegate finishedCallback = null) {
		spellBox.Cast(spell, target, finishedCallback);
	}

	public bool CanCastOnTarget(SpellData spell, Tile target) {
		return spellBox.CanCastOnTarget(spell, target);
	}


	#endregion
}