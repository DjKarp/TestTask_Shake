using System.Collections.Generic;
using UnityEngine;

public class SpawnLootService : MonoBehaviour
{
    public static SpawnLootService Instance { get; private set; }

    [Header("Config")]
    [SerializeField] private List<PickupEntry> _pickupEntries;
    [SerializeField] private float _bonusDropChance = 0.2f; // 0.2 -> 20% шанс
    [SerializeField] private float _dropRadius = 3.5f;

    private readonly Dictionary<PickableType.TypePickable, Pool<Pickup>> pools = new();


    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);

        foreach (var entry in _pickupEntries)
        {
            if (!pools.ContainsKey(entry.TypePickable))
            {
                CreatePoolPickupEntry(entry);
            }
        }
    }
    public void SpawnLoot(Vector3 position)
    {
        // Спавн кристала всегда
        SpawnLoot(PickableType.TypePickable.Diamond, position);

        // Спавн бонусов с шансом и не в том же месте, а рядом, чтобы не появлялись один в другом объекты
        if (Random.value < _bonusDropChance)
        {
            Vector3 offset = Random.insideUnitCircle * _dropRadius;
            SpawnLoot(EnumUtils.GetRandomEnumExcludingFirst<PickableType.TypePickable>(), position + offset);
        }

    }

    private void SpawnLoot(PickableType.TypePickable type, Vector3 position)
    {
        var pikupObj = pools[type].GetOrCreate();
        pikupObj.transform.position = new Vector3(position.x, 0.0f, position.z);
    }

    public void RecycleLoot(Pickup pickupObj)
    {
        if (pickupObj == null || !pools.TryGetValue(pickupObj.TypePickable, out var pool)) 
            return;

        pool.Recycle(pickupObj);
    }

    private void CreatePoolPickupEntry(PickupEntry pickupEntry)
    {
        Pool<Pickup> pool = new()
        {
            parent = transform,
            prefab = pickupEntry.PickupPrefab
        };

        pools.Add(pickupEntry.TypePickable, pool);
    }
}
