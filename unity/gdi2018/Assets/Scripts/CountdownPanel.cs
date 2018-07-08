using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountdownPanel : MonoBehaviour
{

	[SerializeField]
	private Text countdownText;

	[SerializeField]
	private Animator countdownAnimator;

	private bool isRunning;

	private void Start()
	{
		GameManager.I.OnCountdown += OnCountdown;
	}

	private void OnCountdown(int count)
	{
		Debug.LogError(count);
		if (!isRunning)
		{
			isRunning = true;
			countdownAnimator.SetTrigger("startCountdown");
		}

		countdownText.text = count.ToString();
		
		if (count == 0)
		{
			isRunning = false;
			countdownText.text = "GO!";
			countdownAnimator.SetTrigger("fadeOut");
		}
	}
}
