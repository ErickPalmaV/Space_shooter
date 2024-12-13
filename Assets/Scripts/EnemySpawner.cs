using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool enemysPool;
    [SerializeField] private float[] spawnTimes;
    [SerializeField] private int[]  enemiesToSpawnTimes;
    [SerializeField] private Transform[] spawnPositions;
    private float _spawnTimer;
    private int _index;
    private bool _gameIsRunning;

    public void IsGameRunning(bool gameIsRunning)
    {
        _gameIsRunning = gameIsRunning;
    }

    private void Start()
    {
        _spawnTimer = 0;
        _index = 0;
    }

    private void Update()
    {
        if (_gameIsRunning)
        {
            SpawnEnemies();
            _spawnTimer += Time.deltaTime;
        }
    }
    private void SpawnEnemies()
    {
        if (_spawnTimer >= spawnTimes[_index])
        {
            if (_index < enemiesToSpawnTimes.Length)
            {
                for (int i = 0; i < enemiesToSpawnTimes[_index]; i++)
                {
                    var randomPosition = Random.Range(0, spawnPositions.Length);
                    var enemy=enemysPool.RequestGameObject();
                    enemy.transform.position = spawnPositions[randomPosition].position;
                    enemy.transform.rotation = quaternion.RotateY(180);
                }
                _index++;
            }
            if (_index >= spawnTimes.Length)
            {
                _index = 0;
                _spawnTimer = 0;
            }
        }
    }
}

