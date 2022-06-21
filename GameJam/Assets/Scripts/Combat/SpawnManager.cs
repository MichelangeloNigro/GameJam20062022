using System;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private Actor player;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Actor[] enemies;
    [SerializeField] private Transform[] spawnPoints;

    [SerializeField] private TileManager tileManager;

    private void Start() {
        SpawnPlayer();
        SpawnEnemies();
    }

    private void OnEnable() {
        tileManager.OnFinishRunning += SpawnEnemies;
    }

    private void OnDisable() {
        tileManager.OnFinishRunning -= SpawnEnemies;
    }

    private void SpawnPlayer() {
        var spawnedActor = Instantiate(player.actorPrefab, playerSpawnPoint);
        var actorWorld = spawnedActor.GetComponent<ActorWorld>();
        if (!actorWorld) {
            actorWorld = spawnedActor.AddComponent<ActorWorld>();
        }
        actorWorld.Init(player);
    }
    
    private void SpawnEnemies() {
        for (int i = 0; i < enemies.Length; i++) {
            var spawnedActor = Instantiate(enemies[i].actorPrefab, spawnPoints[i]);
            var actorWorld = spawnedActor.GetComponent<ActorWorld>();
            if (!actorWorld) {
                actorWorld = spawnedActor.AddComponent<ActorWorld>();
            }
            actorWorld.Init(enemies[i]);
        }
    }
}
