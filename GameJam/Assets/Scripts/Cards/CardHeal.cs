using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Card/HealCard",fileName = "Heal_")]
public class CardHeal : HealthCard
{
	public override void Use(ActorWorld chooser, ActorWorld target) {
		Debug.Log($"heal for {amount}");
		target.ModifyHealth(amount);
		if (canUnstunnedActor) {
			target.isStunned = false;
		}
	}
}

