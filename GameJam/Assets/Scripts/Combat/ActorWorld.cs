using System;
using BehaviorDesigner.Runtime;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActorWorld : MonoBehaviour {
    [SerializeField, ReadOnly] private float maxHealth;
    [SerializeField, ReadOnly] private float currentHealth;

    private BehaviorTree behaviorTree;

    public Action<ActorWorld, GeneralCard> OnCardSelected;
    public Action<ActorWorld, ActorWorld> OnTargetSelected;
    public Action<ActorWorld> OnDeath;
    public Action OnFinishedTurn;

    [NonSerialized] public Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    #region Turn Related Methods
    
    public void Init(Actor actor) {
        maxHealth = actor.baseHealth;
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
        if (currentHealth <= 0) {
            Die();
        }
    }

    private void Die() {
        OnDeath?.Invoke(this);
    }

    #endregion
}
