using UnityEngine;

public abstract class AttackCard : GeneralCard {
    [SerializeField] protected int damage;
    //enemy
    private void OnAttack( /*enemy*/) {
	    //enemy.health -= damage;
    }
    public override void Use(){}
}
