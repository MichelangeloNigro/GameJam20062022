using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum StatusName {
	Paralize,
	AtkBouns,
	DefBonus
}


public class StatusManager : MonoBehaviour {

	public List<Status> statusList;
	private StatusName statusName;
	public ActorWorld currentActor;
	private int extrabonus;

	public void SetStatus(int numberOfTurns,int listNumber,int bonus) {
		statusList[listNumber].isAfflicted = true;
		statusList[listNumber].numberOfTurnAfflicted = numberOfTurns;
		statusList[listNumber].Draw(currentActor);
		if (listNumber == (int) (StatusName.AtkBouns)) {
			currentActor.extraDamage = bonus;
		}
		if (listNumber == (int) (StatusName.DefBonus)) {
			extrabonus += bonus;
			currentActor.defense += extrabonus;
		}

	}

	public void ReduceTurnAfflicted() {
		foreach (var status in statusList) {
			status.numberOfTurnAfflicted -= 1;
		}
		CheckIfEndStatus();
		
	}

	public void MakeEffect() {
		for (int i = 0; i < statusList.Count; i++) {
			if (statusList[i].isAfflicted) {
				switch (i) {
					case (int)StatusName.Paralize:
						statusList[i].Paralize(currentActor);
						break;
				}
			}
		}
	
	}

	private void CheckIfEndStatus() {
		foreach (var status in statusList) {
			if (status.numberOfTurnAfflicted <= 0) {
				status.DeleteTooltip();
				status.isAfflicted = false;
				status.numberOfTurnAfflicted = 0;
				var indx=statusList.IndexOf(status);
				if (indx == (int) (StatusName.AtkBouns)) {
					currentActor.extraDamage = 0;
				}
				if (indx == (int) (StatusName.DefBonus)) {
					currentActor.defense-=extrabonus;
					extrabonus = 0;
				}
			}
		}
		
	}


	


}