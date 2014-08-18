using UnityEngine;
using System.Collections;
using System;


public class DeckManager : MonoBehaviour {

	public HandManager hand;
	public Card[] player_deck = new Card[30];
	public GameObject[] cards = new GameObject[5];
	public int deck_count = 0;

	void Start() {

		//TEST CARDS
		Card[] cards = new Card[5];
		Debug.Log ("In Deck");
		Card card1 = new Card ();
		card1.id = 0;
		cards [0] = card1;
		card1.tag = "card1";
		Card card2 = new Card ();
		card2.tag = "card2";
		card2.id = 1;
		cards [1] = card2;
		Card card3 = new Card ();
		card3.id = 2;
		cards [2] = card3;
		card3.tag = "card3";
		Card card4 = new Card ();
		card4.id = 3;
		cards [3] = card4;
		card4.tag = "card4";
		Card card5 = new Card ();
		card5.id = 4;
		cards [4] = card5;
		card5.tag = "card5";
		System.Random gen = new System.Random ();
		
		for (int i = 0; i < player_deck.Length; i++) {
			int index = (gen.Next(4)+1);
			add (cards[index]);
		}
		hand.drawHand ();

     }

	// Add Cards to deck
	public void add(Card card){
		if (deck_count < 30){
			player_deck[deck_count]=card;
			deck_count++;
		}
	}

	//Pops top card of deck
	public Card nextCard(){
		Card temp = player_deck [deck_count-1];
		deck_count--;
		return temp;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
