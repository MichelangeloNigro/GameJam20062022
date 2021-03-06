using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiCardDrawer : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler{

   
   public GeneralCard card;
   public TMP_Text name;
   public TMP_Text description;
   public Image image;
   public RawImage border;
   private void Start() {
      switch (card.type) {
         case CardType.BuffCard:
            border.color = Color.yellow;
            break;
         case CardType.HealthCard:
            border.color = Color.green;
            break;
         case CardType.AttackCard:
            border.color = Color.red;
            break;
         case CardType.DefenceCard:
            border.color = Color.blue;
            break;
         default:
            border.color = Color.grey;
            break;
      }
      name.text = card.cardName;
      description.text = card.description;
      image.sprite = card.image;   }
   
   public void OnPointerEnter(PointerEventData eventData)
   {
      
     ToolTipManager.instace.ShowToolTip(card.cardName,card.description);
   }
   

   public void OnPointerExit(PointerEventData eventData) {
      
      // ToolTipManager.instace.OffToolTip();
   }
   
}
