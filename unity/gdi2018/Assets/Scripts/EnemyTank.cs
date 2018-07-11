using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyTank : MonoBehaviour
{

	private NavMeshAgent myAgent;

	[SerializeField]
	private Transform playerTank;

	[SerializeField]
	private float refreshTime = 1f;
	
	private float timeToRefresh;
	
	// Use this for initialization
	void Start ()
	{
		myAgent = GetComponent<NavMeshAgent>();

		StartCoroutine(DoWork());
	}

	private IEnumerator DoWork()
	{
		while (true)
		{
			myAgent.SetDestination(playerTank.position);
			yield return new WaitForSeconds(refreshTime);
		}
	}
}
