using UnityEngine;

public class BattleManager : MonoBehaviour {
	public GeneralCard card;
	public ActorWorld chooser;
	public ActorWorld target;
	
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
		card = null;
		target = null;
		chooser = null;
		// target.ModifyHealth(-1);
		// chooser.animator.SetTrigger("shooting");
	}
}
