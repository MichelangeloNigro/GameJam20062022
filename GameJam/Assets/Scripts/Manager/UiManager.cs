using System;
using System.Collections;
using System.Collections.Generic;
using Riutilizzabile;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiManager : SingletonDDOL<UiManager> {
  public Image life;
  public Button currentCategory;
  public GameObject deckSelectionContent;
  public GameObject deckSelectedContent;
  public TMP_Text deckCardsNumber;
  
  public void changeCategoryCards() {
    currentCategory.interactable = true;
    deckSelectionContent.transform.Clear();
    var go = EventSystem.current.currentSelectedGameObject;
     foreach (var VARIABLE in GameManager.Instance.unlockedCards) {
       if (VARIABLE.type==go.GetComponent<CardTypeHolder>().type&& VARIABLE.quantityInDeck<=VARIABLE.quantityUnlocked) {
         var temp=GameObject.Instantiate(GameManager.Instance.cardPrefab,deckSelectionContent.transform);
         temp.GetComponent<CardReferenceHolder>().cardData = VARIABLE;
       }
     }
  }

  private void Update() {
    deckCardsNumber.text = $"{CardManager.Instance.Deck.Count}/{CardManager.Instance.maxNumberOfCard}";
  }

  public void ChangeCurrentCategory() {
    currentCategory = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();
    currentCategory.interactable = false;
  }
  private Color GetHealthBarColor(float value)
  {
    return Color.Lerp(Color.red, Color.green, Mathf.Pow(value / 1f, 2));
  }
  [ProgressBar(0, 1, ColorGetter = "GetHealthBarColor")]
  [OnValueChanged("setLifeEditor")]
  public float lifeTest;
  public void setLife(int currentLife, int totalLife, Image lifebar) {
    lifebar.fillAmount = currentLife /(float) totalLife;
    lifebar.color=Color.Lerp(Color.red, Color.green, Mathf.Pow(currentLife / 1f, 2));
  } public void setLifeEditor() {
    life.fillAmount = lifeTest /(float) 1;
    life.color=Color.Lerp(Color.red, Color.green, Mathf.Pow(lifeTest / 1f, 2));
  }
}
