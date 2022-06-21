using System;
using System.Collections.Generic;
using System.Net;
using Riutilizzabile;
using UnityEngine;

public class CardManager : SingletonDDOL<CardManager> {
    public CardManager instance;

    public List<GeneralCard> Deck;
    private const int maxNumberOfCard=10;
    private const int maxNumberOfAttackCard=10;
    private const int maxNumberOfDefenseCard=10;
    private const int maxNumberOfHealthCard=10;
    private const int maxNumberOfBuffCard=10;

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
}
