using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class TileManager : MonoBehaviour {
   public List<GameObject> currentTiles;
   public List<GameObject> possibleScenarios;
   private Destroyer destroyer;
   [SerializeField] private  float shiftXtile=25;

   private void Start() {
      destroyer = FindObjectOfType<Destroyer>();
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

}
