using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card/BuffCard",fileName = "BuffCard")]
public class CardBuff : BuffCard {
	public int bonusAmount;
	
	public override void Use(ActorWorld chooser, ActorWorld target) {
		base.Use(chooser, target);
	}
	
	protected override void CardEffect() {
		base.CardEffect();
		status.ApplyStatus(target,chooser,bonusAmount);
	}
}
