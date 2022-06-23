using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class GenericStatus : ScriptableObject {
	public Sprite sprite;
	public int effectTurns;

	public virtual void ApplyStatus(ActorWorld target, ActorWorld chooser,int bonus) {
		
	}

}
