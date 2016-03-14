using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class InputController : MonoBehaviour
{
	[SerializeField] private float _defaultOffset = 20;

	private Vector2 _beginDragPosition;
	public void BeginDrag(BaseEventData data)
	{
		PointerEventData pointerData = (PointerEventData)data;
		_beginDragPosition = pointerData.position;
	}

	public void EndDrag(BaseEventData data)
	{
		PointerEventData pointerData = (PointerEventData)data;
		var delta = pointerData.position - _beginDragPosition;
		if (delta.magnitude < _defaultOffset) return;

		IntVector2 direction;
		if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
		{
			direction = delta.x > 0 ? IntVector2.right : IntVector2.left;
		}
		else
		{
			direction = delta.y > 0 ? IntVector2.down : IntVector2.up;
		}

		GameManager.Instance.SetInputMove(direction);
	}
}
