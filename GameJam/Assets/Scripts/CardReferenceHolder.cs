using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardReferenceHolder : UiCardDrawer {
	
	
	private void Update() {
		if (GameManager.Instance.cardsInDeck.ContainsKey(card)) {
			if (GameManager.Instance.unlockedCards[card]!=GameManager.Instance.cardsInDeck[card]) {
				GetComponent<Button>().interactable = true;
			}
			else {
				GetComponent<Button>().interactable = false;
			}
		}
		
	}

	public void OnCardClick() {
		if (CardManager.Instance.CheckIfCanAddCard()) {
			if (!GameManager.Instance.cardsInDeck.ContainsKey(card)) {
				var temp = GameObject.Instantiate(GameManager.Instance.deckButtonPrefab, UiManager.Instance.deckSelectedContent.transform);
				temp.GetComponent<DeckIconReferenceHolder>().card = card;
			}
			GameManager.Instance.AddCardToDeck(card);
			CardManager.Instance.Deck.Add(card);
		}
	}
}