using UnityEngine;
using System.Collections;


public class DeckManager : MonoBehaviour {

	public Card[] player_deck = new Card[30];
	public int deck_count = 0;
	// Use this for initialization
	public void add(Card card){
		if (deck_count < 30){
			player_deck[deck_count]=card;
			deck_count++;
		}
	}

	public Card nextCard(){
		Card temp = player_deck [deck_count];
		deck_count--;
		return temp;
	}

	// Update is called once per frame
	void Update () {
	
	}
}
