using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardGameplay : MonoBehaviour {
  public GeneralCard card;
  public void OnClick() {
    card.Use();
    Destroy(this.gameObject);
  }
}
