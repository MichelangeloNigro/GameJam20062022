using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card/AttackCard",fileName = "Damage_")]
public class CardDamage : AttackCard
{
	public override void Use(ActorWorld chooser, ActorWorld target) {
		base.Use(chooser, target);
		Destroy(chooser.currWeapon);
		chooser.currWeapon=Instantiate(weaponModel, chooser.handR);
		GameManager.Instance.StartCoroutine(spawnVfx(chooser,target));
	}

	public IEnumerator spawnVfx(ActorWorld self, ActorWorld enemy) {
		yield return new WaitForSeconds(1.4f);
		if (self.currWeapon.GetComponentInChildren<ParticleSystem>(true)) {
			self.currWeapon.GetComponentInChildren<ParticleSystem>(true).gameObject.SetActive(true);
		}
		AudioSource.PlayClipAtPoint(sound,Vector3.zero);
		yield return new WaitForSeconds(0.4f);
		if (enemy) {
			Instantiate(GameManager.Instance.bloodVFX, enemy.transform.position,enemy.transform.rotation);
			enemy.animator.SetTrigger("hitReaction");	
			AudioSource.PlayClipAtPoint(grunt,Vector3.zero);

		}
	}

	protected override void CardEffect() {
		base.CardEffect();
		var damageTotal = (damage + chooser.extraDamage - target.defense);
		if (damageTotal < 0) {
			damageTotal = 0;
		}
		UiManager.Instance.StopShowEnemy();
		UiManager.Instance.ShowFeedBack($"fa {damageTotal} danni!");
		target.ModifyHealth(-damageTotal);
		if (status != null) {
			status.ApplyStatus(target,chooser,0);
		}
		chooser.transform.LookAt(target.transform, Vector3.up);
	}
}
