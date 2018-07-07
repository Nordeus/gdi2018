using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

	[SerializeField]
	private Transform tankTransform;

	[SerializeField]
	private float lerpSpeed = 10f;

	private Vector3 initialOffset;
	private Vector3 initialRotation;
	
	void Start ()
	{
		initialOffset = transform.position - tankTransform.position;
		initialRotation = transform.rotation.eulerAngles;
	}
	
	void Update()
	{
		var targetPosition = tankTransform.position + tankTransform.rotation * initialOffset;
		transform.position = Vector3.Lerp(transform.position, targetPosition, lerpSpeed * Time.deltaTime);

		var targetRotation = Quaternion.Euler(initialRotation.x, tankTransform.rotation.eulerAngles.y, initialRotation.z);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, lerpSpeed * Time.deltaTime);
	}
}
