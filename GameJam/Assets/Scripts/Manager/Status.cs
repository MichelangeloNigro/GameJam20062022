using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Status {
	public bool isAfflicted;
	public int numberOfTurnAfflicted;
	public string name;
	public string description;
	public Sprite sprite;
	public GameObject toolTip;

	public void Draw(ActorWorld target) {
		toolTip=GameObject.Instantiate(UiManager.Instance.statusObj, target.statusContent.transform);
		toolTip.GetComponent<Image>().sprite = sprite;
		toolTip.GetComponent<statusTooltip>().statsGeneric = this;
	}

	public void DeleteTooltip() {
		GameObject.Destroy(toolTip);
	}
	public void Paralize(ActorWorld currentActor) {
		currentActor.FinishTurn();
	}


	


}