using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UnityEngine;

public class EnemyBehaviour : CharacterBody
{
    [SerializeField]
    public Transform target;
	[SerializeField]
	public float detectionRange = 15.0f;
    [SerializeField]
    public float shootingCooldown = 0.7f;
    private bool tempShootingBool = true;
	[SerializeField]
	private List<Vector3> path = null;
	[SerializeField]
	public GameObject drop;
	

	private int pathIndex = 0;
	
	private void Start()
	{
		target = GameObject.Find("Player").transform;
		animator = GetComponent<Animator>();
	}
	private void Highlight()
	{
		if(Vector3.Distance(transform.position, target.position) < detectionRange )
		{
			StartCoroutine(Pathfinder.HighlightPath(transform, target));
		}
	}
			
	void Update()
    {
		//Pathfinder.HighlightPath(transform, target);
		if(Vector3.Distance(transform.position, target.position) < detectionRange)
		{
			if(path != null && path.Count > 0)
			{
				if(Vector3.Distance(path.ElementAt(path.Count - 1), target.position) > 5.0f)
				{
					path = Pathfinder.GetPath(transform, target);
					pathIndex = 0;
					moveDirection = (path.ElementAt(Mathf.Min(pathIndex, path.Count - 1)) - transform.position).normalized;
					
				}
			}
			else
			{
				path = Pathfinder.GetPath(transform, target);
				pathIndex = 0;
				moveDirection = (path.ElementAt(Mathf.Min(pathIndex, path.Count - 1)) - transform.position).normalized;
				
			}
		}
		if(health <= 0)
		{
			GameObject.Find("Manager").GetComponent<Manager>().EnemyDied(gameObject);
			if (drop)
			{
				Instantiate(drop, transform.position, Quaternion.Euler(0, 0, 0));
			}
			Destroy(gameObject);
		}
	}
	private void FixedUpdate()
	{
		Move();

		GetComponent<Shooting>().RotateFiringPoint(this, target.position);
		if (tempShootingBool)
		{
			StartCoroutine(ShootingCoroutine());
		}
		if (moveDirection != Vector2.zero)
		{

			animator.Play("walk");
			animator.SetFloat("xMove", moveDirection.x);
			animator.SetFloat("yMove", moveDirection.y);
		}
		else
		{
			animator.Play("idle");
		}

	}

	private void Move()
	{
		if (path != null && path.Count > 0)
		{
			moveDirection = (path.ElementAt(Mathf.Min(pathIndex, path.Count - 1)) - transform.position).normalized;
			if (Vector3.Distance(transform.position, path.ElementAt(Mathf.Min(pathIndex, path.Count - 1))) < 0.1f)
			{
				pathIndex++;
			}
		}

		Vector3 move = (moveDirection * 3);
		Debug.DrawLine(transform.position, transform.position + move, Color.yellow);
		if (path != null && path.Count > 0)
			Debug.DrawLine(path.ElementAt(0), path.ElementAt(Mathf.Min(pathIndex, path.Count - 1)), Color.red);

		
		rb.velocity = moveDirection * movementSpeed;
	}

	IEnumerator ShootingCoroutine()
    {
		if (Vector3.Distance(transform.position, target.position) < detectionRange)
		{
			GetComponent<Shooting>().Shoot();
		}
		tempShootingBool = false;
				    
		yield return new WaitForSeconds(shootingCooldown);
		tempShootingBool = true;

	}

	public override void Damage(int damage)
	{
		health -= damage;
	}
}
