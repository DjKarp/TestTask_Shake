using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AddedControl
{
    public class KeyboardDirectionInput : IDirectionInput
    {
        public Vector3 GetHorizontal()
        {
            return Input.GetKey(KeyCode.D) ? Vector3.right : Input.GetKey(KeyCode.A) ? Vector3.left : Vector3.zero;
        }

        public Vector3 GetVertical()
        {
            return Input.GetKey(KeyCode.W) ? Vector3.forward : Input.GetKey(KeyCode.S) ? Vector3.back : Vector3.zero;
        }        
    }
}