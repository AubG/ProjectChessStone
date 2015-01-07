using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : Singleton<PlayerManager>
{
	#region Game Data
	

	[SerializeField]
	private Player[] players;

	[SerializeField]
	private int _localPlayerIndex; 
	public int localPlayerIndex { 
		get { return _localPlayerIndex; } 
	}

	public int numPlayers { get; private set; }

	
	#endregion
	
	#region Init
	
	
	void Start() {
		numPlayers = players.Length;
	}

	
	#endregion
	
	#region Getters
	
	
	/// <summary>
	/// Get a player by index.
	/// </summary>
	public Player GetPlayer(int index) {
		return players[index];
	}

	public Player GetLocalPlayer() {
		return players[localPlayerIndex];
	}
	
	
	#endregion
}