using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : CharacterBody
{
    
    private PlayerState playerMovementState =  new MoveState();

    [SerializeField]
    public Camera cam;
	

	[HideInInspector]
	public bool canDash = true;
	[SerializeField]
	public float dashPower;
	[SerializeField]
	public float dashTime;
    [SerializeField]
    public float dashCooldown;
    
    
    void Update()
    {
        PlayerState state = playerMovementState.ProcessInput(this);
        if(state != null)
        {
            playerMovementState.Exit(this);
            playerMovementState = state;
            playerMovementState.Enter(this);
        }    
        
    }
    void FixedUpdate()
    {
        playerMovementState.FixedUpdate(this);
        
    }
   
    public void Dash()
    {
        StartCoroutine(DashCoroutine(moveDirection));
    }
    IEnumerator DashCoroutine(Vector2 direction)
    {
        canDash = false;

        rb.velocity = direction * dashPower;

        yield return new WaitForSeconds(dashTime);

        playerMovementState.Exit(this);
        playerMovementState = new MoveState();
        playerMovementState.Enter(this);

        yield return new WaitForSeconds(dashCooldown);  

        canDash = true;
    }

    
}
