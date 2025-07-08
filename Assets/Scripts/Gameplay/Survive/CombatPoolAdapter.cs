using UnityEngine;

public class CombatPoolAdapter : PooledBehaviour
{
    public CombatSurvive CombatSurvive;
    private EnemySpawner _spawner;

    private void Awake()
    {
        CombatSurvive = GetComponent<CombatSurvive>();
    }

    public void Initialize(EnemySpawner spawnerRef)
    {
        _spawner = spawnerRef;

        // Защита от двойной подписки
        CombatSurvive.onDeath -= HandleDeath;
        CombatSurvive.onDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        // Добавляем время. А время будет ограничено, чтобы игрок не стоял на месте и был стимул бегать и искать врага.
        LevelTimer.activeLevelTimer.AddedTimeOnSurviveMode();

        // Добавляем собираемые объекты
        SpawnLootService.Instance.SpawnLoot(this.transform.position);

        _spawner.OnEnemyKilled();
        _spawner.RecycleEnemy(this);        
    }

    public override void Activate()
    {
        gameObject.SetActive(true);
        CombatSurvive.ResetCombat();
    }

    public override void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public override void Reset()
    {
        transform.position = Vector3.zero;
    }
}
