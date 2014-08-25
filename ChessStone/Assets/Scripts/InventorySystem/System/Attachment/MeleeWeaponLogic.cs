using UnityEngine;
using System.Collections;

public class MeleeWeaponLogic : HandLogic
{
	#region Data

	
	private DamageManager damageManager;


	#endregion

	#region Initialization


	protected override void Start() {
		base.Start();

		if(!damageManager) {
			Transform characterRoot = transform.root;
			if(characterRoot.tag != "Player" && characterRoot.tag != "Character")
				Debug.LogError("Error: Weapon Attachment is not a child of a character!");

			damageManager = characterRoot.GetComponentInChildren<DamageManager>();
		}
	}


	#endregion

	#region Interaction

	
	protected override void OnTriggerEnter(Collider other) {
		if(!logicActive) return;

		base.OnTriggerEnter(other);

		if(other.tag == "Character") {
			DamageInfo damage = new DamageInfo(
				damageManager.adjustedValue,
				damageManager.type
			);

			other.SendMessage("OnDamageHit", damage, SendMessageOptions.DontRequireReceiver);
		}
	}


	#endregion
}