using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Card/AttackCard",fileName = "Damage_")]
public class CardDamage : AttackCard
{
	public override void Use(ActorWorld chooser, ActorWorld target) {
		target.ModifyHealth(-damage);
		Debug.Log("damage"+damage);
		chooser.animator.SetTrigger("shooting");
	}
}
