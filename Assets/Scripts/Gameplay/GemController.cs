using UnityEngine;
using System.Collections;
using System;

public class GemController : MonoBehaviour {

	[SerializeField] private TextMesh _text;
	private int _number;

	public void Init(int index)
	{
		_number = index;
		_text.text = _number.ToString();
	}
}
