using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[SerializeField] private IDs.GameType _gameType;

	// Use this for initialization
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

	public IDs.GameType GetCurrentGameType()
	{
		return _gameType;
	}

	public void SetCurrentGameType(IDs.GameType type)
	{
		_gameType = type;
	}
}
