using System;
using System.Collections.Generic;
using System.Net;
using Riutilizzabile;
using UnityEngine;

public class CardManager : SingletonDDOL<CardManager> {
    public List<GeneralCard> Deck;
    public  int maxNumberOfCard=10;
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
}
