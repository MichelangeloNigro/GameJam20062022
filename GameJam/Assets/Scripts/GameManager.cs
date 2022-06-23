using System;
using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks.Unity.UnityPlayerPrefs;
using Riutilizzabile;
using UnityEngine;

public class GameManager : SingletonDDOL<GameManager> , ISavable {
    public CardQuantity unlockedCards;
    public GameObject cardPrefab;
    public GameObject cardGameplayPrefab;
    public GameObject deckButtonPrefab;
    public int money;
    public Dictionary<GeneralCard, int> cardsInDeck = new();
    public GameObject bloodVFX;

    private void OnEnable() {
        // foreach (var card in unlockedCards) {
        //     cardsInDeck.Add(card.Key, 0);
        // }
        SaveAndLoad.Instance.StartSave += OnSave;
    }

    private void OnDisable() {
        SaveAndLoad.Instance.StartSave -= OnSave;
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

    public void OnSave() {
        SaveAndLoad.Instance.ToBeSaved.money = money;
        Debug.Log(unlockedCards.Count);
        foreach (var VARIABLE in unlockedCards) {
            SaveAndLoad.Instance.ToBeSaved.cardUids.Add(VARIABLE.Key.uid);
            SaveAndLoad.Instance.ToBeSaved.cardNumber.Add(VARIABLE.Value);
        }
        
    }
}
