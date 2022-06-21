using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardReferenceHolder : MonoBehaviour {
	public TMP_Text name;
	public TMP_Text description;
	public Image image;
	public GeneralCard cardData;
	public RawImage border;

	private void Start() {
		switch (cardData.type) {
			case CardType.BuffCard:
				border.color = Color.yellow;
				break;
			case CardType.HealthCard:
				border.color = Color.green;

				break;
			case CardType.AttackCard:
				border.color = Color.red;
				break;
			case CardType.DefenceCard:
				border.color = Color.blue;
				break;
			default:
				border.color = Color.grey;
				break;
		}
	}

	public void OnCardClick() {
		if (cardData.quantityInDeck==0) {
			CardManager.Instance.Deck.Add(cardData);
			var temp=GameObject.Instantiate(GameManager.Instance.deckButtonPrefab,UiManager.Instance.deckSelectedContent.transform);
			temp.GetComponent<DeckIconReferenceHolder>().card = cardData;
		}
		cardData.quantityInDeck++;
		if (cardData.quantityInDeck+1<cardData.quantityUnlocked) {
			
		}
		else {
			Destroy(this.gameObject);
		}
		
	}
}