using UnityEngine;
using AddedControl;

public class Pointer : MonoBehaviour
{
	[HideInInspector]
	public Camera virtualCamera;

	[SerializeField]
	private LayerMask groundLayer;

	private IPointerService _pointerService;

	// Это лучше сделать через DI
	public void Init(IPointerService pointerService)
    {
		_pointerService = pointerService;
    }

	private void Start()
	{
		Cursor.visible = false;
	}

	private void Update()
	{
		base.transform.position = _pointerService.GetPointerPosition(virtualCamera, groundLayer);
	}
}
