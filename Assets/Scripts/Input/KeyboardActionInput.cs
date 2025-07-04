using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AddedControl
{
    public class KeyboardActionInput : IActionInput
    {
        public bool IsDashPressed()
        {
            return Input.GetMouseButtonDown(1);
        }

        public bool IsFirePress()
        {
            return Input.GetMouseButton(0);
        }

        public bool IsFirePressed()
        {
            return Input.GetMouseButtonDown(0);
        }

        public bool IsJumpPressed()
        {
            return Input.GetKeyDown(KeyCode.Space);
        }

        public bool IsRebootPressed()
        {
            return Input.GetKeyDown(KeyCode.R);
        }

        public bool IsSlowPressed()
        {
            return Input.GetKeyDown(KeyCode.LeftShift);
        }
    }
}