using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Manages health for GameUnits.
/// </summary>
public class HealthManager : VitalManager {

	#region Data
	

	private float lastDamageTime = 0f;
	
	private bool dead = false;
	
	
	#endregion
	
	#region Initialization

	
	protected override void Start() {
		base.Start();
	}
	
	
	#endregion
	
	#region Helper Methods


	private void HandleDeath(DamageInfo deathDamage) {
		//GameScore.Instance.RegisterDeath (this.gameObject);

		dead = true;

		SendMessage("OnDeath", deathDamage, SendMessageOptions.DontRequireReceiver);
	}

	
	#endregion
	
	#region Public Interaction
	
	
	public void OnDamage(DamageInfo damage) {
		float amount = damage.amount;

		// Take no damage if invincible, dead, or if the damage is zero
		if(dead || amount <= 0) return;
	
		// Decrease health by damage and send damage signals
	
		currentValue -= amount;
		lastDamageTime = Time.time;
	
		// Die if no health left
		if (currentValue <= 0) {
			HandleDeath(damage);
		}
	}
	
	/// <summary>
	/// Computes the base value based on the given stat factor. This means that only one stat can affect the base value of the vital.
	/// </summary>
	public override void ComputeBaseValue(int factor) {
		baseMaxValue = valueBaseDefault;
		baseMaxValue += factor * 20;
		baseMaxValue = Mathf.Max (baseMaxValue, 1);
		
		ComputeAdjustedValue();
	}
	
	
	#endregion
}