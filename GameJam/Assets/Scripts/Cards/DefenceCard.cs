using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card/DefenceCard",fileName = "DefenceCard_")]
public class DefenceCard : DefenseCard {

	public override void Use(ActorWorld chooser, ActorWorld target) {
		target.ModifyHealth(percent);
	}
}