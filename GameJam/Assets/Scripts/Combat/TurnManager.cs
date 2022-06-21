using System;
using System.Collections.Generic;
using Riutilizzabile;
using Sirenix.OdinInspector;
using UnityEngine;

public class TurnManager : SingletonDDOL<TurnManager> {
    private enum TurnPhase {
        NotInBattle,
        CardSelection,
        TargetSelection
    }

    [SerializeField, ReadOnly] private TurnPhase turnPhase;

    private Action<ActorWorld> OnActorSubscribed;
    private Action OnCardSuccessfullySelected;
    private Action<ActorWorld> OnTargetSuccessfullySelected;

    private List<ActorWorld> actors = new();
    
    private ActorWorld currentActor;
    private int currentIndex;
    
    private ActorWorld playerActor;
    public ActorWorld PlayerActor => playerActor;

    private void Start() {
        Invoke(nameof(StartBattle), 1);
    }

    public void Subscribe(ActorWorld actor) {
        if (actors.Count == 0) {
            playerActor = actor;
        }
        actors.Add(actor);
        actor.OnCardSelected += OnCardSelected;
        actor.OnTargetSelected += OnTargetSelected;
        actor.OnDeath += OnActorDeath;
        OnActorSubscribed?.Invoke(actor);
    }
    
    private void StartBattle() {
        currentActor = actors[0];
        currentIndex = 0;
        turnPhase = TurnPhase.CardSelection;
    }

    private void OnCardSelected(ActorWorld actor) {
        if (turnPhase == TurnPhase.CardSelection && actor == currentActor) {
            turnPhase = TurnPhase.TargetSelection;
            OnCardSuccessfullySelected?.Invoke();
        }
    }

    private void OnTargetSelected(ActorWorld chooser, ActorWorld target) {
        if (turnPhase == TurnPhase.TargetSelection && chooser == currentActor) {
            OnTargetSuccessfullySelected?.Invoke(target);
            PassTurn();
        }
    }

    private void PassTurn() {
        currentIndex = ExtensionMethods.Cycle(currentIndex + 1, 0, actors.Count);
        currentActor = actors[currentIndex];
        turnPhase = TurnPhase.CardSelection;
    }

    private void OnActorDeath(ActorWorld actor) {
        int actorIndex = actors.IndexOf(actor);
        if (currentIndex >= actorIndex) {
            currentIndex = Mathf.Clamp(currentIndex - 1, 0, actors.Count - 1);
        }
        actors.Remove(actor);
    }
}