using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UiManager : MonoBehaviour {
  public Image life;
  public Button currentCategory;
  public GameObject deckSelectionContent;
  
  public void changeCategoryCards() {
    currentCategory.interactable = true;
    deckSelectionContent.transform.Clear();
    var go = EventSystem.current.currentSelectedGameObject;
    // foreach (var VARIABLE in GameManager.Instance.unlockedCards) {
    //   if (VARIABLE) {
    //     var temp=GameObject.Instantiate(GameManager.Instance.cardPrefab,deckSelectionContent.transform);
    //     //temp.type == VARIABLE;
    //   }
    // }
    //spawncards

  }

  public void OnCardClick() {
    //check quantity
    //add to deck
    //update deck number
  }

  public void OnCardBack() {
    //set back in selectable card
    //remove from deck
    //update deck number
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
