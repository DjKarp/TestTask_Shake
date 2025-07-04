using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AddedControl
{
    public class JoystickDirectionInput : IDirectionInput
    {
        private Joystick _joystick;

        public JoystickDirectionInput(Joystick joystick)
        {
            _joystick = joystick;
        }

        public Vector3 GetHorizontal()
        {
            return _joystick.JoystickDirection.x == 0 ? Vector3.zero : _joystick.JoystickDirection.x > 0 ? Vector3.right : Vector3.left; ;
        }

        public Vector3 GetVertical()
        {
            return _joystick.JoystickDirection.y == 0 ? Vector3.zero : _joystick.JoystickDirection.y > 0 ? Vector3.forward : Vector3.back;
        }
    }
}