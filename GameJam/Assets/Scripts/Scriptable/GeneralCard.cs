using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum CardType {
	BuffCArd,
	HealthCard,
	AttackCard,
	DefenceCard,
}
public abstract class GeneralCard : ScriptableObject {
	public Action OnUse;
	public Action OnSelected;
	private Sprite image;
	private Text description;
	private Text cardName;
	private CardType type;

	public abstract void Use();

}
