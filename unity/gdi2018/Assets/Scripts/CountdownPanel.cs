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

	private bool isStarted;

	private void Start()
	{
		GameManager.I.OnCountdown += OnCountdown;
		isStarted = false;
	}

	private void OnCountdown(int count)
	{
		if (!isStarted)
		{
			isStarted = true;
			countdownAnimator.SetTrigger("countdown");
		}

		countdownText.text = count.ToString();
		if (count == 0)
		{
			countdownText.text = "GO!";
			countdownAnimator.SetTrigger("fadeOut");
		}
	}
}
