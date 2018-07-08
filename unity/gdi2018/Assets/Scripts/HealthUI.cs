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
	private float duration = 2f;

	private void Start()
	{
		tank.OnHealthChanged += OnHealthChanged;
		
		// refresh it immediately
		for (int i = 0; i < healthImages.Count; i++)
		{
			healthImages[i].color = fullColor;
		}
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.E))
		{
			StartCoroutine(HealthChangeAnimation(healthImages[2]));
		}
		else if (Input.GetKeyDown(KeyCode.R))
		{
			healthImages[2].color = fullColor;
		}
	}

	private void OnHealthChanged()
	{
		var healthImage = healthImages[tank.Health];
		//for (float i = 0; i < 1; i += 0.1f)
		//{
		//	healthImage.color = Color.Lerp(fullColor, emptyColor, i);
		//}
		StartCoroutine(HealthChangeAnimation(healthImage));
	}

	private IEnumerator HealthChangeAnimation(Image healthImage)
	{
		var progress = 0f;
		var speed = 1f / duration;
		while (progress < 1)
		{
			progress += Time.unscaledDeltaTime * speed;
			healthImage.color = Color.Lerp(fullColor, emptyColor, progress);
			if (progress < 0.5)
			{
				// grow
				healthImage.rectTransform.localScale = Vector3.one * Mathf.Lerp(1f, 1.3f, progress / 0.5f);
			}
			else
			{
				// shrink
				healthImage.rectTransform.localScale = Vector3.one * Mathf.Lerp(1.3f, 1f, (progress - 0.5f) / 0.5f);
			}
			// returns execution, waits for next frame
			yield return null;
		}
	}
}
