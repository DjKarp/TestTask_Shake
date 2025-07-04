using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace AddedControl
{
    public class TouchActionInput : MonoBehaviour, IActionInput, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Button _fireButton;
        [SerializeField] private Button _slowButton;

        private bool _isFirePress = false;

        public void Start()
        {
            _fireButton.onClick.AddListener(() => IsFirePressed());

            _slowButton.onClick.AddListener(() => IsSlowPressed());
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
            return true;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_fireButton.interactable)
            {
                _isFirePress = true;
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isFirePress = false;
        }
    }
}