using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Cinemachine;
using UnityEngine;

public class CameraManager : MonoBehaviour
{

    [SerializeField]
    private CinemachineVirtualCamera tankCamera;
    
    [SerializeField]
    private CinemachineVirtualCamera topDownCamera;

    private void Awake()
    {
        topDownCamera.Priority = 100;
        tankCamera.Priority = 10;
    }

    private void Update()
    {
        if (Input.anyKeyDown && topDownCamera.Priority == 100)
        {
            topDownCamera.Priority = 10;
            tankCamera.Priority = 100;
        }
    }
}
