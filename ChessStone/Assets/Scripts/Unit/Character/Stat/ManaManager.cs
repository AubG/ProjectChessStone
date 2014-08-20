using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages mana for magical units.
/// NOTE: The name might be changed to something like:
/// vitae, faith, etc!!!!
/// </summary>
public class ManaManager : VitalManager {

	#region Data


	private bool dead = false;
	
	
	#endregion
	
	#region Initialization

	
	protected override void Start() {
		base.Start();
	}
	
	
	#endregion
	
	#region Public Interaction
	
	
	public void OnSpend(float amount) {
		// Spending more than possible mana is an error.
		if(!IsEnough(amount)) {
			Debug.LogError("Critical Error: A spell was allowed to cast even with not enough mana!");
			return;
		}
	
		currentValue -= amount;
	}

	public bool IsEnough(float amount) {
		return currentValue >= amount;
	}
	
	/// <summary>
	/// Computes the base value based on the given stat factor. This means that only one stat can affect the base value of the vital.
	/// </summary>
	public override void ComputeBaseValue(int factor) {
		baseMaxValue = valueBaseDefault;
		baseMaxValue += factor * 20;
		baseMaxValue = Mathf.Max (baseMaxValue, 0);
		
		ComputeAdjustedValue();
	}
	
	
	#endregion
}