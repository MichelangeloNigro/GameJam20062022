using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card/AttackCard",fileName = "Damage_")]
public class CardDamage : AttackCard
{
	public override void Use(ActorWorld chooser, ActorWorld target) {
		base.Use(chooser, target);
		chooser.animator.SetTrigger("shooting");
		Destroy(chooser.currWeapon);
		chooser.currWeapon=Instantiate(weaponModel, chooser.handR);
	}

	protected override void CardEffect() {
		base.CardEffect();
		target.ModifyHealth(-damage);
		chooser.transform.LookAt(target.transform, Vector3.up);
		Debug.Log("damage"+damage);
	}
}
