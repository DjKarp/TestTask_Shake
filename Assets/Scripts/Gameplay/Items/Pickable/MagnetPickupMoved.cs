using UnityEngine;

public class MagnetPickupMoved : MonoBehaviour
{
    private Transform _target;
    private float _speed = 2f;

    public void Init(Transform targetTransform)
    {
        _target = targetTransform;
    }

    private void Update()
    {
        if (_target == null) 
            return;

        transform.position = Vector3.Lerp(transform.position, _target.position, _speed * Time.deltaTime);
    }
}
