using UnityEngine;
using System.Collections;
using System;

public class GemController : MonoBehaviour
{
	[SerializeField] private TextMesh _text;
	private int _number;

	const int EmptyNumber = -33;

	public void Init(int index, Vector3 newPos)
	{
		_number = index;
		_text.text = _number.ToString();

		transform.localPosition = newPos;
	}

	public void InitAsBlank()
	{
		_number = EmptyNumber;
	}

	internal void MoveTo(Vector3 targetPos, float animationTime)
	{
		StopAllCoroutines();
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

	public bool IsEmpty()
	{
		return _number == EmptyNumber;
	}
}
