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

	private void Start()
	{
		StartCoroutine(GoToTank());
	}

	private IEnumerator GoToTank()
	{
		while (true)
		{
			agent.SetDestination(tank.transform.position);
			yield return new WaitForSeconds(refreshTime);
		}
	}
}
