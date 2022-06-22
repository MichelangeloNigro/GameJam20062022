using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatusName {
	Paralize,
	DownDefense,
	LoseHealt
}


public class StatusManager : MonoBehaviour {

	public List<Status> statusList;
	private StatusName statusName;
	public ActorWorld currentActor;

	public void SetStatus(int numberOfTurns,int listNumber) {
		statusList[listNumber].isAfflicted = true;
		statusList[listNumber].numberOfTurnAfflicted = numberOfTurns;
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
					case (int) StatusName.DownDefense:
						break;
					case (int)StatusName.LoseHealt:
						break;
				}
			}
		}
	
	}

	private void CheckIfEndStatus() {
		foreach (var status in statusList) {
			if (status.numberOfTurnAfflicted <= 0) {
				status.isAfflicted = false;
				status.numberOfTurnAfflicted = 0;
			}
		}
		
	}


	


}