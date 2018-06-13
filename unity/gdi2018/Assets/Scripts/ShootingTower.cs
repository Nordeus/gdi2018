using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTower : MonoBehaviour
{

	[SerializeField]
	private float towerRotationSpeed;

	[SerializeField]
	private float reloadDuration;
	
	[SerializeField]
	private float shootingForce;

	[SerializeField]
	private Transform shootingFrom;

	[SerializeField]
	private LayerMask shootingLayerMask;

	private float timeToReload;

	private Tank targetTank;

	private void Awake()
	{
		timeToReload = 0f;
	}

	private void Start()
	{
		targetTank = FindObjectOfType<Tank>();
	}

	private void Update()
	{
		if (targetTank == null)
			return;

		var towerToTank = targetTank.transform.position - transform.position;
		towerToTank.y = 0f;
		var rotation = Quaternion.LookRotation(towerToTank);
		
		transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * towerRotationSpeed);

		if (timeToReload > 0)
		{
			timeToReload -= Time.deltaTime;
		}
		
		Debug.DrawRay(transform.position, transform.forward * 50f, Color.red);

		if (timeToReload <= 0)
		{
			// check if we can see the tank
			RaycastHit hit;
			if (Physics.Raycast(transform.position, transform.forward, out hit, 100f, shootingLayerMask))
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
		timeToReload = reloadDuration;
		
		var bulletGameobject = Instantiate(Resources.Load("Prefabs/CompleteShell")) as GameObject;
		
		bulletGameobject.transform.position = shootingFrom.position;
		bulletGameobject.transform.forward = transform.forward;
		bulletGameobject.GetComponent<Rigidbody>().AddForce(shootingForce * bulletGameobject.transform.forward);
	}

}
