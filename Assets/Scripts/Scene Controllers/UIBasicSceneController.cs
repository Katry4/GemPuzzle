using UnityEngine;
using System.Collections;

public abstract class UIBasicSceneController : MonoBehaviour
{
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			OnBackkeyPressed();
		}
	}

	public abstract void OnBackkeyPressed();
}
