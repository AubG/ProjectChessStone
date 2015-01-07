using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using X_UniTMX;

/// <summary>
/// Manages health for GameUnits.
/// </summary>
public class Health : MonoBehaviour {

	#region Data


	public Color linkColor { get; set; }

	public int currSize {
		get {
			int count = links.Count;
			if(head != null) count++;
			return count;
		}
	}

	public int maxSize { get; set; }

	private GameCharacter _head;
	public GameCharacter head {
		get {
			return _head;
		}
	}

	private LinkedList<GameUnitLink> _links = new LinkedList<GameUnitLink>();
	public LinkedList<GameUnitLink> links {
		get { return _links; }
	}

	public bool isDead { get; private set; }
	
	
	#endregion

	#region Init


	public void Start() {
		isDead = false;

		_head = GetComponent<GameCharacter>();
	}


	#endregion
	
	#region Helper Methods


	private void HandleDeath(GameCharacter source) {
		//GameScore.Instance.RegisterDeath (this.gameObject);

		SendMessage("OnDeath", source, SendMessageOptions.DontRequireReceiver);
	}

	
	#endregion
	
	#region Public Interaction


	public void AddLink(Tile t) {
		if(currSize >= maxSize) return;

		GameUnitLink newLink = null;

		if(links.Count > 0) {
			newLink = (GameUnitLink)Instantiate(links.Last.Value);
			GameMap.Instance.RegisterUnit(newLink);
		} else {
			newLink = CharacterManager.Instance.CreateLink(linkColor);
		}

		newLink.name = _head.name + "." + links.Count;
		newLink.head = _head;
		newLink.SetTile(t);

		links.AddFirst(newLink);
	}

	public void RemoveTail() {
		GameUnitLink link = links.Last.Value;
		GameMap.Instance.RemoveUnit(link);
		links.RemoveLast();
	}

	public void Heal(int amount, GameCharacter source) {
	}

	public void Damage(int amount, GameCharacter source, DamageFinishedDelegate finishedCallback = null) {
		if(isDead || amount <= 0) return;

		// Decrease health by damage and send damage signals
		StartCoroutine(UpdateDamage(amount, finishedCallback));

		// Die if no health left
		if (currSize <= 0) {
			HandleDeath(source);
		}
	}

	private IEnumerator UpdateDamage (int amount, DamageFinishedDelegate finishedCallback = null) {
		int count = 0;
		while(count < amount && links.Count > 0) {
			RemoveTail();
			count++;
			yield return new WaitForSeconds(0.1f);
		}

		if(count < amount) {
			GameMap.Instance.RemoveUnit(_head);
		}

		finishedCallback();
	}

	
	#endregion
}