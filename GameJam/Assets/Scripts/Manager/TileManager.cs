using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour {
   public List<GameObject> currentTiles;
   public List<GameObject> possibleScenarios;
   private Destroyer destroyer;
   private PlayerAnimator playerAnimator;
   [SerializeField] private  float shiftXtile=25;
   [SerializeField] private float timerLerp;
   public static TileManager instance;
   public int wave;
   public Action OnFinishRunning;

   private void Start() {
      destroyer = FindObjectOfType<Destroyer>();
      playerAnimator = FindObjectOfType<PlayerAnimator>();
      destroyer.hasDestoyed += DestroyTile;
      destroyer.hasDestoyed += SpawnNewTile;
      TurnManager.Instance.OnActorSubscribed += SavePlayerAnimator;
      TurnManager.Instance.OnFinishCombat += MoveTiles;
   }

   private void Awake() {
      instance = this;
   }

   private void SavePlayerAnimator(ActorWorld actor) {
      if (actor == TurnManager.Instance.PlayerActor) {
         playerAnimator = actor.GetComponent<PlayerAnimator>();
      }
   }

   private void DestroyTile() {
      var firstTile = currentTiles[0];
      currentTiles.Remove(firstTile);
      Destroy(firstTile.gameObject);
   }

   private void SpawnNewTile() {
      var index = Random.Range(0, possibleScenarios.Count);
      var position = currentTiles[0].transform.position;
      position.x += shiftXtile;
      var newTile= Instantiate(possibleScenarios[index], position, Quaternion.identity, transform);
      currentTiles.Add(newTile);
   }



   public void MoveTiles() {
      wave += 1;
      playerAnimator.StartRunning();
      StartCoroutine(MoveTileManager());
   }


   private IEnumerator MoveTileManager() {
      var finalPosition = transform.position;
      finalPosition.x -= shiftXtile;
      var t = 0f;
      while (transform.position.x>finalPosition.x+0.05f) {
         transform.position= Vector3.Lerp(transform.position,finalPosition,t/timerLerp);
         t += Time.deltaTime;
         yield return null;
      }
      playerAnimator.StopRunning();
      OnFinishRunning?.Invoke();
   }

}
