using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DashState : PlayerState
{
    public override void Enter(PlayerMovement sender)
    {
        sender.Dash();
    }

    public override void Exit(PlayerMovement sender)
    {
        
    }

    public override void FixedUpdate(PlayerMovement sender)
    {
        
    }

    public override PlayerState ProcessInput(PlayerMovement sender)
    {
        return null;
    }

    public override void Update(PlayerMovement sender)
    {
        
    }
    

}
