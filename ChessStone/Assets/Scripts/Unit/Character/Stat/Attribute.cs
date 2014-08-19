
[System.Serializable]
public class Attribute : BaseStat
{
	
	#region Data
	
	
	public IStat.Identifier id;
	
	/// <summary>
	/// The exp to level.
	/// </summary>
	private int _expToLevel;
	public int ExpToLevel
	{
		get { return _expToLevel; }
		set { _expToLevel = value; }
	}
	
	/// <summary>
	/// The level modifier applied to the exp needed to raise the skill.
	/// </summary>
	private float _levelModifier;
	public float LevelModifier
	{
		get { return _levelModifier; }
		set { _levelModifier = value; }
	}
	
	/// <summary>
	/// Gets the modified exp to level.
	/// </summary>
	public int ModExpToLevel
	{
		get { return (int)(_expToLevel * _levelModifier); }
	}
	
	
	#endregion
	
	
	#region Initialization
	
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Attribute"/> class.
	/// </summary>
	public Attribute()
	{
		_levelModifier = 1.1f;
		_expToLevel = 100;
		ExpToLevel = 0;
		LevelModifier = 1.05f;
	}
	
	
	#endregion
	
	
	#region Interaction
	
	
	/// <summary>
	/// Levels up the stat.
	/// </summary>
	public void LevelUp()
	{
		_expToLevel = ModExpToLevel;
		BaseValue++;
	}
	
	
	#endregion
}