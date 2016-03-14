using UnityEngine;
using System.Collections;
using System;

public class GameplayController : MonoBehaviour
{

	[SerializeField] private bool _isGamePaused = false;
	[SerializeField] private BoardController _board;

	Action OnMoveComplited;
	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	internal void Move(IntVector2 dir)
	{
		if (!IsPaused())
		{
			if (!_board.TryMove(dir))
			{
				iTween.ShakePosition(Camera.main.gameObject, Vector3.one * UnityEngine.Random.value * 0.5f, 0.3f);
			}
			else
			{
				OnMoveComplited();
			}
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
