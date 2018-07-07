using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

	[SerializeField]
	private CinemachineVirtualCamera tankCam;

	[SerializeField]
	private CinemachineVirtualCamera topdownCam;

	private void Start()
	{
		tankCam.Priority = 1;
		topdownCam.Priority = 10;
	}

	private void Update()
	{
		if (Input.anyKeyDown && topdownCam.Priority == 10)
		{
			tankCam.Priority = 99;
			topdownCam.Priority = 1;
		}
	}
}
