using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveState : PlayerState
{
    private Vector2 mousePos;

	public override PlayerState ProcessInput(PlayerMovement sender)
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        sender.moveDirection = new Vector2(moveX, moveY).normalized;

        mousePos = sender.cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonDown(1) && sender.canDash)
        {
            return new DashState();
        }
        if (Input.GetButtonDown("Fire1"))
		{
            
			sender.GetComponent<Shooting>().Shoot();
		}
		return null;
    }

    public override void Update(PlayerMovement sender)
    {
        
    }

    public override void FixedUpdate(PlayerMovement sender)
    {
        sender.rb.velocity = sender.moveDirection * sender.movementSpeed;

        sender.GetComponent<Shooting>().RotateFiringPoint(sender, mousePos);
        
    }

    public override void Enter(PlayerMovement sender)
    {

    }
    public override void Exit(PlayerMovement sender)
    {
        
    }
}
