using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	private bool exploding;
	private float timeToDestroy;
	
	private void OnTriggerEnter(Collider other)
	{
		Debug.LogWarning("TRIGGER " + other.gameObject.tag);
	}

	private void OnCollisionEnter(Collision other)
	{
		Debug.LogWarning("COLLISION " + other.gameObject.tag);
		if (exploding)
			return;
		
		if (other.gameObject.CompareTag("Destructible"))
		{
			Destroy(other.gameObject);
		}
		
		// fire explosion and explode
		GetComponentInChildren<ParticleSystem>().Play();
		timeToDestroy = 0.5f;
		exploding = true;
	}

	private void Update()
	{
		if (exploding)
		{
			timeToDestroy -= Time.deltaTime;
			if (timeToDestroy <= 0f)
			{
				Destroy(gameObject);
			}
		}
	}
}
