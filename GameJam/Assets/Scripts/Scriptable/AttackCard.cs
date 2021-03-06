using UnityEngine;

public abstract class AttackCard : GeneralCard {
    [SerializeField] protected int damage;
    public GameObject weaponModel;
    public GenericStatus status;
    public AudioClip grunt;
}
