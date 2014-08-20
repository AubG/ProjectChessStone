using UnityEngine;
using System.Collections;

public class CardSelect : MonoBehaviour {
	public HandManager hand;
	public int index;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnMouseDown(){
		hand.playCard (index);
	}
}
