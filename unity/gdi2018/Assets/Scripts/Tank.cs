using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
	private void Awake()
	{
		Debug.Log("Awake " + gameObject.name);
	}

	private void Start()
	{
		Debug.Log("Start " + gameObject.name);
	}

	private void Update()
	{
		Debug.Log("Update " + transform.position);
	}

	private void OnDestroy()
	{
		Debug.Log("OnDestroy");
	}
}
