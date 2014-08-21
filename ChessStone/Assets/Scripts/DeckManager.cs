using UnityEngine;
using System.Collections;
using System;


public class DeckManager : MonoBehaviour {

	public HandManager hand;
	public InvBaseCard[] player_deck = new InvBaseCard[30];
	public GameObject[] cards = new GameObject[5];
	public int deck_count = 0;
	public InvCardDatabase decks;

	void Start() {
		System.Random gen = new System.Random ();
		
		for (int i = 0; i < player_deck.Length; i++) {
			int index = (gen.Next(5));
			add (decks.items[index]);
		}
		hand.drawHand ();

     }

	// Add Cards to deck
	public void add(InvBaseCard card){
		if (deck_count < 30){
			player_deck[deck_count]=card;
			deck_count++;
		}
	}

	//Pops top card of deck
	public InvBaseCard nextCard(){
		InvBaseCard temp = player_deck [deck_count-1];
		deck_count--;
		return temp;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
