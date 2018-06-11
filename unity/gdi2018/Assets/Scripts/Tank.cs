using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{

	[SerializeField]
	private float speed = 6f;
	
	[SerializeField]
	private float rotationSpeed = 50f;

	private Rigidbody rigidbody;

	private bool pressedForward;
	private bool pressedLeft;
	private bool pressedBack;
	private bool pressedRight;
	
	private void Awake()
	{
		Debug.Log("Awake " + gameObject.name);

		rigidbody = GetComponent<Rigidbody>();
	}

	private void Start()
	{
		Debug.Log("Start " + gameObject.name);
	}

	private void Update()
	{
		// we check for input in every frame
		pressedForward = Input.GetKey(KeyCode.W);
		pressedBack = Input.GetKey(KeyCode.S);
		pressedRight = Input.GetKey(KeyCode.D);
		pressedLeft = Input.GetKey(KeyCode.A);
	}

	private void FixedUpdate()
	{
		//Debug.Log("Update " + transform.position);

		// if the input is ok, move the physics
		if (pressedForward)
		{
			rigidbody.AddForce(transform.forward * speed * Time.deltaTime);
		}
		else if (pressedBack)
		{
			rigidbody.AddForce(-transform.forward * speed * Time.deltaTime);
		}
		
		if (pressedRight)
		{
			transform.Rotate(transform.up, rotationSpeed * Time.deltaTime);
		}
		else if (pressedLeft)
		{
			transform.Rotate(transform.up, -rotationSpeed * Time.deltaTime);
		}
		
	}

	private void OnDestroy()
	{
		Debug.Log("OnDestroy");
	}
}
