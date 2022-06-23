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
	public string anim;

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
		Debug.Log(anim);
		this.chooser = chooser;
		this.target = target;
		chooser.OnCardUsed += CardEffect;
		if (!chooser.isPlayer) {
			string text = "nemico usa " + cardName;
			UiManager.Instance.ShowEnemyAction(text);
		}
		if (anim!="") {
			Debug.Log("anim");
			chooser.animator.SetTrigger(anim);
		}
	}

	protected virtual void CardEffect() {
		chooser.OnCardUsed -= CardEffect;
	}
}