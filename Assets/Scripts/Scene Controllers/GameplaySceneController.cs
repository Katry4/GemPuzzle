using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplaySceneController : BasicSceneController
{
	[SerializeField] private GameplayController _gameplayController;
	[SerializeField] private Text _leftText;

	[SerializeField] private GameObject _pausePanel;

	[SerializeField] private GameObject _winPanel;
	[SerializeField] private Text _winMainText;


	void Start()
	{
		_pausePanel.SetActive(false);
		_winPanel.SetActive(false);
	}

	private void PauseGame()
	{
		_pausePanel.SetActive(true);
		_gameplayController.Pause();
    }

	private void UpauseGame()
	{
		_pausePanel.SetActive(false);
		_gameplayController.Unpause();
    }

	public void ShowCompleteGamePanel(bool didWin) {
		string mainText = didWin ? "You win" : "You lose";
		_winMainText.text = mainText;
		_winPanel.SetActive(true);
	}

	public void UpdateGameHUD(string text)
	{
		_leftText.text = text;
	}

	#region Buttons events
	public void OnResumeButtonPressed()
	{
		UpauseGame();
	}

	public void OnReplayButtonPressed()
	{
		SceneManager.LoadScene(IDs.Scenes.gameScene);
	}

	public void OnExitButtonPressed()
	{
		SceneManager.LoadScene(IDs.Scenes.mainMenuScene);
	}
	#endregion

	#region Overrided base method
	public override void OnBackkeyPressed()
	{
		if (_pausePanel.activeSelf) {
			UpauseGame();
			return;
		} else if (_winPanel.activeSelf)
		{
			return;
		}
		else
		{
			PauseGame();
		}
	}
	#endregion
}
