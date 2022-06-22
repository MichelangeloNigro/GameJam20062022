using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ScriptableObjects/Status/Paralize",fileName = "Paralize_")]
public class ParalizeStatus : GenericStatus
{
	public override void ApplyStatus(ActorWorld target, ActorWorld chooser) {
		target.managerStatus.SetStatus(effectTurns,(int) StatusName.Paralize);
	}

    
}