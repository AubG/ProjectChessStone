using System.Collections.Generic;

/// <summary>
/// Mod stat.cs
/// 
/// The base class for stats that are modified by attributes.
/// </summary>
public class StatModifier : IStat
{
	
	#region Data
	
	
	public int id = -1;
	public Modifier modifier;
	public int amount;
	public int ratio;
	
	
	#endregion
	
	
	#region Initialization
	
	
	public StatModifier() {
	}
	
	
	#endregion
	
	
	#region Interaction
	
	
	#endregion
}