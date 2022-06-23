using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class TargetPlayerAction : Action
{
	private ActorWorld actorWorld;
	
	public override void OnAwake() {
		actorWorld = GetComponent<ActorWorld>();
	}

	public override TaskStatus OnUpdate()
	{
		if (BattleManager.instance.card.type != CardType.AttackCard) {
			actorWorld.SelectTarget(actorWorld);
		}
		else {
			actorWorld.SelectTarget(TurnManager.Instance.PlayerActor);
		}
		return TaskStatus.Success;
	}
}