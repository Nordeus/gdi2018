using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{

	[SerializeField]
	private float rotateSpeed;

	[SerializeField]
	private float reloadTime = 1f;

	[SerializeField]
	private Transform fireAt;
	
	[SerializeField]
	private float shootingForce = 1500f;

	[SerializeField]
	private LayerMask blockingLayers;

	private Tank tank;

	private float timeToReload;

	private void Start()
	{
		tank = FindObjectOfType<Tank>();

		timeToReload = reloadTime;
	}

	private void Update()
	{
		var towerToTank = tank.transform.position - transform.position;
		towerToTank.y = 0f;

		var rotation = Quaternion.LookRotation(towerToTank);

		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.deltaTime);

		Debug.DrawRay(fireAt.position, fireAt.forward * 1000, Color.red, 1f);

		
		
		if (timeToReload > 0f)
		{
			timeToReload -= Time.deltaTime;
		}
		
		if (timeToReload <= 0f)
		{
			RaycastHit hit;
			if (Physics.Raycast(fireAt.position, fireAt.forward, out hit, 1000f, blockingLayers))
			{
				if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Tank"))
				{
					Shoot();
				}
			}
		}
	}

	private void Shoot()
	{
		timeToReload = reloadTime;
		
		// make bullet
		var bulletPrefab = Resources.Load("Prefabs/CompleteShell") as GameObject;
		var bullet = Instantiate(bulletPrefab);
		bullet.transform.position = fireAt.position;
		bullet.transform.rotation = transform.rotation;
		
		bullet.GetComponent<Rigidbody>().AddForce(shootingForce * transform.forward);
	}
}
