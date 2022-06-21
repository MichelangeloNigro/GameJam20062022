using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardReferenceHolder : UiCardDrawer {
	
	
	private void Update() {
		if (card.quantityInDeck!=card.quantityUnlocked) {
			GetComponent<Button>().interactable = true;
		}
		else {
			GetComponent<Button>().interactable = false;
		}
	}

	public void OnCardClick() {
		if (card.quantityInDeck==0) {
			var temp=GameObject.Instantiate(GameManager.Instance.deckButtonPrefab,UiManager.Instance.deckSelectedContent.transform);
			temp.GetComponent<DeckIconReferenceHolder>().card = card;
		}
		card.quantityInDeck++;
		CardManager.Instance.Deck.Add(card);

	}
}