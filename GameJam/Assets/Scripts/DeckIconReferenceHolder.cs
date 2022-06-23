using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeckIconReferenceHolder : MonoBehaviour {
	public GeneralCard card;
	public TMP_Text name;
	public TMP_Text quantity;

	private void Update() {
		name.text = card.cardName;
		quantity.text = $"X {GameManager.Instance.cardsInDeck[card]}";
	}

	public void onDeselectCard() {
		GameManager.Instance.cardsInDeck[card]--;
		GameManager.Instance.cardsInDeck[card] = Mathf.Clamp(GameManager.Instance.cardsInDeck[card], 0, 100);
		CardManager.Instance.Deck.Remove(card);
		if (GameManager.Instance.cardsInDeck[card]==0) {
			Destroy(this.gameObject);
		}
		else {
			
		}
	}
}
