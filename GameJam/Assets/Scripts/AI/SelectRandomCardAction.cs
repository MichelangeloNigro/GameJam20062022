using BehaviorDesigner.Runtime.Tasks;

public class SelectRandomCardAction : Action {
	private ActorWorld actorWorld;
	
	public override void OnAwake() {
		actorWorld = GetComponent<ActorWorld>();
	}

	public override TaskStatus OnUpdate()
	{
		//actorWorld.SelectCard();
		return TaskStatus.Success;
	}
}