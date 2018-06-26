using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{

	[SerializeField]
	private Tank tank;

	[SerializeField]
	private List<Image> healthImages;

	[SerializeField]
	private Color fullColor;
	
	[SerializeField]
	private Color emptyColor;

	private void Update()
	{
		for (int i = 0; i < healthImages.Count; i++)
		{
			healthImages[i].color = i < tank.Health ? fullColor : emptyColor;
		}
	}
}
