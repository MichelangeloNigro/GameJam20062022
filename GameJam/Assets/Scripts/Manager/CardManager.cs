using System.Collections.Generic;
using Riutilizzabile;
using UnityEngine;

public class CardManager : SingletonDDOL<CardManager> {
    public List<GeneralCard> Deck=new List<GeneralCard>();
    public List<GeneralCard> DeckChangable=new List<GeneralCard>();
    public List<GeneralCard> Hand=new List<GeneralCard>();
    public  int maxNumberOfCard=30;
    public int cardInHand=5;
    
    public const int maxNumberOfAttackCard=10;
    public const int maxNumberOfDefenseCard=10;
    public const int maxNumberOfHealthCard=10;
    public const int maxNumberOfBuffCard=10;

    private int numberOfAttackCard;
    private int numberOfCard;
    private int numberOfDefenseCard;
    private int numberOfHealthCard;
    private int numberOfBuffCard;

    public bool CheckIfCanAddCard() {
       return numberOfCard < maxNumberOfCard;
    }
    
    private bool CheckIfCanAddAttackCard() {
        if (!CheckIfCanAddCard()) {
            return false;
        }
        return numberOfAttackCard < maxNumberOfAttackCard;
    }
    
    private bool CheckIfCanAddDefenseCard() {
        if (!CheckIfCanAddCard()) {
            return false;
        }
        return numberOfDefenseCard < maxNumberOfDefenseCard;
    }
    
    private bool CheckIfCanAddHealthCard() {
        if (!CheckIfCanAddCard()) {
            return false;
        }
        return numberOfHealthCard < maxNumberOfHealthCard;
    }
    
    private bool CheckIfCanAddBuffCard() {
        if (!CheckIfCanAddCard()) {
            return false;
        }
        return numberOfBuffCard < maxNumberOfBuffCard;
    }

    public void getHand() {
        for (int i = 0; i < cardInHand; i++) {
            int temp = Random.Range(0, DeckChangable.Count);
            var tempGameObject=GameObject.Instantiate(GameManager.Instance.cardGameplayPrefab,UiManager.Instance.handContent.transform);
            tempGameObject.GetComponent<CardGameplay>().card = DeckChangable[temp];
            Hand.Add(DeckChangable[temp]);
            DeckChangable.Remove(DeckChangable[temp]);
        }
    }

    public void draw() {
        if (DeckChangable.Count>0&& Hand.Count<=cardInHand) {
            int temp = Random.Range(0, DeckChangable.Count);
            var tempGameObject=GameObject.Instantiate(GameManager.Instance.cardGameplayPrefab,UiManager.Instance.handContent.transform);
            tempGameObject.GetComponent<CardGameplay>().card = DeckChangable[temp];
            Hand.Add(DeckChangable[temp]);
            DeckChangable.Remove(DeckChangable[temp]);
        }
    }
}
