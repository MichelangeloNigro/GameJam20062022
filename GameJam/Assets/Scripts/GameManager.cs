using System;
using System.Collections;
using System.Collections.Generic;
using Riutilizzabile;
using UnityEngine;

public class GameManager : SingletonDDOL<GameManager> {
    public CardQuantity unlockedCards;
    public Dictionary<GeneralCard, int> cardsInDeck = new();
    public GameObject cardPrefab;
    public GameObject cardGameplayPrefab;
    public GameObject deckButtonPrefab;
    public int money;

    private void OnEnable() {
        foreach (var card in unlockedCards) {
            cardsInDeck.Add(card.Key, 0);
        }
    }

    public void AddCardToDeck(GeneralCard card) {
        if (cardsInDeck.ContainsKey(card)) {
            cardsInDeck[card]++;
        }
        else {
            cardsInDeck.Add(card, 1);
        }
    }

    public void UnlockCard(GeneralCard card) {
        if (unlockedCards.ContainsKey(card)) {
            unlockedCards[card]++;
        }
        else {
            unlockedCards.Add(card, 1);
        }
    }
    
    public void AddGold(int goldObtained) {
        money += goldObtained;
    }
}
