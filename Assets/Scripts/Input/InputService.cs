using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AddedControl;

public class InputService : MonoBehaviour
{	
	public bool TouchInput => _touchAction.gameObject.activeSelf;

	[SerializeField] private Joystick _joystickMove;
	[SerializeField] private Joystick _joystickPointer;
	private TouchActionInput _touchAction;


	private IDirectionInput _directionController;
	public IDirectionInput DirectionController => _directionController;

	private IDirectionInput _pointerController;
	public IDirectionInput PointerController => _pointerController;


	private IActionInput _actionController;
	public IActionInput ActionController => _actionController;


	private void Awake()
	{
		// Initialize control
		if (true)
		{
			_joystickMove.gameObject.SetActive(true);
			_directionController = new JoystickDirectionInput(_joystickMove);
			_pointerController = new JoystickDirectionInput(_joystickPointer);
			_actionController = GetTouchActionInput();
		}
		else
		{
			_directionController = new KeyboardDirectionInput();
			_actionController = new KeyboardActionInput();
		}
	}

	public TouchActionInput GetTouchActionInput()
	{
		_touchAction = GetComponentInChildren<TouchActionInput>();
		_touchAction.gameObject.SetActive(true);
		return _touchAction;
	}
}
