using System.Collections;
using System.Collections.Generic;
using Riutilizzabile;
using UnityEngine;

public class GameManager : SingletonDDOL<GameManager> {
    public List<GeneralCard> unlockedCards;
    public GameObject cardPrefab;
    public GameObject cardGameplayPrefab;
    public GameObject deckButtonPrefab;
    public int money;

    
    public void AddGold(int goldObtained) {
        money += goldObtained;
    }
}
