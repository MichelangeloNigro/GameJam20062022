using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class ActorWorld : MonoBehaviour {
    [SerializeField, ReadOnly] private float maxHealth;
    [SerializeField, ReadOnly] private float currentHealth;
    public List<GeneralCard> DeckChangable=new List<GeneralCard>();
    public List<GeneralCard> Hand=new List<GeneralCard>();
    private int cardAvviable;
    private BehaviorTree behaviorTree;

    public Action<ActorWorld, GeneralCard> OnCardSelected;
    public Action<ActorWorld, ActorWorld> OnTargetSelected;
    public Action<ActorWorld> OnDeath;
    public Action OnFinishedTurn;

    [NonSerialized] public Animator animator;

    public StatusManager managerStatus;
    

    private void Awake() {
        animator = GetComponent<Animator>();
       
    }
    public void getHand() {
        for (int i = 0; i < cardAvviable; i++) {
            int temp = Random.Range(0, DeckChangable.Count);
            var tempGameObject=GameObject.Instantiate(GameManager.Instance.cardGameplayPrefab,UiManager.Instance.handContent.transform);
            tempGameObject.GetComponent<CardGameplay>().card = DeckChangable[temp];
            Hand.Add(DeckChangable[temp]);
            DeckChangable.Remove(DeckChangable[temp]);
        }
    }

    public void draw() {
        if (DeckChangable.Count>0) {
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
        foreach (var VARIABLE in actor.deck) {
            DeckChangable.Add(VARIABLE);
        }
        cardAvviable = actor.cardInHand;
        currentHealth = actor.baseHealth;
        behaviorTree = GetComponent<BehaviorTree>();
        TurnManager.Instance.Subscribe(this);
        TurnManager.Instance.OnTurnPassed += ExecuteBehavior;
        getHand();
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
        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        OnDeath?.Invoke(this);
    }

    #endregion


    
}
