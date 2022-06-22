using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card/AttackCard",fileName = "Damage_")]
public class CardDamage : AttackCard
{
	public override void Use(ActorWorld chooser, ActorWorld target) {
		base.Use(chooser, target);
		chooser.animator.SetTrigger("shooting");
	}

	protected override void CardEffect() {
		base.CardEffect();
		target.ModifyHealth(-damage);
		Debug.Log("damage"+damage);
	}
}
