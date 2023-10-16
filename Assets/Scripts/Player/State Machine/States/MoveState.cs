using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MoveState : PlayerState
{

    public override PlayerState ProcessInput(PlayerMovement sender)
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        sender.moveDirection = new Vector2(moveX, moveY).normalized;
        if (Input.GetMouseButtonDown(1) && sender.canDash)
        {
            return new DashState();
        }
        else return null;
    }

    public override void Update(PlayerMovement sender)
    {
        
    }

    public override void FixedUpdate(PlayerMovement sender)
    {
        sender.rb.velocity = sender.moveDirection * sender.playerSpeed;
    }

    public override void Enter(PlayerMovement sender)
    {

    }
    public override void Exit(PlayerMovement sender)
    {
        
    }
}
