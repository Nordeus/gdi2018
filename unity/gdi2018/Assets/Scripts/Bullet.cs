using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
	
	private void OnTriggerEnter(Collider other)
	{
		Destroy(gameObject);
		//Destroy(other.gameObject);
		
		//if (other.gameObject.CompareTag("Destroyable"))
		//{
		//	other.gameObject.SetActive(false);
		//}
		if (other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
		{
			other.gameObject.SetActive(false);
		}
	}

	private void OnCollisionEnter(Collision other)
	{
		Debug.LogError("OnCollisionEnter");
	}
}
