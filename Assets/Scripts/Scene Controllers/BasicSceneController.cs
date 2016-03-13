using UnityEngine;
using System.Collections;

public abstract class BasicSceneController : MonoBehaviour
{

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			OnBackkeyPressed();
		}
	}

	public abstract void OnBackkeyPressed();
}
