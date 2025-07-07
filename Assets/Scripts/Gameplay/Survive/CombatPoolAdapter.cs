using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPoolAdapter : PooledBehaviour
{
    private CombatSurvive _combatSurvive;
    private EnemySpawner _spawner;

    private void Awake()
    {
        _combatSurvive = GetComponent<CombatSurvive>();
    }

    public void Initialize(EnemySpawner spawnerRef)
    {
        _spawner = spawnerRef;

        // Защита от двойной подписки
        _combatSurvive.onDeath -= HandleDeath;
        _combatSurvive.onDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        _spawner.OnEnemyKilled();
        _spawner.RecycleEnemy(this);

        LevelTimer.activeLevelTimer.AddedTimeOnSurviveMode();
    }

    public override void Activate()
    {
        gameObject.SetActive(true);
        _combatSurvive.ResetCombat();
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
