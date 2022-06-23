using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Sellable : UiCardDrawer, IPointerEnterHandler,IPointerExitHandler {
  public int cost;
  //public  
  public void OnPointerEnter(PointerEventData eventData)
  {
    ToolTipManager.instace.ShowToolTip(card.cardName,card.description+" This card costs: "+cost);
  }
  public void OnClick() {
    if (GameManager.Instance.money-cost>=0) {
      GameManager.Instance.money -= cost;
      if (card==null) {
      
      }
      else {
        if (GameManager.Instance.unlockedCards.ContainsKey(card)) {
          GameManager.Instance.unlockedCards[card]++;
          if (GameManager.Instance.unlockedCards[card]==card.maximumOwned) {
            GetComponent<Button>().interactable = false;
          }
          GameManager.Instance.unlockedCards[card]=Mathf.Clamp(GameManager.Instance.unlockedCards[card], 0, card.maximumOwned);
        }
        else {
          GameManager.Instance.UnlockCard(card);
        }
      }
    }
    else {
      Debug.Log("not enough money!");
    }
  }
}
