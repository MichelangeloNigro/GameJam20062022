using System;
using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ActorWorld : MonoBehaviour,IPointerEnterHandler, IPointerExitHandler {
	[SerializeField, ReadOnly] private float maxHealth;
	[SerializeField, ReadOnly] private float currentHealth;
	public float MaxHealth => maxHealth;
	public float CurrentHealth => currentHealth;
	private List<GeneralCard> deck = new();
	public List<GeneralCard> tempDeck = new();
	public List<GeneralCard> hand;
	public bool isPlayer;
	private BehaviorTree behaviorTree;
	public Image lifebar;
	public Image arrowTurn;
	public int goldDrop;
	public Transform handR;
	public GameObject currWeapon;
	public int defense;
	public int extraDamage;
	public Action<ActorWorld, GeneralCard> OnCardSelected;
	public Action<ActorWorld, ActorWorld> OnTargetSelected;
	public Action<ActorWorld> OnDeath;
	public Action<ActorWorld> OnFinishDeathAnimation;
	public Action<ActorWorld> OnFinishedTurn;
	public Action OnCardUsed;
	public GameObject statusContent;
	[SerializeField] private float lifeModifier=2;

	[NonSerialized] public Animator animator;

	public StatusManager managerStatus;

	private int maxCardsInHand;

	private void Awake() {
		animator = GetComponent<Animator>();
	}

	private void PopulateHand() {
		hand.Clear();
		for (int i = 0; i < maxCardsInHand; i++) {
			int temp = Random.Range(0, tempDeck.Count);
			if (isPlayer) {
				var tempGameObject = Instantiate(GameManager.Instance.cardGameplayPrefab, UiManager.Instance.handContent.transform);
				tempGameObject.GetComponent<CardGameplay>().card = tempDeck[temp];
			}
			hand.Add(tempDeck[temp]);
			tempDeck.Remove(tempDeck[temp]);
		}
	}

	public void Draw() {
		if (tempDeck.Count > 0 && hand.Count <= maxCardsInHand) {
			int temp = Random.Range(0, tempDeck.Count);
			if (isPlayer) {
				var tempGameObject = Instantiate(GameManager.Instance.cardGameplayPrefab, UiManager.Instance.handContent.transform);
				tempGameObject.GetComponent<CardGameplay>().card = tempDeck[temp];
			}
			hand.Add(tempDeck[temp]);
			tempDeck.Remove(tempDeck[temp]);
		}
	}

	#region Turn Related Methods

	public void Init(Actor actor) {
		GetComponentInChildren<Canvas>(true).worldCamera=Camera.main;
		maxHealth = actor.baseHealth;
		maxCardsInHand = actor.maxCardsInHand;
		currentHealth = actor.baseHealth;
		if (isPlayer) {
			lifebar = GameObject.FindWithTag("Player").GetComponent<Image>();
			statusContent = GameObject.FindWithTag("status");
			if (CardManager.Instance.Deck.Count < actor.maxCardsInHand) {
				maxCardsInHand = CardManager.Instance.Deck.Count;
			}
			deck = CardManager.Instance.Deck;
			tempDeck.AddRange(deck);
		}
		else {
			var actorDeck = actor.deck[0];
			if (TileManager.instance.wave > 3) {
				actorDeck = actor.deck[1];
			}
			foreach (var card in actorDeck) {
				for (int i = 0; i < card.Value; i++) {
					tempDeck.Add(card.Key);
				}
			}
			var  value = (int) Random.Range(0, TileManager.instance.wave * lifeModifier+1);
			maxHealth += value;
			currentHealth = maxHealth;
		}
		PopulateHand();
		//currentHealth = actor.baseHealth;
		behaviorTree = GetComponent<BehaviorTree>();
		TurnManager.Instance.Subscribe(this);
		TurnManager.Instance.OnTurnPassed += ExecuteBehavior;
		TurnManager.Instance.OnFinishCombat += RestockDeck;
		TurnManager.Instance.OnFinishCombat += PopulateHand;
	}

	private void OnDisable() {
		TurnManager.Instance.OnTurnPassed -= ExecuteBehavior;
		TurnManager.Instance.OnFinishCombat -= RestockDeck;
		TurnManager.Instance.OnFinishCombat -= PopulateHand;
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
			UiManager.Instance.handContent.transform.Clear();
			// foreach (var card in hand) {
			//     tempDeck.Remove(card);
			// }
		}
	}

	#endregion

	#region Health Related Methods

	public void ModifyHealth(float amount) {
		var temp = currentHealth;
		currentHealth += amount;
		currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
		UiManager.Instance.setLife(currentHealth, temp, lifebar);
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

	public void OnPointerEnter(PointerEventData eventData) {
		Debug.Log("hi");
		if (TurnManager.Instance.turnPhase==TurnManager.TurnPhase.TargetSelection) {
			GetComponentInChildren<Outline>().enabled = true;
		}
	}

	public void OnPointerExit(PointerEventData eventData) {
		Debug.Log("bye");
		if ( TurnManager.Instance.turnPhase==TurnManager.TurnPhase.TargetSelection) {
			GetComponentInChildren<Outline>().enabled = false;
		}
	}
}