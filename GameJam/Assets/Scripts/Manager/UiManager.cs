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
  public GameObject handContent;
  public TMP_Text deckCardsNumber;
  public TMP_Text deckCardsRemaning;
  public GameObject gameplayUi;
  public GameObject deckBuild;
  public GameObject doneButton;
  public Action onFinishDeck;
 

  private void Start() {
    
    
  }

  public void changeCategoryCards() {
    currentCategory.interactable = true;
    deckSelectionContent.transform.Clear();
    var go = EventSystem.current.currentSelectedGameObject;
     foreach (var VARIABLE in GameManager.Instance.unlockedCards) {
       if (VARIABLE.type==go.GetComponent<CardTypeHolder>().type&& VARIABLE.quantityInDeck<=VARIABLE.quantityUnlocked) {
         var temp=GameObject.Instantiate(GameManager.Instance.cardPrefab,deckSelectionContent.transform);
         temp.GetComponent<CardReferenceHolder>().card = VARIABLE;
       }
     }
  }

  public void DoneWithDeckBuild() {
    gameplayUi.SetActive(true);
    deckBuild.SetActive(false);
    onFinishDeck?.Invoke();
    TurnManager.Instance.InitBattle();
    foreach (var VARIABLE in CardManager.Instance.Deck) {
      CardManager.Instance.DeckChangable.Add(VARIABLE);
    }
    CardManager.Instance.getHand();
  }
  private void Update() {
    if (CardManager.Instance.Deck.Count <= 0) {
      doneButton.SetActive(false);
    }
    else {
      doneButton.SetActive(true);
    }
      deckCardsNumber.text = $"{CardManager.Instance.Deck.Count}/{CardManager.Instance.maxNumberOfCard}";
    deckCardsRemaning.text = $"{CardManager.Instance.DeckChangable.Count}/{CardManager.Instance.Deck.Count}";
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
