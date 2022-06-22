using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card/DdefenceCard",fileName = "DefenceCard_")]
public class DefenceCard : DefenseCard {

	public override void Use(ActorWorld chooser, ActorWorld target) {
		target.ModifyHealth(percent);
	}
}