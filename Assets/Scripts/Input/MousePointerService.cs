using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AddedControl
{
    public class MousePointerService : IPointerService
    {
        private RaycastHit[] hitInfo = new RaycastHit[1];

        public Vector3 GetPointerPosition(Camera virtualCamera, LayerMask groundLayer)
        {
            Ray ray = virtualCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.RaycastNonAlloc(ray, hitInfo, 100f, groundLayer) > 0)
            {
                return hitInfo[0].point;
            }

            return Vector3.zero;
        }
    }
}