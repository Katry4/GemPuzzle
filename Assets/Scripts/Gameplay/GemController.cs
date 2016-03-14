using UnityEngine;
using System.Collections;
using System;

public class GemController : MonoBehaviour
{
	[SerializeField] private TextMesh _text;
	private int _number;
	private float _distance;

	const int EmptyNumber = -33;

	public void Init(int index, float distance)
	{
		_number = index;
		_text.text = _number.ToString();
		_distance = distance;
	}

	internal void MoveTo(IntVector2 direction, float animationTime)
	{
		//FIXME Because of the rotation of the board - left and right direction are swaped
		Vector3 targetPos = new Vector3(transform.localPosition.x + direction.x * _distance, transform.localPosition.y, transform.localPosition.z - direction.y * _distance);
		if (animationTime == 0)
		{
			transform.localPosition = targetPos;
		}
		else
		{
			iTween.MoveTo(gameObject, iTween.Hash("position", targetPos, "islocal", true, "time", animationTime));
		}
	}

	public int Number
	{
		get { return _number; }
	}

	public static GemController BlankGem()
	{
		var gem = new GemController();
		gem._number = EmptyNumber;
		return gem;
	}

	public bool IsEmpty()
	{
		return _number == EmptyNumber;
	}
}
