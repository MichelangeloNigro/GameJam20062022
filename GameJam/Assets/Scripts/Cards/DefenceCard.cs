using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card/DefenceCard",fileName = "DefenceCard_")]
public class DefenceCard : DefenseCard {

	public int bonusAmount;
	public bool isPermanent;
	
	public override void Use(ActorWorld chooser, ActorWorld target) {
		base.Use(chooser, target);
		AudioSource.PlayClipAtPoint(sound,Vector3.zero);
	}
	
	
	protected override void CardEffect() {
		base.CardEffect();
		if (isPermanent) {
			chooser.defense += bonusAmount;
		}
		else {
			status.ApplyStatus(target, chooser, bonusAmount);
		}
	}
	
	
}