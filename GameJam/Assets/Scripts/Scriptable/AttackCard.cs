using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card/AttackCard")]
public abstract class AttackCard : GeneralCard {
    [SerializeField] private int damage;
    //enemy
    private void OnAttack( /*enemy*/) {
	    //enemy.health -= damage;
    }
    public override void Use(){}
}
