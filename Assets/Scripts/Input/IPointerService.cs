using UnityEngine;

namespace AddedControl
{
    public interface IPointerService
    {
        Vector3 GetPointerPosition(Camera virtualCamera, LayerMask groundLayer);
    }
}