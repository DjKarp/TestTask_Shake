using UnityEngine;

public class DiamondPickup : Pickup, IPickable
{
    private int _amount = 1;
    private PickableCollector _playerPickables;

    private void Awake()
    {
        Pickable = this;
    }

    public void OnPicked(GameObject picker)
    {
        picker?.TryGetComponent(out _playerPickables);

        if (_playerPickables != null)
        {
            _playerPickables.AddDiamonds(_amount);
        }

        SpawnLootService.Instance.RecycleLoot(this);
    }

    public override void Deactivate()
    {
        MagnetPickupMoved magnetPickupMoved;
        gameObject.TryGetComponent(out magnetPickupMoved);
        Destroy(magnetPickupMoved);

        base.Deactivate();
    }
}
