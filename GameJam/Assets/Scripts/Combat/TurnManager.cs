using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TurnManager : MonoBehaviour {
    private enum TurnPhase {
        NotInBattle,
        CardSelection,
        TargetSelection
    }

    public static TurnManager Instance;

    [SerializeField, ReadOnly] private TurnPhase turnPhase;
    [SerializeField] private BattleManager battleManager;
    [SerializeField] private TileManager tileManager;

    public Action<ActorWorld> OnActorSubscribed;
    public Action<GeneralCard> OnCardSuccessfullySelected;
    public Action<ActorWorld, ActorWorld> OnTargetSuccessfullySelected;
    public Action<ActorWorld> OnTurnPassed;
    public Action OnFinishCombat;

    private List<ActorWorld> actors = new();
    
    private List<ActorWorld> enemies = new();
    public List<ActorWorld> Enemies => enemies;

    private ActorWorld currentActor;
    [SerializeField, ReadOnly] private int currentIndex;
    
    private ActorWorld playerActor;
    public ActorWorld PlayerActor => playerActor;
    private int goldRecived;

    public GeneralCard selectedCard;
    public GameObject cardUI;

    private void Awake() {
        Instance = this;
        battleManager.Init();
    }

    private void OnEnable() {
        tileManager.OnFinishRunning += StartBattle;
    }

    private void OnDisable() {
        tileManager.OnFinishRunning -= StartBattle;
    }

    private void Start() {
       
    }

    public void InitBattle() {
        
        Invoke(nameof(StartBattle), 1);
    }

    public void Subscribe(ActorWorld actor) {
        if (actors.Count == 0) {
            playerActor = actor;
        }
        else {
            enemies.Add(actor);
        }
        actors.Add(actor);
        actor.OnCardSelected += OnCardSelected;
        actor.OnTargetSelected += OnTargetSelected;
        actor.OnDeath += OnActorDeath;
        actor.OnFinishedTurn += PassTurn;
        OnActorSubscribed?.Invoke(actor);
    }
    
    private void StartBattle() {
        goldRecived = enemies.Count;
        currentActor = actors[0];
        currentIndex = 0;
        turnPhase = TurnPhase.CardSelection;
    }

    private void OnCardSelected(ActorWorld actor, GeneralCard card) {
        if (turnPhase == TurnPhase.CardSelection && actor == currentActor) {
            turnPhase = TurnPhase.TargetSelection;
            selectedCard = card;
            OnCardSuccessfullySelected?.Invoke(card);
        }
    }

    private void OnTargetSelected(ActorWorld chooser, ActorWorld target) {
        if (turnPhase == TurnPhase.TargetSelection && chooser == currentActor) {
            OnTargetSuccessfullySelected?.Invoke(chooser, target);
            chooser.RemoveCardFromHand(selectedCard);
            if (cardUI!=null) {
                Destroy(cardUI);
            }
        }
        else {
            Debug.Log("wrong");
        }
    }

    public void PlayerEndTurn() {
        PassTurn(playerActor);
    }

    public void PassTurn(ActorWorld chooser) {
        if (chooser != currentActor) {
            return;
        }
        
        if (enemies.Count == 0) {
            OnFinishCombat?.Invoke();
        }
        
        currentIndex = ExtensionMethods.Cycle(currentIndex + 1, 0, actors.Count);
        currentActor = actors[currentIndex];
        turnPhase = TurnPhase.CardSelection;
        currentActor.Draw();
        if (currentActor != playerActor) {
            OnTurnPassed?.Invoke(currentActor);
        }
    }

    private void OnActorDeath(ActorWorld actor) {
        int actorIndex = actors.IndexOf(actor);
        if (currentIndex >= actorIndex) {
            currentIndex = Mathf.Clamp(currentIndex - 1, 0, actors.Count - 1);
        }
        actor.OnCardSelected -= OnCardSelected;
        actor.OnTargetSelected -= OnTargetSelected;
        actor.OnDeath -= OnActorDeath;
        actors.Remove(actor);
        if (actor != playerActor) {
            enemies.Remove(actor);
            Destroy(actor.gameObject);
        }
        else {
            GameManager.Instance.AddGold(goldRecived);
            goldRecived = 0;
            StartCoroutine(FadeManager.Instance.ReloadScene());
        }
    }
}
