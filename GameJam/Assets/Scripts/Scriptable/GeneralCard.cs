using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType {
	BuffCard,
	HealthCard,
	AttackCard,
	DefenceCard,
}

public abstract class GeneralCard : ScriptableObject {
	public Action OnUse;
	public Action OnSelected;
	public Sprite image;
	[TextArea] public string description;
	public string cardName;
	public CardType type;
	public int maximumOwned;

	protected ActorWorld chooser;
	protected ActorWorld target;
	public virtual void Use(ActorWorld chooser, ActorWorld target) {
		this.chooser = chooser;
		this.target = target;
		chooser.OnCardUsed += CardEffect;
	}

	protected virtual void CardEffect() {
		chooser.OnCardUsed -= CardEffect;
	}
}