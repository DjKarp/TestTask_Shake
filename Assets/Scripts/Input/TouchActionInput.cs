using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AddedControl
{
    public class TouchActionInput : MonoBehaviour, IActionInput
    {
        [SerializeField] private Button _fireButton;
        [SerializeField] private Button _slowButton;

        private bool _isFirePress = false;
        private bool _isSlowPress = false;

        public void Start()
        {
            AddEvents(_fireButton, () => _isFirePress = true, () => _isFirePress = false);
            AddEvents(_slowButton, () => _isSlowPress = true, () => _isSlowPress = false);
        }

        public bool IsDashPressed()
        {
            return true;
        }

        public bool IsFirePress()
        {
            return _isFirePress;
        }

        public bool IsFirePressed()
        {
            return _isFirePress;
        }

        public bool IsJumpPressed()
        {
            return true;
        }

        public bool IsRebootPressed()
        {
            return true;
        }

        public bool IsSlowPressed()
        {
            return _isSlowPress;
        }

        private void AddEvents(Button button, System.Action onDown, System.Action onUp)
        {
            var trigger = button.gameObject.GetComponent<EventTrigger>();
            if (trigger == null)
            {
                trigger = button.gameObject.AddComponent<EventTrigger>();
            }

            // PointerDown
            var entryDown = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerDown
            };
            entryDown.callback.AddListener((data) => onDown.Invoke());
            trigger.triggers.Add(entryDown);

            // PointerUp
            var entryUp = new EventTrigger.Entry
            {
                eventID = EventTriggerType.PointerUp
            };
            entryUp.callback.AddListener((data) => onUp.Invoke());
            trigger.triggers.Add(entryUp);
        }
    }
}