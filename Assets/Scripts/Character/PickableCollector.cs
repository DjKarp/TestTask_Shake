using UnityEngine;

public class PickableCollector : MonoBehaviour
{
    private int _diamondsCollected;
    private int _alliesSpawned;

    public void AddDiamonds(int amount)
    {
        _diamondsCollected += amount;

        Debug.Log($"Diamonds: {_diamondsCollected}");
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

    public void ResetInventory()
    {
        _diamondsCollected = 0;
        _alliesSpawned = 0;
    }
}
