using UnityEngine;

public abstract class DefenseCard : GeneralCard
{
	[SerializeField] protected int percent;
	public GenericStatus status;
}