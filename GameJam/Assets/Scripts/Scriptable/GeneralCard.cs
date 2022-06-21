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
	[NonSerialized] public int quantityInDeck;
	public int quantityUnlocked;

	public virtual void Use(ActorWorld chooser, ActorWorld target) {
		chooser.GetComponent<Animator>();
	}

}