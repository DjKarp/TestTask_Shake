using UnityEngine;

public class HealthPickup : Pickup, IPickable
{
    private int _healAmount = 25;
    private Combat _combat;

    private void Awake()
    {
        Pickable = this;
    }

    public void OnPicked(GameObject picker)
    {
        picker?.TryGetComponent(out _combat);

        if (_combat != null)
        {
            _combat.AddedHealth(_healAmount);
        }

        SpawnLootService.Instance.RecycleLoot(this);
    }
}
