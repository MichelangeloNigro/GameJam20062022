using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckIconReferenceHolder : MonoBehaviour {
	public GeneralCard card;

	public void onDeselectCard() {
		card.quantityInDeck--;
		if (card.quantityInDeck-1==0) {
			Destroy(this.gameObject);
		}
		else {
			
		}
	}
}
