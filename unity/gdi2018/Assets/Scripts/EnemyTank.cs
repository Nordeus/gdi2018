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
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (timeToRefresh >= 0)
		{
			timeToRefresh -= Time.deltaTime;
			if (timeToRefresh < 0)
			{
				myAgent.SetDestination(playerTank.position);
				timeToRefresh = refreshTime;
			}
		}
	}
}
