using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class SelectRandomCardOfType : Action {
	public CardType cardType;
	private ActorWorld actorWorld;
	
	public override void OnAwake() {
		actorWorld = GetComponent<ActorWorld>();
	}

	public override TaskStatus OnUpdate()
	{
		List<GeneralCard> hand = actorWorld.hand;
		var c = hand.FindAll(card => card.type == cardType);
		if (c.Count > 0) {
			actorWorld.SelectCard(c[Random.Range(0, c.Count)]);
			return TaskStatus.Success;
		}
		return TaskStatus.Failure;
	}
}