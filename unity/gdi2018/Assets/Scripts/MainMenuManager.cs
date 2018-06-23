using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
	
	[SerializeField]
	private Button startButton;
	
	[SerializeField]
	private Button quitButton;

	private void Awake()
	{
		startButton.onClick.AddListener(OnStartButtonClick);
		quitButton.onClick.AddListener(OnQuitButtonClick);
	}

	private void OnStartButtonClick()
	{
		SceneManager.LoadScene("CompletedScene");
	}

	private void OnQuitButtonClick()
	{
		Application.Quit();
	}
	
}
