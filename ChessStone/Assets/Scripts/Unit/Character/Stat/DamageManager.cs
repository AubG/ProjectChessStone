using UnityEngine;
using System.Collections.Generic;

public class DamageManager : CombatManager {
	
	#region Data


	public DamageType type { get; set; }
	
	
	#endregion
	
	#region Interaction
	
	
	/// <summary>
	/// Computes the base value based on the given stat factor. This means that only one stat can affect the base value of the vital.
	/// </summary>
	public override void ComputeBaseValue(int factor) {
		baseValue = Mathf.Max(factor, 0);;
		
		ComputeAdjustedValue();
	}
	
	/// <summary>
	/// Sums all the mod values from the mods and adds them to the base value.
	/// </summary>
	public override void ComputeAdjustedValue() {
		base.ComputeAdjustedValue();
		
		adjustedValue = Mathf.Max(adjustedValue, 0f);
	}
	
	
	#endregion
}