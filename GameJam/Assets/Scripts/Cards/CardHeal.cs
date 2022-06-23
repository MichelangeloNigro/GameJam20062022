using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card/HealCard",fileName = "Heal_")]
public class CardHeal : HealthCard
{
	public override void Use(ActorWorld chooser, ActorWorld target) {
		base.Use(chooser, target);
		chooser.animator.SetTrigger("shooting");
	}

	protected override void CardEffect() {
		base.CardEffect();
		target.ModifyHealth(amount);
		chooser.transform.LookAt(target.transform, Vector3.up);
		Debug.Log("Heal");
	}
}
