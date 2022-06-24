using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Card/HealCard",fileName = "Heal_")]
public class CardHeal : HealthCard
{
	public override void Use(ActorWorld chooser, ActorWorld target) {
		base.Use(chooser, target);
		AudioSource.PlayClipAtPoint(sound,Vector3.zero);
	}

	protected override void CardEffect() {
		base.CardEffect();
		target.ModifyHealth(amount);
		UiManager.Instance.StopShowEnemy();
		UiManager.Instance.ShowFeedBack($"si cure per {amount} punti vita!");
		chooser.transform.LookAt(target.transform, Vector3.up);
		Debug.Log("Heal");
	}
}
