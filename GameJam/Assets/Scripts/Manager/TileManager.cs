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

   private void Start() {
      destroyer = FindObjectOfType<Destroyer>();
      playerAnimator = FindObjectOfType<PlayerAnimator>();
      destroyer.hasDestoyed += DestroyTile;
      destroyer.hasDestoyed += SpawnNewTile;

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

   }

}
