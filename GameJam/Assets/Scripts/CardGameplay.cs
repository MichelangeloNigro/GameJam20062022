using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardGameplay : UiCardDrawer {
  
  public void OnClick() {
    //CardManager.Instance.draw();
    //TurnManager.Instance.PlayerActor.Draw();
    if (TurnManager.Instance.currentActor == TurnManager.Instance.PlayerActor && TurnManager.Instance.turnPhase == TurnManager.TurnPhase.CardSelection) {
      TurnManager.Instance.PlayerActor.SelectCard(card);
      TurnManager.Instance.cardUI = gameObject;
    }
    //Destroy(this.gameObject);
  }
}
