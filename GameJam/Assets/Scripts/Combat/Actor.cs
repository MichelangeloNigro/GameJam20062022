using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Actor")]
public class Actor : ScriptableObject {
    public float baseHealth;
    public GameObject actorPrefab;
    public List<GeneralCard> deck;
    public  int maxNumberOfCard=30;
    public int cardInHand=5;
}
