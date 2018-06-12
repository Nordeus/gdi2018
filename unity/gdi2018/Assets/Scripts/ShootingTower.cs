using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingTower : MonoBehaviour
{

	[SerializeField]
	private float towerRotationSpeed;

	private Tank targetTank;

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
	}

}
