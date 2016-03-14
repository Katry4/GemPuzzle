using UnityEngine;
using System.Collections;
using System;

public class GameplayController : MonoBehaviour {

	[SerializeField] private bool _isGamePaused = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	internal void Move(Vector2 dir)
	{
		if (!IsPaused())
		{
			throw new NotImplementedException();
		}
	}

	
	internal bool IsPaused()
	{
		return _isGamePaused;
	}

	internal void Pause()
	{
		_isGamePaused = true;
	}


	internal void Unpause()
	{
		_isGamePaused = false;
    }
}
