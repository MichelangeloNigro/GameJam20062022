using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class HasCardType : Conditional {
	public CardType cardType;
	private ActorWorld actorWorld;

	public override void OnAwake() {
		actorWorld = GetComponent<ActorWorld>();
	}

	public override TaskStatus OnUpdate() {
		List<GeneralCard> hand = actorWorld.hand;
		GeneralCard c = hand.Find(card => card.type == cardType);
		if (c) {
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}
