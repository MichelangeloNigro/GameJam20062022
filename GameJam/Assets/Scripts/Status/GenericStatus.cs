using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class GenericStatus : ScriptableObject {
	public int effectTurns;


	public virtual void ApplyStatus(ActorWorld target, ActorWorld chooser,int bonus) {
		
	}

}
