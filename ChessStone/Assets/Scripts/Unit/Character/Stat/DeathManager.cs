using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manager to handle Character death (spawning ragdolls on death etc)
/// </summary>
public class DeathManager : MonoBehaviour {

	#region Data


	[SerializeField]
	private GameObject ragdollPrefab;


	#endregion

	#region Interaction
	
	
	public void OnDeath(DamageInfo deathDamage) {
	}
	
	
	#endregion
}