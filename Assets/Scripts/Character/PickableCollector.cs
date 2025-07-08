using UnityEngine;

public class PickableCollector : MonoBehaviour
{
    [SerializeField] private int _waveMultiplier = 3;
    [SerializeField] private int _startCountDiamondsForAlly = 5;

    private int _diamondsCollected;
    private int _alliesSpawned;

    public int CurrentDiamonds => _diamondsCollected;

    public void AddDiamonds(int amount)
    {
        _diamondsCollected += amount;
    }

    public int GetRequiredDiamondsForNextAlly()
    {
        return _startCountDiamondsForAlly + Mathf.Min(_alliesSpawned, EnemySpawner.Instance.CurrentWave) * _waveMultiplier;
    }

    public bool CanSpawnAlly()
    {
        return _diamondsCollected >= GetRequiredDiamondsForNextAlly();
    }

    public bool TrySpendDiamonds(int amount)
    {
        if (_diamondsCollected >= amount)
        {
            _diamondsCollected -= amount;
            return true;
        }
        return false;
    }

    public void IncrementAllyCount()
    {
        _alliesSpawned++;
    }
}
