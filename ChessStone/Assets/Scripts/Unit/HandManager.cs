using UnityEngine;
using System.Collections;
using X_UniTMX;

public class HandManager : MonoBehaviour {

	//Hand Array Containing Card objects
	//Has an extra node in position 8 that always contains Null
	//	for the playCard() function to set empty positions in Array 
	private Card[] hand = new Card[8]; 
	public int deck_pos = 0;
	void Start () {
		}
	
	// Update is called once per frame
	public void add(Card card){
		if (deck_pos < 7) {
			hand[deck_pos] = card;
			deck_pos++;
			//update GUI
		}

	}

	public void playCard(int index){
		deck_pos --;
		Card toRet = hand[index];
		hand [index] = null;
		for (int i = index; i < hand.Length-1; i++) {
			Card temp = hand[i+1];
			hand [i] = temp;
		}
		//RefreshGUI();
		//Run Card Playing Script
		toRet.run();
	}
}
