using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{

	[SerializeField]
	private Text scoreText;

	private void Update()
	{
		if (!Application.isPlaying)
			return;

		scoreText.text = GameManager.I.Score.ToString();
	}
}
