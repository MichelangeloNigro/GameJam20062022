using UnityEngine;

public class BattleManager : MonoBehaviour {
	private GeneralCard card;
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

	private void SaveCard(GeneralCard card) {
		this.card = card;
	}

	private void SaveActors(ActorWorld chooser, ActorWorld target) {
		this.chooser = chooser;
		this.target = target;
		Act();
	}

	private void Act() {
		card.Use(chooser, target);
		// target.ModifyHealth(-1);
		// chooser.animator.SetTrigger("shooting");
	}
}
