using UnityEngine;

public class Pickup : PooledBehaviour
{
    public PickableType.TypePickable TypePickable;
    protected IPickable Pickable;

    public override void Activate()
    {
        gameObject.SetActive(true);
    }

    public override void Deactivate()
    {
        gameObject.SetActive(false);
    }

    public override void Reset()
    {
        // none
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            if (Pickable == null) 
                TryGetComponent<IPickable>(out var _pickable);

            Pickable?.OnPicked(other.gameObject);
        }
    }
}
