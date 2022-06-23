using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.WSA;
using Random = UnityEngine.Random;

public class ActorWorld : MonoBehaviour {
    [SerializeField, ReadOnly] private float maxHealth;
    [SerializeField, ReadOnly] private float currentHealth;
    private List<GeneralCard> deck = new();
    public List<GeneralCard> tempDeck = new();
    public List<GeneralCard> hand;
    public bool isPlayer;
    private BehaviorTree behaviorTree;
    public Image lifebar;
    public int goldDrop;
    public Transform handR;
    public GameObject currWeapon;

    public Action<ActorWorld, GeneralCard> OnCardSelected;
    public Action<ActorWorld, ActorWorld> OnTargetSelected;
    public Action<ActorWorld> OnDeath;
    public Action<ActorWorld> OnFinishDeathAnimation;
    public Action<ActorWorld> OnFinishedTurn;
    public Action OnCardUsed;

    [NonSerialized] public Animator animator;

    public StatusManager managerStatus;
    
    private int maxCardsInHand;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void PopulateHand() {
        for (int i = 0; i < maxCardsInHand; i++) {
            int temp = Random.Range(0, tempDeck.Count);
            if (isPlayer) {
                var tempGameObject = Instantiate(GameManager.Instance.cardGameplayPrefab,UiManager.Instance.handContent.transform);
                tempGameObject.GetComponent<CardGameplay>().card = tempDeck[temp]; 
            }
            hand.Add(tempDeck[temp]);
            tempDeck.Remove(tempDeck[temp]);
        }
    }

    public void Draw() {
        if (tempDeck.Count> 0 && hand.Count<=maxCardsInHand) {
            int temp = Random.Range(0, tempDeck.Count);
            if (isPlayer) {
                var tempGameObject = Instantiate(GameManager.Instance.cardGameplayPrefab,UiManager.Instance.handContent.transform);
                tempGameObject.GetComponent<CardGameplay>().card = tempDeck[temp];
            }
            hand.Add(tempDeck[temp]);
            tempDeck.Remove(tempDeck[temp]);
        }
    }
    #region Turn Related Methods
    
    public void Init(Actor actor) {
        maxHealth = actor.baseHealth;
        maxCardsInHand = actor.maxCardsInHand;
        currentHealth = actor.baseHealth;
        if (isPlayer) {
            lifebar = GameObject.FindWithTag("Player").GetComponent<Image>();
            if (CardManager.Instance.Deck.Count < actor.maxCardsInHand) {
                maxCardsInHand = CardManager.Instance.Deck.Count;
            }
            deck = CardManager.Instance.Deck;
            tempDeck.AddRange(deck);
        }
        else {
            foreach (var card in actor.deck) {
                for (int i = 0; i < card.Value; i++) {
                    tempDeck.Add(card.Key);
                }
            }
            maxHealth += TileManager.instance.wave * 2;
            currentHealth = maxHealth;
        }
        PopulateHand();
        //currentHealth = actor.baseHealth;
        behaviorTree = GetComponent<BehaviorTree>();
        TurnManager.Instance.Subscribe(this);
        TurnManager.Instance.OnTurnPassed += ExecuteBehavior;
        TurnManager.Instance.OnFinishCombat += RestockDeck;
    }



    private void OnDisable() {
        TurnManager.Instance.OnTurnPassed -= ExecuteBehavior;
        TurnManager.Instance.OnFinishCombat -= RestockDeck;
    }

    public void SelectCard(GeneralCard card) {
        OnCardSelected?.Invoke(this, card);
    }

    public void SelectTarget(ActorWorld actor) {
        OnTargetSelected(this, actor);
    }

    public void RemoveCardFromHand(GeneralCard card) {
        hand.Remove(card);
    }

    private void OnMouseDown() {
        TurnManager.Instance.PlayerActor.SelectTarget(this);        
    }

    private void ExecuteBehavior(ActorWorld actorWorld) {
        if (this == actorWorld) {
            behaviorTree.EnableBehavior();
        }
    }

    public void FinishTurn() {
        OnFinishedTurn?.Invoke(this);
        Debug.Log("finishTurn");
    }

    public void UseCard() {
        OnCardUsed?.Invoke();
    }

    private void RestockDeck() {
        if (isPlayer) {
            tempDeck.Clear();
            tempDeck.AddRange(deck);
            foreach (var card in hand) {
                tempDeck.Remove(card);
            }
        }
    }
    
    #endregion

    #region Health Related Methods

    public void ModifyHealth(float amount) {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UiManager.Instance.setLife(currentHealth,maxHealth,lifebar);
        if (currentHealth <= 0) {
            Die();
            animator.SetTrigger("Death");
        }
    }

    private void Die() {
        OnDeath?.Invoke(this);
    }

    private void FinishAnimationDie() {
        OnFinishDeathAnimation?.Invoke(this);
    }

    #endregion


    
}
