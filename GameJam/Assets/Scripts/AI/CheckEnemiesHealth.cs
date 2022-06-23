using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

public class CheckEnemiesHealth : Conditional {
	public float percentageTreshold;

	public override TaskStatus OnUpdate() {
		var enemies = TurnManager.Instance.Enemies;
		foreach (var enemy in enemies) {
			var hpPerc = enemy.CurrentHealth * 100 / enemy.MaxHealth;
			if (hpPerc <= percentageTreshold) {
				return TaskStatus.Success;
			}
		}
		return TaskStatus.Failure;
	}
}