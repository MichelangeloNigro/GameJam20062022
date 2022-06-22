using UnityEngine;

public abstract class HealthCard : GeneralCard {
	[SerializeField] public int amount;
	public bool canUnstunnedActor;
}
