using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CardQuantity : UnitySerializedDictionary<GeneralCard, int> { }

[CreateAssetMenu(menuName = "ScriptableObjects/Actor")]
public class Actor : ScriptableObject {
    public float baseHealth;
    public GameObject actorPrefab;
    public List<CardQuantity> deck;
    public  int maxNumberOfCard=30;
    public int maxCardsInHand=5;
}
