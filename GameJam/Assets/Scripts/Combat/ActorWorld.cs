using System;
using Sirenix.OdinInspector;
using UnityEngine;

public class ActorWorld : MonoBehaviour {
    [SerializeField, ReadOnly] private float currentHealth;

    public Action<ActorWorld> OnCardSelected;
    public Action<ActorWorld, ActorWorld> OnTargetSelected;
    public Action<ActorWorld> OnDeath;

    public void Init(Actor actor) {
        currentHealth = actor.baseHealth;
        TurnManager.Instance.Subscribe(this);
    }

    public void SelectCard() {
        OnCardSelected?.Invoke(this);
    }

    public void SelectTarget(ActorWorld actor) {
        OnTargetSelected(this, actor);
    }

    private void Die() {
        OnDeath?.Invoke(this);
    }

    private void OnMouseDown() {
        TurnManager.Instance.PlayerActor.SelectTarget(this);        
    }
}
