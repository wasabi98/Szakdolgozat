using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : CharacterBody
{
    [SerializeField]
    private Transform target;
    [SerializeField]
    public float shootingCooldown = 0.7f;
    private bool tempShootingBool = true;

    // Update is called once per frame
    void Update()
    {
        
    }
	private void FixedUpdate()
	{
		GetComponent<Shooting>().RotateFiringPoint(this, target.position);
		if (tempShootingBool)
		{
			StartCoroutine(ShootingCoroutine());
		}
	}
	IEnumerator ShootingCoroutine()
    {
        
		GetComponent<Shooting>().Shoot();
        tempShootingBool = false;
				    
		yield return new WaitForSeconds(shootingCooldown);
		tempShootingBool = true;
	}
}
