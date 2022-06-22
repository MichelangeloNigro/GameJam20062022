using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

public class SelectRandomCardAction : Action {
	private ActorWorld actorWorld;
	
	public override void OnAwake() {
		actorWorld = GetComponent<ActorWorld>();
	}

	public override TaskStatus OnUpdate()
	{
		actorWorld.SelectCard(actorWorld.hand[Random.Range(0,actorWorld.hand.Count)]);
		return TaskStatus.Success;
	}
}