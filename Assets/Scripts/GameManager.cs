using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[SerializeField] private IDs.GameType _gameType;
	[SerializeField] private int _maxAmountOfSteps = 20;
	[SerializeField] private float _maxGameDurationInMins = 0.5f;
	[SerializeField] private GameplayController gameController;

	public void SetInputMove(IntVector2 dir)
	{
		gameController.Move(dir);
	}

	public IDs.GameType GetCurrentGameType()
	{
		return _gameType;
	}

	public void SetCurrentGameType(IDs.GameType type)
	{
		_gameType = type;
	}

	public int GetMaxAmountOfSteps()
	{
		return _maxAmountOfSteps;
	}

	public float GetMaxGameplayTimeInMins()
	{
		return _maxGameDurationInMins;
	}

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			gameObject.name = "GameManager";

			if (SceneManager.GetActiveScene().name == IDs.Scenes.gameScene)
			{
				gameController = FindObjectOfType<GameplayController>();
				gameController.StartGame();
			}
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
			if (SceneManager.GetActiveScene().name == IDs.Scenes.gameScene)
			{
				gameController = FindObjectOfType<GameplayController>();
				gameController.StartGame();
			}
		}
	}
}
