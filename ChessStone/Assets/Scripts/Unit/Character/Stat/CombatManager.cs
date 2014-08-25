using UnityEngine;
using System.Collections.Generic;

public abstract class CombatManager : MonoBehaviour {
	
	#region Data Constants
	
	
	[SerializeField]
	protected float valueBaseDefault = 100f;
	
	
	#endregion
	
	#region Data


	public float baseValue { get; protected set; }
	public float adjustedValue { get; protected set; }
	
	private List<StatModifier> mods;
	
	
	#endregion
	
	#region Initialization
	
	
	protected virtual void Awake() {
		mods = new List<StatModifier>();
		
		if(valueBaseDefault > 0f) {
			baseValue = adjustedValue = valueBaseDefault;
		}
	}
	
	protected virtual void Start() {
		ComputeAdjustedValue();
	}
	
	
	#endregion
	
	#region Helper Methods
	
	
	#endregion
	
	#region Interaction
	
	
	/// <summary>
	/// Computes the base value based on the given stat factor. This means that only one stat can affect the base value of the vital.
	/// </summary>
	public abstract void ComputeBaseValue(int factor);
	
	/// <summary>
	/// Sums all the mod values from the mods and adds them to the base value.
	/// </summary>
	public virtual void ComputeAdjustedValue() {
		float modValue = 0f;
		
		if(mods.Count > 0) {
			foreach(StatModifier mod in mods) {
				if(mod.modifier == IStat.Modifier.Percent) {
					modValue += baseValue * mod.amount * mod.ratio / 100;
				} else {
					modValue += mod.amount * mod.ratio;
				}
			}
		}
		
		adjustedValue = baseValue + modValue;
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