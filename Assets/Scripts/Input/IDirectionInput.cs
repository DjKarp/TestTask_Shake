using UnityEngine;

namespace AddedControl
{
    public interface IDirectionInput
    {
        Vector3 GetHorizontal();
        Vector3 GetVertical();
    }
}