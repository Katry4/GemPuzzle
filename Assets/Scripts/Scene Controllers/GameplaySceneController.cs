using UnityEngine;
using System.Collections;
using System;
using UnityEngine.SceneManagement;

public class GameplaySceneController : BasicSceneController
{
	#region Overrided base method
	public override void OnBackkeyPressed()
	{
		SceneManager.LoadScene(IDs.Scenes.mainMenuScene);
	}
	#endregion
}
