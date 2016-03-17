using UnityEngine;
using System.Collections;
using System;

public class GameplayController : MonoBehaviour
{
	[SerializeField] private UIGameplaySceneController _gameplaySceneController;
	private bool _isGamePaused = false;
	[SerializeField] private BoardController _board;

	public Action<IDs.GameType, int> UpdateHUD;

	private IDs.GameType _currentGametype;
	private int _completedSteps = 0;
	private int _maxAmountOfSteps;
	private float _gameplayTime = 0;
	private int _gameplayTimeInSec = 0;
	private float _maxGameplayTime;

	public void StartGame()
	{
		Pause();
		_board.Init();
		_board.OnWin = () =>
		{
			GameManager.Instance.OnLevelCompleted();
			StartCoroutine(WinGame());
		};
		_currentGametype = GameManager.Instance.GetCurrentGameType();
		if (_currentGametype == IDs.GameType.Steps)
		{
			_maxAmountOfSteps = GameManager.Instance.GetMaxAmountOfSteps();
			_completedSteps = 0;
		}
		else
		{
			_gameplayTime = 0;
			_gameplayTimeInSec = 0;
			_maxGameplayTime = GameManager.Instance.GetMaxGameplayTimeInMins() * 60;
		}

		UpdateGameplayHUD();
		Unpause();
	}

	private IEnumerator WinGame()
	{
		yield return new WaitForSeconds(0.2f * Time.timeScale);
		CompleteGame(true);
	}

	public void Move(IntVector2 dir)
	{
		if (!IsPaused())
		{
			if (!_board.TryMove(dir))
			{
				iTween.ShakePosition(Camera.main.gameObject, Vector3.one * UnityEngine.Random.value * 0.5f, 0.3f);
			}
			else
			{
				if (_currentGametype == IDs.GameType.Steps && ++_completedSteps >= _maxAmountOfSteps)
				{
					CompleteGame(false);
				}
				UpdateGameplayHUD();
			}
		}
	}

	void Update()
	{
		if (_currentGametype == IDs.GameType.Time && !IsPaused())
		{
			_gameplayTime += Time.deltaTime;
			if (_gameplayTime > _gameplayTimeInSec)
			{
				OnTimeChanged();
			}
		}
	}

	private void OnTimeChanged()
	{
		_gameplayTimeInSec = (int)Math.Ceiling(_gameplayTime) - 1;
		float spentTime = (int)(_maxGameplayTime - _gameplayTime);

		UpdateGameplayHUD();

		if (spentTime > _maxGameplayTime)
		{
			CompleteGame(false);
		}
	}

	private void CompleteGame(bool result)
	{
		_isGamePaused = true;
		_gameplaySceneController.ShowCompleteGamePanel(result);
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

	private void UpdateGameplayHUD()
	{
		if (UpdateHUD != null)
		{
			UpdateHUD(_currentGametype, GetCurrentLeftAmount());
		}
	}

	private int GetCurrentLeftAmount()
	{
		if (_currentGametype == IDs.GameType.Time)
		{
			return GetSecondsLeft();
		}
		else
		{
			return GetMovesLeft();
		}
	}

	private int GetSecondsLeft()
	{
		return (int)_maxGameplayTime - _gameplayTimeInSec;
	}

	private int GetMovesLeft()
	{
		return _maxAmountOfSteps - _completedSteps;
	}
}
