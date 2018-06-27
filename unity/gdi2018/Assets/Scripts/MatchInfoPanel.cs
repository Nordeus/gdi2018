using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MatchInfoPanel : MonoBehaviour
{

	[SerializeField]
	private Text matchTimeText;

	[SerializeField]
	private Text matchScoreText;

	[SerializeField]
	private Image backImage;

	[SerializeField]
	private Color winColor;
	
	[SerializeField]
	private Color lostColor;

	public void Refresh(PlayerModel.BattleData data)
	{
		matchTimeText.text = data.endedAt;
		matchScoreText.text = data.score.ToString();
		backImage.color = data.won ? winColor : lostColor;
	}

}
