using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckIconReferenceHolder : MonoBehaviour {
	public GeneralCard card;
	public TMP_Text name;
	public TMP_Text quantity;

	private void Start() {
		name.text = card.cardName;
		quantity.text = $"X {card.quantityInDeck}";
	}

	public void onDeselectCard() {
		card.quantityInDeck--;
		card.quantityInDeck = Mathf.Clamp(card.quantityInDeck, 0, 100);
		CardManager.Instance.Deck.Remove(card);
		if (card.quantityInDeck==0) {
			Destroy(this.gameObject);
		}
		else {
			
		}
	}
}
