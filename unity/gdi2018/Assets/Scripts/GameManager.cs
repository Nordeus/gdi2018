using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

	private static GameManager instance;
	public static GameManager I
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<GameManager>();
			}
			return instance;
		}
	}
	
	private const float TIME_FOR_LEVEL = 60f;

	private Tank tank;
	private List<ShootingTower> towers;
	private float remainingTime;

	public enum GameStateType
	{
		Playing,
		Paused,
		EndGame
	}
	
	public GameStateType GameState { get; private set; }
	
	public int Score
	{
		get { return Mathf.RoundToInt(remainingTime); }
	}

	public Action<bool> OnGameEnds;

	private void Start()
	{
		ResumeGame();
		tank = FindObjectOfType<Tank>();
		towers = FindObjectsOfType<ShootingTower>().ToList();
		remainingTime = TIME_FOR_LEVEL;
	}

	private void Update()
	{
		if (GameState == GameStateType.Playing)
		{
			remainingTime -= Time.deltaTime;
		}
		
		// check for end game
		if (GameState == GameStateType.Playing && IsEnd())
		{
			Debug.Log("END");
			Time.timeScale = 0f;
			GameState = GameStateType.EndGame;

			var playerHasWon = tank.Health > 0;
			PlayerModel.PostScore(Score, playerHasWon);
			
			if (OnGameEnds != null)
			{
				OnGameEnds(playerHasWon);
			}
		}
		
		// check for pause
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			if (GameState == GameStateType.Playing)
			{
				PauseGame();
			}
			else if (GameState == GameStateType.Paused)
			{
				ResumeGame();
			}
			else if (GameState == GameStateType.EndGame)
			{
				//ExitToMainMenu();
				ReloadGame();
			}
		}
	}

	private bool IsEnd()
	{
		return tank.Health == 0 || remainingTime <= 0 || towers.All(t => !t.gameObject.activeInHierarchy);
	}

	private void ResumeGame()
	{
		Debug.Log("Resumed");
		GameState = GameStateType.Playing;
		Time.timeScale = 1f;
	}

	private void PauseGame()
	{
		Debug.Log("Paused");
		GameState = GameStateType.Paused;
		Time.timeScale = 0f;
	}

	private void ReloadGame()
	{
		Debug.Log("Reload");
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	private void ExitToMainMenu()
	{
		SceneManager.LoadScene("MainMenuScene");
	}
}
