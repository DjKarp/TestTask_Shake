using System.Collections.Generic;
using UnityEngine;

public class BombPickup : Pickup, IPickable
{
    [SerializeField] private float _radius = 10f;

    private void Awake()
    {
        Pickable = this;
    }

    public void OnPicked(GameObject picker)
    {
        List<CombatSurvive> combatSurvives = new (EnemySpawner.Instance.EnemyList);

        foreach (CombatSurvive combat in combatSurvives)
        {
            if (combat != null)
            {
                if (Vector3.Distance(combat.gameObject.transform.position, transform.position) < _radius)
                {
                    combat.Hurt(9999, 3, combat.headPoint.position);
                }
            }
        }

        SpawnLootService.Instance.RecycleLoot(this);
    }
}
