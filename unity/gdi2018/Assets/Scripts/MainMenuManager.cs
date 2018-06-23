using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
	
	[SerializeField]
	private Button startButton;

	private void Awake()
	{
		startButton.onClick.AddListener(OnStartButtonClick);
	}

	private void OnStartButtonClick()
	{
		SceneManager.LoadScene("CompletedScene");
	}
	
}
