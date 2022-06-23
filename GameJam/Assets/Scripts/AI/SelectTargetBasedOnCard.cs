using System.Collections;
using System.Collections.Generic;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class SelectTargetBasedOnCard : Action
{
	private ActorWorld actorWorld;
	
	public override void OnAwake() {
		actorWorld = GetComponent<ActorWorld>();
	}

	public override TaskStatus OnUpdate()
	{
		if (BattleManager.instance.card.type == CardType.AttackCard) {
			actorWorld.SelectTarget(TurnManager.Instance.PlayerActor);
		}
		else {
			actorWorld.SelectTarget(actorWorld);
		}
		return TaskStatus.Success;
	}
}
