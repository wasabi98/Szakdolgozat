using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	[SerializeField] private float damping;
	public Transform target;
	private Vector3 velocity = Vector3.zero;

	private void FixedUpdate()
	{
		Vector3 targetPosition = target.position;
		targetPosition.z = -10;

		transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, damping);

	}
}
