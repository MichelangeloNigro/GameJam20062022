using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour {
    [SerializeField] private Actor player;
    [SerializeField] private Transform playerSpawnPoint;
    [SerializeField] private Actor[] enemies;
    [SerializeField] private Transform[] spawnPoints;
    
    private UiManager uiManager;
    [SerializeField] private TileManager tileManager;

    private void Start() {
        uiManager = FindObjectOfType<UiManager>();
        uiManager.onFinishDeck += SpawnPlayer;
        uiManager.onFinishDeck += SpawnEnemies;
       
        //SpawnPlayer();
        //SpawnEnemies();
    }

    private void OnEnable() {
        tileManager.OnFinishRunning += SpawnEnemies;
    }

    private void OnDisable() {
        tileManager.OnFinishRunning -= SpawnEnemies;
    }

    public void SpawnPlayer() {
        var spawnedActor = Instantiate(player.actorPrefab, playerSpawnPoint);
        var actorWorld = spawnedActor.GetComponent<ActorWorld>();
        if (!actorWorld) {
            actorWorld = spawnedActor.AddComponent<ActorWorld>();
        }
        actorWorld.Init(player);
    }
    
    public void SpawnEnemies() {
        int numOfEnemies = Random.Range(1, spawnPoints.Length + 1);
        for (int i = 0; i < 3; i++) {
            var ranIndex = Random.Range(0, enemies.Length);
            var enemy = enemies[ranIndex].actorPrefab;
            var spawnedActor = Instantiate(enemy, spawnPoints[i]);
            var actorWorld = spawnedActor.GetComponent<ActorWorld>();
            if (!actorWorld) {
                actorWorld = spawnedActor.AddComponent<ActorWorld>();
            }
            actorWorld.Init(enemies[ranIndex]);
        }
    }
}
