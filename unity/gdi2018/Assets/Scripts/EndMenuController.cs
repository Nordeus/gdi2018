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

	private CanvasGroup myCanvasGroup;

	private void Awake()
	{
		myCanvasGroup = GetComponent<CanvasGroup>();
		myCanvasGroup.alpha = 0f;

		GameManager.I.OnGameEnds += OnGameEnds;
	}

	private void OnGameEnds(bool playerWon)
	{
		endGameText.rectTransform.localScale = Vector3.zero;
		endGameText.text = playerWon ? "YOU WON!" : "LOSER!";
		pressKeyText.color = new Color(1,1,1,0);
		
		var fadeTween = Go.to(myCanvasGroup, 0.6f, new GoTweenConfig()
			.floatProp("alpha", 1f)
			.setEaseType(GoEaseType.Linear)
			//.setUpdateType(GoUpdateType.TimeScaleIndependentUpdate)
		);
		
		var bounceTextTween = Go.to(endGameText.rectTransform, 0.4f, new GoTweenConfig()
			.vector3Prop("localScale", Vector3.one)
			.setEaseType(GoEaseType.BounceOut)
		);
		
		var chain = new GoTweenChain(
			new GoTweenCollectionConfig()
			.setUpdateType(GoUpdateType.TimeScaleIndependentUpdate)
		)
		.append(fadeTween)
		.appendDelay(0.3f)
		.append(bounceTextTween);
		chain.play();

		var pressKeyTween = Go.to(pressKeyText, 0.4f, new GoTweenConfig()
			.colorProp("color", Color.white)
			.setEaseType(GoEaseType.Linear)
			.setUpdateType(GoUpdateType.TimeScaleIndependentUpdate)
			.setIterations(-1, GoLoopType.PingPong)
			.setDelay(2f)
		);
	}
}
