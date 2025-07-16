using UnityEngine;

namespace AddedControl
{
    public class JoystickPointerService : IPointerService
    {
        private Joystick _lookJoystick;
        private float _pointerDistance = 5f;
        private Vector3 _lastPointerDirection;

        public JoystickPointerService(Joystick lookJoystick)
        {
            _lookJoystick = lookJoystick;
        }

        public Vector3 GetPointerPosition(Camera virtualCamera, LayerMask groundLayer)
        {
            // Запоминаем направление и если игрок не нажимает на Joystick направления, то оставляем направленым в ту же сторону.
            if (_lookJoystick.JoystickDirection.sqrMagnitude > 0.01f)
                _lastPointerDirection = _lookJoystick.JoystickDirection;

            Vector3 direction = new Vector3(_lastPointerDirection.x, 0f, _lastPointerDirection.y).normalized;
            
            return GameManager.Instance.LevelManager.Player.transform.position + (direction * _pointerDistance);
        }
    }
}