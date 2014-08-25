using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class VitalManager : MonoBehaviour {
	
	#region Data Constants
	
	
	[SerializeField]
	protected float valueBaseDefault = 10f;
	
	
	#endregion
	
	#region Data


	public float currentValue { get; protected set; }
	public float baseMaxValue { get; protected set; }
	public float adjustedMaxValue { get; protected set; }

	private List<StatModifier> mods;
	
	
	#endregion
	
	#region Initialization
	
	
	protected virtual void Awake() {
		mods = new List<StatModifier>();
		
		if(valueBaseDefault > 0f) {
			currentValue = baseMaxValue = adjustedMaxValue = valueBaseDefault;
		}
	}
	
	protected virtual void Start() {
		ComputeAdjustedValue();
	}
	
	
	#endregion
	
	#region Update Methods
	
	
	#endregion
	
	#region Interaction
	
	
	/// <summary>
	/// Computes the base value based on the given stat factor. This means that only one stat can affect the base value of the vital.
	/// </summary>
	public abstract void ComputeBaseValue(int factor);
	
	/// <summary>
	/// Sums all the mod values from the mods and adds them to the base value.
	/// </summary>
	public void ComputeAdjustedValue() {
		float lastRatio = currentValue / adjustedMaxValue;
		
		float modValue = 0f;
		
		if(mods.Count > 0) {
			foreach(StatModifier mod in mods) {
				if(mod.modifier == IStat.Modifier.Percent) {
					modValue += baseMaxValue * mod.amount * mod.ratio / 100;
				} else {
					modValue += mod.amount * mod.ratio;
				}
			}
		}
		
		adjustedMaxValue = Mathf.Max(baseMaxValue + modValue, 0);
		
		currentValue = Mathf.Max(adjustedMaxValue * lastRatio, 0);
	}
	
	/// <summary>
	/// Adds a stat modifier.
	/// </summary>
	public void AddMod(StatModifier mod) {
		if(mod != null) mods.Add(mod);
		else Debug.Log("Attempted to add a null mod!");
	}
	
	public StatModifier RemoveMod(int id) {
		for(int i = 0, il = mods.Count; i < il; i++) {
			StatModifier mod = mods[i];
			if(mod.id == id) {
				mods.RemoveAt(i);
				return mod;
			}
		}
		
		return null;
	}
	
	
	#endregion
}