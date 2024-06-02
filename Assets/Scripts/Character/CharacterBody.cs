using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterBody : MonoBehaviour
{
	[SerializeField]
	public Rigidbody2D rb;
	[SerializeField]
	public float movementSpeed;
	[SerializeField]
	public Transform fireAxis;
	[HideInInspector]
	public Vector2 moveDirection;
	[SerializeField]
	public int health;
	[SerializeField]
	public Animator animator;

	
	public abstract void Damage(int damage);
	
}
