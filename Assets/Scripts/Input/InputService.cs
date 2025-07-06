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

	private IPointerService _pointerService;
	public IPointerService PointerService => _pointerService;


	private IActionInput _actionController;
	public IActionInput ActionController => _actionController;


	private void Awake()
	{
		// Initialize control
		if (false /*GP_Device.IsMobile*/)
		{
			_joystickMove.gameObject.SetActive(true);
			_joystickPointer.gameObject.SetActive(true);
			_directionController = new JoystickDirectionInput(_joystickMove);
			_pointerService = new JoystickPointerService(_joystickPointer);
			_actionController = GetTouchActionInput();
		}
		else
		{
			_directionController = new KeyboardDirectionInput();			
			_pointerService = new MousePointerService();
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
