using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	[SerializeField]
	private Transform tankTransform;

	[SerializeField]
	private float lerpingSpeed;

	private Vector3 initialPositionOffset;
	private Vector3 initialRotation;

	private void Start()
	{
		initialPositionOffset = transform.position - tankTransform.position;
		initialRotation = transform.rotation.eulerAngles;
	}

	private void LateUpdate()
	{
		var targetPosition = tankTransform.position + tankTransform.rotation * initialPositionOffset;
		transform.position = Vector3.Lerp(transform.position, targetPosition, lerpingSpeed * Time.deltaTime);

		var targetRotation = Quaternion.Euler(initialRotation.x, tankTransform.rotation.eulerAngles.y, initialRotation.z);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpingSpeed * Time.deltaTime);
	}
}
