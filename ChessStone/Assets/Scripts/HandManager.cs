using UnityEngine;
using System.Collections;
using X_UniTMX;

public class HandManager : MonoBehaviour {

	//Hand Array Containing Card objects
	//Has an extra node in position 8 that always contains Null
	//	for the playCard() function to set empty positions in Array 
	public DeckManager deck;
	private InvBaseCard[] hand = new InvBaseCard[8]; 
	public GameObject[] hand_positions = new GameObject[7];
	public int hand_pos = 0;

	public void drawHand () {
		for (int i = 0; i < 7; i++) {
			add (deck.nextCard());
			//Debug.Log (i+1 + ": " + hand[i].tag);
		}
		InvBaseCard blank = new InvBaseCard ();
		//blank. = -1;
		hand [7] = blank;
		update_hand ();
	}
	
	// Add cards to hand
	public void add(InvBaseCard card){
		if (hand_pos < 7) {
			hand[hand_pos] = card;
			hand_pos++;
			update_hand ();
			//update GUI
		}

	}

	//Pop card from hand and call Card() run function
	public void playCard(int index){
		hand_pos --;
		InvBaseCard toRet = hand[index];
		hand [index] = hand[7];
		for (int i = index; i < hand.Length-1; i++) {
			InvBaseCard temp = hand[i+1];
			hand [i] = temp;
		}
		//RefreshGUI();
		//Run Card Playing Script
		//toRet.run();
		update_hand ();
	}


	//Redraw Hand on screen
	void update_hand(){
		for (int i = 0; i< hand_pos; i++) {
			//Debug.Log(i);
			hand_positions[i].renderer.material.color = hand[i].color;
			}
		}
	}

