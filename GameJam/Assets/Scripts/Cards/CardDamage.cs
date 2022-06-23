using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card/AttackCard",fileName = "Damage_")]
public class CardDamage : AttackCard
{
	public override void Use(ActorWorld chooser, ActorWorld target) {
		base.Use(chooser, target);
						
		chooser.animator.SetTrigger("shooting");
		Destroy(chooser.currWeapon);
		chooser.currWeapon=Instantiate(weaponModel, chooser.handR);
		GameManager.Instance.StartCoroutine(spawnVfx(chooser,target));
	}

	public IEnumerator spawnVfx(ActorWorld self, ActorWorld enemy) {
		yield return new WaitForSeconds(1.4f);
		Debug.Log(self.currWeapon);
		if (self.currWeapon.GetComponentInChildren<ParticleSystem>(true)) {
			self.currWeapon.GetComponentInChildren<ParticleSystem>(true).gameObject.SetActive(true);
		}
		AudioSource.PlayClipAtPoint(sound,Vector3.zero);
		yield return new WaitForSeconds(0.4f);
		if (enemy) {
			Instantiate(GameManager.Instance.bloodVFX, enemy.transform.position,enemy.transform.rotation);
		}
	}

	protected override void CardEffect() {
		base.CardEffect();
		target.ModifyHealth(-damage);
		chooser.transform.LookAt(target.transform, Vector3.up);
		Debug.Log("damage"+damage);
	}
}
