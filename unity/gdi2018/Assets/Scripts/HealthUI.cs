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

	[SerializeField]
	private float duration = 0.5f;

	private void Start()
	{
		tank.OnHealthChanged += OnHealthChanged;
		
		for (int i = 0; i < healthImages.Count; i++)
		{
			healthImages[i].color = fullColor;
		}
	}

	private void OnHealthChanged()
	{
		StartCoroutine(HealthChangeAnimation(healthImages[tank.Health]));
	}

	private IEnumerator HealthChangeAnimation(Image healthImage)
	{	
		var progress = 0f;
		var speed = 1f / duration;
		while (progress < 1)
		{
			progress += Time.unscaledDeltaTime * speed;
			healthImage.color = Color.Lerp(fullColor, emptyColor, progress);

			if (progress < 0.5f)
			{
				// grow - 0..0.5 -> 0..1
				healthImage.rectTransform.localScale = Vector3.one * Mathf.Lerp(1f, 2f, progress / 0.5f);
			}
			else
			{
				// shrink - 0.5..1 -> 0..1
				healthImage.rectTransform.localScale = Vector3.one * Mathf.Lerp(2f, 1f, (progress - 0.5f) / 0.5f);
			}
			
			yield return null;
		}
	}
}
