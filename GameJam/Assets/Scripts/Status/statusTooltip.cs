using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class statusTooltip : MonoBehaviour,IPointerEnterHandler {
	public Status statsGeneric;
	public void OnPointerEnter(PointerEventData eventData) {
		if (statsGeneric!=null) {
			ToolTipManager.instace.ShowToolTip(statsGeneric.name,statsGeneric.description + "dura ancora per:" + statsGeneric.numberOfTurnAfflicted);
		}
	}
}
