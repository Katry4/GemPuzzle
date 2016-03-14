using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[SerializeField] private IDs.GameType _gameType;
	[SerializeField] private GameplayController gameController;

	public void SetInputMove(Vector2 dir)
	{
		Debug.Log("Input mvoe " + dir);
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

	public bool IsGamePaused()
	{
		return gameController.IsPaused();
	}

	public void PauseGame()
	{
		gameController.Pause();
	}

	public void UnpauseGame()
	{
		gameController.Unpause();
	}
	
	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			gameObject.name = "GameManager";
		}
		else
		{
			DestroyObject(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	public void OnLevelWasLoaded(int level)
	{
		if (SceneManager.GetActiveScene().name == IDs.Scenes.gameScene)
		{
			gameController = FindObjectOfType<GameplayController>();
		}
	}
}
