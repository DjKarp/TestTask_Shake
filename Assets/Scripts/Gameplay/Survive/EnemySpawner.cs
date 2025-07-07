using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private CombatPoolAdapter _enemyPrefab;
    [SerializeField] private int _initialEnemyCount = 3;
    [SerializeField] private int _maxEnemiesPerWave = 99;
    [SerializeField] private float _difficultyMultiplier = 1.3f;
    [SerializeField] private LayerMask _obstacleMask;

    [Header("Spawn Area")]    
    [SerializeField] private float _spawnAreaRadius = 10f;
    private Transform _centerPoint;
    private int _maxTries = 200;

    private Pool<CombatPoolAdapter> _enemyPool;
    private int _currentWave = 0;
    private int _aliveEnemies = 0;

    private void Awake()
    {
        _centerPoint = this.transform;
    }

    private void Start()
    {
        // Создаём пул
        _enemyPool = new Pool<CombatPoolAdapter>
        {
            prefab = _enemyPrefab,
            parent = this.transform
        };

        StartNextWave();
    }

    private void StartNextWave()
    {
        _currentWave++;

        //int enemiesToSpawn = _initialEnemyCount + _currentWave;
        int enemiesToSpawn = Mathf.Min(Mathf.RoundToInt(_initialEnemyCount * Mathf.Pow(_difficultyMultiplier, _currentWave - 1)), _maxEnemiesPerWave);

        _aliveEnemies = enemiesToSpawn;

        for (int i = 0; i < enemiesToSpawn; i++)
            SpawnEnemy();
    }

    private void SpawnEnemy()
    {
        CombatPoolAdapter enemy = _enemyPool.GetOrCreate();
        enemy.Initialize(this); // Подключаемся к Combat.onDeath

        Vector3 spawnPos = GetValidSpawnPosition();
        enemy.transform.position = spawnPos;
    }

    private Vector3 GetValidSpawnPosition()
    {
        for (int i = 0; i < _maxTries; i++)
        {            
            Vector3 randomPos = _centerPoint.position + Random.insideUnitSphere * _spawnAreaRadius;
            randomPos.y = _centerPoint.position.y;

            // Проверка на коллизию с препятствием
            if (!Physics.CheckSphere(randomPos, 1f, _obstacleMask))
            {
                return randomPos;
            }
        }

        return _centerPoint.position;
    }

    public void OnEnemyKilled()
    {
        _aliveEnemies--;

        if (_aliveEnemies <= 0)
        {
            StartCoroutine(DelayNextWave());
        }
    }

    public void RecycleEnemy(CombatPoolAdapter enemy)
    {
        _enemyPool.Recycle(enemy);
    }

    private IEnumerator DelayNextWave()
    {
        yield return new WaitForSeconds(2f);
        StartNextWave();
    }
}
