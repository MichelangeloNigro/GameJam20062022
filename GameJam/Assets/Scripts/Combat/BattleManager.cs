using UnityEngine;

public class BattleManager : MonoBehaviour
{
	//Card
	private ActorWorld chooser;
	private ActorWorld target;
	
	public void Init() {
		TurnManager.Instance.OnCardSuccessfullySelected += SaveCard;
		TurnManager.Instance.OnTargetSuccessfullySelected += SaveActors;
	}

	private void OnDisable() {
		TurnManager.Instance.OnCardSuccessfullySelected -= SaveCard;
		TurnManager.Instance.OnTargetSuccessfullySelected -= SaveActors;
	}

	private void SaveCard() { }

	private void SaveActors(ActorWorld chooser, ActorWorld target) {
		this.chooser = chooser;
		this.target = target;
		Act();
	}

	private void Act() {
		target.ModifyHealth(-1);
		chooser.animator.SetTrigger("shooting");
	}
}
