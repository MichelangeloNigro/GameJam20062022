using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
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
	public string uid;

	protected ActorWorld chooser;
	protected ActorWorld target;
	private void OnValidate()
	{
#if UNITY_EDITOR
		if (uid == "")
		{
			uid = GUID.Generate().ToString();
			EditorUtility.SetDirty(this);
		}
#endif
	}
	public virtual void Use(ActorWorld chooser, ActorWorld target) {
		this.chooser = chooser;
		this.target = target;
		chooser.OnCardUsed += CardEffect;
	}

	protected virtual void CardEffect() {
		chooser.OnCardUsed -= CardEffect;
	}
}