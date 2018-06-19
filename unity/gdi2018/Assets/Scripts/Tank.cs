using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{

	[SerializeField]
	private float speed = 6f;
	
	[SerializeField]
	private float rotationSpeed = 50f;
	
	[SerializeField]
	private float shootingForce;

	[SerializeField]
	private GameObject bulletPrefab;

	[SerializeField]
	private Transform shootingFrom;

	[SerializeField]
	private float reloadDuration;

	private Rigidbody myRigidbody;

	private bool pressedForward;
	private bool pressedLeft;
	private bool pressedBack;
	private bool pressedRight;

	private float timeToReload;
	
	private void Awake()
	{
		Debug.Log("Awake " + gameObject.name);

		myRigidbody = GetComponent<Rigidbody>();
		
		timeToReload = 0f;
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

		if (timeToReload > 0)
		{
			timeToReload -= Time.deltaTime;
		}
		
		if (Input.GetKeyUp(KeyCode.Space) && timeToReload <= 0)
		{
			Shoot();
		}
	}

	private void FixedUpdate()
	{
		//Debug.Log("Update " + transform.position);

		// if the input is ok, move the physics
		if (pressedForward)
		{
			myRigidbody.AddForce(transform.forward * speed * Time.deltaTime);
		}
		else if (pressedBack)
		{
			myRigidbody.AddForce(-transform.forward * speed * Time.deltaTime);
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

	private void Shoot()
	{
		timeToReload = reloadDuration;
		
		//var bulletGameobject = Instantiate(bulletPrefab);
		var bulletGameobject = Instantiate(Resources.Load("Prefabs/CompleteShell")) as GameObject;
		
		bulletGameobject.transform.position = shootingFrom.position;
		bulletGameobject.transform.forward = transform.forward;
		bulletGameobject.GetComponent<Rigidbody>().AddForce(shootingForce * bulletGameobject.transform.forward);
	}
}
