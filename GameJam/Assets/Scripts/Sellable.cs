using UnityEngine;
using UnityEngine.UI;

public class Sellable : UiCardDrawer {
  public int cost;

  public void OnClick() {
    if (GameManager.Instance.money-cost<=0) {
      GameManager.Instance.money -= cost;
      if (card==null) {
      
      }
      else {
        if (GameManager.Instance.unlockedCards.Contains(card)) {
          card.quantityUnlocked++;
          if (card.quantityUnlocked==card.maximumOwned) {
            GetComponent<Button>().interactable = false;
          }
          card.quantityUnlocked=Mathf.Clamp(card.quantityUnlocked, 0, card.maximumOwned);
        }
        else {
          GameManager.Instance.unlockedCards.Add(card);
        }
      }
    }
    else {
      Debug.Log("not enough money!");
    }
  }
}
