using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System;

public class UIMainMenuSceneController : UIBasicSceneController
{
	[SerializeField] private GameObject _credentialPanel;

	void Start()
	{
		_credentialPanel.SetActive(false);
	}

	public void StartTimeModeGame()
	{
		GameManager.Instance.SetCurrentGameType(IDs.GameType.Time);
		SceneManager.LoadScene(IDs.Scenes.gameScene);
	}

	public void StartStepsModeGame()
	{
		GameManager.Instance.SetCurrentGameType(IDs.GameType.Steps);
		SceneManager.LoadScene(IDs.Scenes.gameScene);
	}

	public void ShowCredentials()
	{
		_credentialPanel.SetActive(true);
	}

	public void HideCredentials()
	{
		_credentialPanel.SetActive(false);
	}

	#region Overrided method
	public override void OnBackkeyPressed()
	{
		if (_credentialPanel.activeSelf)
		{
			HideCredentials();
		}
		else
		{
			Application.Quit();
		}
	}
	#endregion
}
