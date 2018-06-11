using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{

	[SerializeField]
	private float speed = 6f;
	
	[SerializeField]
	private float rotationSpeed = 50f;
	
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
			transform.position += transform.forward * speed * Time.deltaTime;
		}
		else if (Input.GetKey(KeyCode.S))
		{
			transform.position -= transform.forward * speed * Time.deltaTime;
		}
		
		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);
		}
		else if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(transform.up, -rotationSpeed * Time.deltaTime);
		}
		
	}

	private void OnDestroy()
	{
		Debug.Log("OnDestroy");
	}
}
