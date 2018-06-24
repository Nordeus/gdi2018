using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{

	[SerializeField]
	private float moveSpeed = 30f;
	
	[SerializeField]
	private float rotateSpeed = 50f;
	
	[SerializeField]
	private GameObject bulletPrefab;

	[SerializeField]
	private Transform fireAtTransform;

	[SerializeField]
	private float shootingForce;

	[SerializeField]
	private float reloadTime = 1f;

	private Rigidbody myRigidbody;

	private bool moveForward;
	private bool moveBackward;

	private float timeToReload;
	
	private void Awake()
	{
		Debug.LogError("AWAKE");
		myRigidbody = GetComponent<Rigidbody>();
	}
	
	
	private void Start()
	{
		Debug.LogError("START");
	}

	private void Update()
	{
		moveForward = Input.GetKey(KeyCode.W);
		moveBackward = Input.GetKey(KeyCode.S);

		if (Input.GetKey(KeyCode.A))
		{
			transform.Rotate(Vector3.up, -rotateSpeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.D))
		{
			transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
		}

		if (timeToReload > 0)
		{
			timeToReload -= Time.deltaTime;
		}
		
		
		if (Input.GetKeyDown(KeyCode.Space) && timeToReload <= 0)
		{
			Shoot();
		}
	}

	private void FixedUpdate()
	{
		if (moveForward)
		{
			myRigidbody.AddForce(transform.forward * moveSpeed * Time.deltaTime);
		}
		if (moveBackward)
		{
			myRigidbody.AddForce(-transform.forward * moveSpeed * Time.deltaTime);
		}
	}

	private void OnDestroy()
	{
		Debug.LogError("OnDestroy");
	}

	private void Shoot()
	{
		timeToReload = reloadTime;
		
		var bullet = Instantiate(bulletPrefab);
		bullet.transform.position = fireAtTransform.position;
		bullet.transform.rotation = transform.rotation;
		
		bullet.GetComponent<Rigidbody>().AddForce(shootingForce * transform.forward);
	}
}
