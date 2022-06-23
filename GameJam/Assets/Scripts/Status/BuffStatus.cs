using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Status/Buff",fileName = "Buff")]
public class BuffStatus : GenericStatus {

	public bool isForAttack;
	public bool isForDefense;
	
	public override void ApplyStatus(ActorWorld target, ActorWorld chooser,int bonus=0) {
		base.ApplyStatus(target,chooser,bonus);
		if (isForAttack && isForDefense) {
			target.managerStatus.SetStatus(effectTurns,(int) StatusName.AtkBouns,bonus);
			target.managerStatus.SetStatus(effectTurns,(int) StatusName.DefBonus,bonus);
		}
		else {
			if (isForAttack) {
				target.managerStatus.SetStatus(effectTurns,(int) StatusName.AtkBouns,bonus);
			}
			else {
				target.managerStatus.SetStatus(effectTurns,(int) StatusName.DefBonus,bonus);
			}


		}


	}

    
}