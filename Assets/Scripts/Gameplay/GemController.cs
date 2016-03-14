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
	}

	internal void Move(IntVector2 direction)
	{

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
