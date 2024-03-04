using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBody : MonoBehaviour
{
	[SerializeField]
	public Rigidbody2D rb;
	[SerializeField]
	public float movementSpeed;
	[SerializeField]
	public Transform firePoint;
	[HideInInspector]
	public Vector2 moveDirection;
}
