using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Base stat.
/// </summary>

public class BaseStat {
	
	#region Data
	
	
	/// <summary>
	/// The base value of the stat.
	/// </summary>
	private int baseValue;
	public virtual int BaseValue {
		get { return baseValue; }
		set { baseValue = value; }
	}
	
	/// <summary>
	/// The list modifying attributes that modify this stat.
	/// </summary>
	private List<StatModifier> mods;
	
	/// <summary>
	/// The sum of all the bonuses from the mods.
	/// </summary>
	private int modValue;
	public virtual int ModValue {
		get { return modValue; }
	}
	
	/// <summary>
	/// Gets the modified base value.
	/// </summary>
	public int AdjustedBaseValue {
		get { return baseValue + modValue; }
	}
	
	
	#endregion
	
	
	#region Initialization
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="BaseStat"/> class.
	/// </summary>
	public BaseStat() {	
		mods = new List<StatModifier>();
	}
	
	
	#endregion
	
	#region Interaction
	
	
	/// <summary>
	/// Resets and calculates the mod value from the mods list.
	/// </summary>
	public void ComputeModValue() {
		modValue = 0;
		
		if(mods.Count > 0)
		{
			foreach(StatModifier mod in mods)
			{
				if(mod.modifier == IStat.Modifier.Percent) {
					modValue += baseValue * mod.amount * mod.ratio / 100;
				} else {
					modValue += mod.amount * mod.ratio;
				}
			}
		}
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