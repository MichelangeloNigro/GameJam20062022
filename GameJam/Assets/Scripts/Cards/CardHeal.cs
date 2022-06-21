using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Card/HealCard",fileName = "Heal_")]
public class CardHeal : HealthCard
{
	public override void Use() {
		base.Use();
		Debug.Log($"heal for {amount}");
	}
}
