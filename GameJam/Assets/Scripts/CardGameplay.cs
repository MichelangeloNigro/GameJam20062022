using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardGameplay : UiCardDrawer {
  
  public void OnClick() {
    CardManager.Instance.draw();
    TurnManager.Instance.PlayerActor.SelectCard(card);
    Destroy(this.gameObject);
  }
}
