using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    
    public Rigidbody2D rb;

    private PlayerState playerMovementState =  new MoveState();

    public Vector2 moveDirection;
    
    public bool canDash = true;
    public float dashPower;
    public float dashTime;
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
