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
		actorWorld.SelectTarget(TurnManager.Instance.PlayerActor);
		return TaskStatus.Success;
	}
}