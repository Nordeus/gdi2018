using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuController : MonoBehaviour
{

	[SerializeField]
	private Text endGameText;

	private void Awake()
	{
		gameObject.SetActive(false);
		GameManager.I.OnGameEnds += OnGameEnds;
	}

	private void OnGameEnds(bool playerWon)
	{
		endGameText.text = playerWon ? "YOU WON!" : "LOSER!";
		gameObject.SetActive(true);
	}
}
