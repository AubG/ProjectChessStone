using UnityEngine;
using System.Collections;

public class ActivateSpellOnClick : MonoBehaviour
{
	/// <summary>
	/// The name of the spell to activate when clicked.
	/// </summary>
	public string activateName { get; private set; }

	/// <summary>
	/// The TileSelector that this notifies when clicked.
	/// </summary>
	public TileSelector currSelector;

	public void DefineSpell(string name) {
		activateName = name;
	}
	
	void OnClick()
	{
		currSelector.PrimeSpell(activateName);
	}
}