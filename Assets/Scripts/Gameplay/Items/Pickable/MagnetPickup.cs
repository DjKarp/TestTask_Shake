using UnityEngine;

public class MagnetPickup : Pickup, IPickable
{

    private void Awake()
    {
        Pickable = this;
    }

    public void OnPicked(GameObject picker)
    {
        var diamonds = FindObjectsOfType<DiamondPickup>();

        foreach (var diamond in diamonds)
        {
            MagnetPickupMoved magnetPickupMoved;
            diamond.TryGetComponent(out magnetPickupMoved);

            if (magnetPickupMoved == null)
            {
                magnetPickupMoved = diamond.gameObject.AddComponent<MagnetPickupMoved>();
            }

            magnetPickupMoved.Init(picker.transform);
        }

        SpawnLootService.Instance.RecycleLoot(this);
    }
}
