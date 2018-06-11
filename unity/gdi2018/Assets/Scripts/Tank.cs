using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{

	private float speed = 0.5f;
	private float rotationSpeed = 5f;
	
	private void Awake()
	{
		Debug.Log("Awake " + gameObject.name);
	}

	private void Start()
	{
		Debug.Log("Start " + gameObject.name);
	}

	private void Update()
	{
		//Debug.Log("Update " + transform.position);

		if (Input.GetKey(KeyCode.W))
		{
			transform.position += transform.forward * speed;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			transform.position -= transform.forward * speed;
		}
		
		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(transform.up, rotationSpeed);
		}
		else if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(transform.up, -rotationSpeed);
		}
		
	}

	private void OnDestroy()
	{
		Debug.Log("OnDestroy");
	}
}
