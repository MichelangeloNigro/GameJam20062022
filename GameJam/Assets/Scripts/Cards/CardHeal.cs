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
		Debug.Log("Heal");
	}
}
