using UnityEngine;
using System.Collections;
using System;

public class GameplayController : MonoBehaviour
{
	[SerializeField] private GameplaySceneController _gameplaySceneController;
	[SerializeField] private bool _isGamePaused = false;
	[SerializeField] private BoardController _board;

	private IDs.GameType _currentGametype;
	private int _completedSteps = 0;
	private int _maxAmountOfSteps;
	private float _gameplayTime = 0;
	private float _maxGameplayTime;

	public void StartGame()
	{
		Pause();
		_board.Init();
		_board.OnWin = () => { StartCoroutine(WinGame()); };
		_currentGametype = GameManager.Instance.GetCurrentGameType();
		if (_currentGametype == IDs.GameType.Steps)
		{
			_maxAmountOfSteps = GameManager.Instance.GetMaxAmountOfSteps();
			_completedSteps = 0;
		}
		else
		{
			_gameplayTime = 0;
			_maxGameplayTime = GameManager.Instance.GetMaxGameplayTimeInMins() * 60;
		}
		Unpause();
	}

	private IEnumerator WinGame()
	{
		yield return new WaitForSeconds(0.2f * Time.timeScale);
		CompleteGame(true);
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
				if (_currentGametype == IDs.GameType.Steps && ++_completedSteps >= _maxAmountOfSteps)
				{
					CompleteGame(false);
				}
			}
		}
	}

	void Update()
	{
		if (_currentGametype == IDs.GameType.Time && !IsPaused())
		{
			_gameplayTime += Time.deltaTime;
			if (_gameplayTime > _maxGameplayTime)
			{
				CompleteGame(false);
			}
		}
		UpdateHUD();
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

	private void UpdateHUD()
	{
		string leftText;
		if (_currentGametype == IDs.GameType.Time)
		{
			float seconds = _maxGameplayTime - _gameplayTime;
			int minutes = (int)seconds / 60;
			seconds -= minutes * 60;
			leftText = minutes + ":" + (int)seconds + " seconds";
		}
		else
		{
			int leftSteps = _maxAmountOfSteps - _completedSteps;
			leftText = leftSteps + (leftSteps == 1 ? " move" : " moves");
		}
		_gameplaySceneController.UpdateGameHUD("Left: " + leftText);
	}
}
