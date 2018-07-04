using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTank : MonoBehaviour
{

	[SerializeField]
	private NavMeshAgent agent;
	
	[SerializeField]
	private Tank tank;

	[SerializeField]
	private float refreshTime = 0.5f;

	private float timeToRefresh;

	private void Update()
	{
		if (timeToRefresh >= 0)
		{
			timeToRefresh -= Time.deltaTime;
			if (timeToRefresh <= 0)
			{
				timeToRefresh = refreshTime;
				agent.SetDestination(tank.transform.position);
			}
		}
	}
}
