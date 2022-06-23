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
  public TMP_Text lifeRemain;
  public TMP_Text goldUi;
  public TMP_Text feedbackText;

  public IEnumerator uiGoldOn(int i) {
    goldUi.text = $"+{i}";
    goldUi.gameObject.transform.parent.gameObject.SetActive(true);
    yield return new WaitForSeconds(0.5f);
    goldUi.gameObject.transform.parent.gameObject.SetActive(false);
  }
  private void Start() {
    
      SetInitialCategory();
  }

  public void changeCategoryCards() { 
    currentCategory.interactable = true;
    deckSelectionContent.transform.Clear();
    var go = EventSystem.current.currentSelectedGameObject;
    foreach (var cardQuantity in GameManager.Instance.unlockedCards) {
      if (cardQuantity.Key.type == go.GetComponent<CardTypeHolder>().type && GameManager.Instance.unlockedCards[cardQuantity.Key] <= cardQuantity.Value) {
        var temp = Instantiate(GameManager.Instance.cardPrefab,deckSelectionContent.transform);
        temp.GetComponent<CardReferenceHolder>().card = cardQuantity.Key;
      }
    }
  }

  private void SetInitialCategory() {
    currentCategory.interactable = false;
    deckSelectionContent.transform.Clear();
    var go = currentCategory;
    foreach (var cardQuantity in GameManager.Instance.unlockedCards) {
      if (cardQuantity.Key.type == go.GetComponent<CardTypeHolder>().type && GameManager.Instance.unlockedCards[cardQuantity.Key] <= cardQuantity.Value) {
        var temp = Instantiate(GameManager.Instance.cardPrefab,deckSelectionContent.transform);
        temp.GetComponent<CardReferenceHolder>().card = cardQuantity.Key;
      }
    }
  }

  public void DoneWithDeckBuild() {
    gameplayUi.SetActive(true);
    deckBuild.SetActive(false);
    onFinishDeck?.Invoke();
    TurnManager.Instance.InitBattle();
    foreach (var card in CardManager.Instance.Deck) {
      CardManager.Instance.DeckChangable.Add(card);
    }
    //CardManager.Instance.getHand();
  }
  private void Update() {
    if (CardManager.Instance.Deck!=null &&CardManager.Instance.Deck.Count <= 0) {
      doneButton.SetActive(false);
    }
    else {
      doneButton.SetActive(true);
    }
    if (CardManager.Instance.Deck!=null) {
      deckCardsNumber.text = $"{CardManager.Instance.Deck.Count}/{CardManager.Instance.maxNumberOfCard}";
      if (TurnManager.Instance.PlayerActor) {
        deckCardsRemaning.text = $"{TurnManager.Instance.PlayerActor.tempDeck.Count}/{CardManager.Instance.Deck.Count}";
      }
    }
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
  
  public void setLife(float currentLife, float totalLife, Image lifebar) {
    lifebar.fillAmount = currentLife / totalLife;
    lifebar.color=Color.Lerp(Color.red, Color.green, Mathf.Pow(currentLife /totalLife, 2));
    if (lifebar == life) { 
    lifeRemain.text = currentLife + " / " + totalLife;
    }
  } 
  public void setLifeEditor() {
    life.fillAmount = lifeTest /(float) 1;
    life.color=Color.Lerp(Color.red, Color.green, Mathf.Pow(lifeTest / 1f, 2));
  }

  public void ShowFeedBack(string text) {
    
    feedbackText.gameObject.SetActive(true);
    feedbackText.text = text;
  }

  public void StopShow() {
    feedbackText.gameObject.SetActive(false);
  }


}
