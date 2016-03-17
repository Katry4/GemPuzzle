using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[SerializeField] private GameplayController gameController;
	[SerializeField] private IDs.GameType _gameType;

	[Header("Steps type")]
	[Range(10, 100)] [SerializeField] private int _maxAmountOfSteps = 20;
	[Tooltip("Acceptable amount of mistakes per correct step")]
	[Range(1, 5)] [SerializeField] private int _difficultyPerStep = 2;

	[Header("Time type")]
	[Range(0.5f, 2f)] [SerializeField] private float _maxGameDurationInMins = 0.5f;
	[Tooltip("Acceptable seconds per correct step")]
	[Range(1, 8)] [SerializeField]	private int _difficultyTimePerStep = 4;

	//	[SerializeField] private float _timeDeltaDifficulty = 0.5f;


	public IDs.GameType GetCurrentGameType()
	{
		return _gameType;
	}

	public void SetCurrentGameType(IDs.GameType type)
	{
		_gameType = type;
	}


	public void OnLevelCompleted()
	{
		if (_gameType == IDs.GameType.Steps){
			if (_difficultyPerStep > 1)
			{
				--_difficultyPerStep;
			}
		}
		else
		{
			if (_difficultyTimePerStep > 1)
			{
				--_difficultyTimePerStep;
			}
		}
	}

	public int GetMaxAmountOfSteps()
	{
		return _maxAmountOfSteps;
	}

	public int GetStepsDifficultyKoef()
	{
		return _difficultyPerStep;
	}

	public float GetMaxGameplayTimeInMins()
	{
		return _maxGameDurationInMins;
	}

	public int GetTimeDifficultyKoef()
	{
		return _difficultyTimePerStep;
	}

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			gameObject.name = "GameManager";

			StartGame();
		}
		else
		{
			DestroyObject(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	public void OnLevelWasLoaded(int level)
	{
		if (this == Instance)
		{
			StartGame();
		}
	}

	private void StartGame()
	{
		if (SceneManager.GetActiveScene().name == IDs.Scenes.gameScene)
		{
			gameController = FindObjectOfType<GameplayController>();
			gameController.StartGame();
		}
	}
}
