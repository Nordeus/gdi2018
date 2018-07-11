using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndMenuController : MonoBehaviour
{

	[SerializeField]
	private Text endGameText;

	[SerializeField]
	private Text pressKeyText;

	[SerializeField]
	private float fadeDuration = 0.6f;
	
	[SerializeField]
	private float bounceDuration = 0.5f;

	[SerializeField]
	private float flashDuration = 0.4f;

	[SerializeField]
	private Color endColor;

	private CanvasGroup myCanvasGroup;

	private void Awake()
	{
		myCanvasGroup = GetComponent<CanvasGroup>();

		myCanvasGroup.alpha = 0f;
		
		GameManager.I.OnGameEnds += OnGameEnds;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			OnGameEnds(true);
		}
		else if (Input.GetKeyDown(KeyCode.R))
		{
			myCanvasGroup.alpha = 0f;
			endGameText.rectTransform.localScale = Vector3.zero;
			pressKeyText.color = new Color(endColor.r, endColor.g, endColor.b, 0);
		}
	}

	private void OnGameEnds(bool playerWon)
	{
		endGameText.text = playerWon ? "YOU WON!" : "LOSER!";

		var fadeTween = Go.to(myCanvasGroup, fadeDuration, new GoTweenConfig()
			.floatProp("alpha", 1f)
			.setEaseType(GoEaseType.Linear)
			//.setUpdateType(GoUpdateType.TimeScaleIndependentUpdate)
		);

		endGameText.rectTransform.localScale = Vector3.zero;
		var bounceTween = Go.to(endGameText.rectTransform, bounceDuration, new GoTweenConfig()
			.vector3Prop("localScale", Vector3.one)
			.setEaseType(GoEaseType.BounceOut)
			//.setUpdateType(GoUpdateType.TimeScaleIndependentUpdate)
		);
		
		var chain = new GoTweenChain(new GoTweenCollectionConfig()
			.setUpdateType(GoUpdateType.TimeScaleIndependentUpdate)
		);
		chain.append(fadeTween).appendDelay(0.2f).append(bounceTween).play();

		pressKeyText.color = new Color(endColor.r, endColor.g, endColor.b, 0);
		var flashTween = Go.to(pressKeyText, flashDuration, new GoTweenConfig()
			.colorProp("color", endColor)
			.setEaseType(GoEaseType.Linear)
			.setIterations(-1, GoLoopType.PingPong)
			.setUpdateType(GoUpdateType.TimeScaleIndependentUpdate)
			.setDelay(2f)
		);
	}
}
