using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class SelectLowestHealthEnemy : Action
{
	private ActorWorld actorWorld;
	
	public override void OnAwake() {
		actorWorld = GetComponent<ActorWorld>();
	}

	public override TaskStatus OnUpdate() {
		var enemies = TurnManager.Instance.Enemies;
		float lowestHealth = actorWorld.CurrentHealth;
		ActorWorld target = actorWorld;
		foreach (var enemy in enemies) {
			if (enemy.CurrentHealth < lowestHealth) {
				lowestHealth = enemy.CurrentHealth;
				target = enemy;
			}
		}
		actorWorld.SelectTarget(target);
		return TaskStatus.Success;
	}
}