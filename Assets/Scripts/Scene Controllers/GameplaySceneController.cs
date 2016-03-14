using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplaySceneController : BasicSceneController
{
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
		Time.timeScale = 0.0001f;
		_pausePanel.SetActive(true);
	}

	private void UpauseGame()
	{
		Time.timeScale = 1;
		_pausePanel.SetActive(false);
	}

	public void ShowWinPanel(bool didWin) {
		string MainText = didWin ? "You win" : "You lose";
		_winPanel.SetActive(true);
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
