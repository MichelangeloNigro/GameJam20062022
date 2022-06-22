using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ActorWorld : MonoBehaviour {
    [SerializeField, ReadOnly] private float maxHealth;
    [SerializeField, ReadOnly] private float currentHealth;
    public List<GeneralCard> DeckChangable=new List<GeneralCard>();
    public List<GeneralCard> Hand=new List<GeneralCard>();
    public bool isPlayer;
    private int cardAvviable;
    private BehaviorTree behaviorTree;
    public Image lifebar;

    public Action<ActorWorld, GeneralCard> OnCardSelected;
    public Action<ActorWorld, ActorWorld> OnTargetSelected;
    public Action<ActorWorld> OnDeath;
    public Action OnFinishedTurn;

    [NonSerialized] public Animator animator;

    public StatusManager managerStatus;
    

    private void Awake() {
        animator = GetComponent<Animator>();
       
    }
    public void GetHand() {
        for (int i = 0; i < cardAvviable; i++) {
            int temp = Random.Range(0, DeckChangable.Count);
            if (isPlayer) {
                var tempGameObject = Instantiate(GameManager.Instance.cardGameplayPrefab,UiManager.Instance.handContent.transform);
                tempGameObject.GetComponent<CardGameplay>().card = DeckChangable[temp]; 
            }
            Hand.Add(DeckChangable[temp]);
            DeckChangable.Remove(DeckChangable[temp]);
        }
    }

    public void Draw() {
        if (DeckChangable.Count>0&& Hand.Count<=cardAvviable) {
            int temp = Random.Range(0, DeckChangable.Count);
            var tempGameObject=GameObject.Instantiate(GameManager.Instance.cardGameplayPrefab,UiManager.Instance.handContent.transform);
            tempGameObject.GetComponent<CardGameplay>().card = DeckChangable[temp];
            Hand.Add(DeckChangable[temp]);
            DeckChangable.Remove(DeckChangable[temp]);
        }
    }
    #region Turn Related Methods
    
    public void Init(Actor actor) {
        maxHealth = actor.baseHealth;
        cardAvviable = actor.cardInHand;
        if (isPlayer) {
            lifebar = GameObject.FindWithTag("Player").GetComponent<Image>();
            if (CardManager.Instance.Deck.Count < actor.cardInHand) {
                cardAvviable = CardManager.Instance.Deck.Count;
            }
            foreach (var card in CardManager.Instance.Deck) {
                DeckChangable.Add(card);
            }
        }
        else {
            foreach (var card in actor.deck) {
                DeckChangable.Add(card);
            }
            
        }
        GetHand();
        currentHealth = actor.baseHealth;
        behaviorTree = GetComponent<BehaviorTree>();
        TurnManager.Instance.Subscribe(this);
        TurnManager.Instance.OnTurnPassed += ExecuteBehavior;
        
    }

    private void OnDisable() {
        TurnManager.Instance.OnTurnPassed -= ExecuteBehavior;
    }

    public void SelectCard(GeneralCard card) {
        OnCardSelected?.Invoke(this, card);
    }

    public void SelectTarget(ActorWorld actor) {
        OnTargetSelected(this, actor);
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
        OnFinishedTurn?.Invoke();
    }
    
    #endregion

    #region Health Related Methods

    public void ModifyHealth(float amount) {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UiManager.Instance.setLife(currentHealth,maxHealth,lifebar);
        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        OnDeath?.Invoke(this);
    }

    #endregion


    
}
