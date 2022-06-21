using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Actor")]
public class Actor : ScriptableObject {
    public float baseHealth;
    public GameObject actorPrefab;
}
