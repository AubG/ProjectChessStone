using UnityEngine;
using System.Collections;
using X_UniTMX;

public class Controller : MonoBehaviour
{
	#region Game Data

	
	public Player currPlayer { get; protected set; }

	public Tile selectedTile { get; protected set; }
	public GameUnit selectedUnit { get; protected set; }
	public GameCharacter selectedCharacter { get; protected set; }

	public SpellData primedSpell { get; protected set; }


	#endregion

	#region Init


	public virtual void Begin(Player p) {
		currPlayer = p;
	}


	#endregion

	#region Helpers
	

	public virtual void HandleEnd() {
		currPlayer.ResetCharacters();
	}

	protected virtual void CheckFinishedCharacters() {
	}


	#endregion
}

