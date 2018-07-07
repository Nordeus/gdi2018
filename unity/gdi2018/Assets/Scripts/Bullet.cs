using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
			var tank = other.gameObject.GetComponent<Tank>();
			if (tank != null)
			{
				tank.OnHit();
			}
			else
			{
				other.gameObject.SetActive(false);
				FindObjectOfType<NavMeshSurface>().BuildNavMesh();
			}
		}
		
		// fire explosion and explode
		GetComponentInChildren<ParticleSystem>().Play();
		GetComponent<Rigidbody>().velocity = Vector3.zero;
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
