using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class PlayerState
{
    abstract public void Enter(PlayerMovement sender);
    abstract public void Exit(PlayerMovement sender);
    abstract public PlayerState ProcessInput(PlayerMovement sender);
    abstract public void Update(PlayerMovement sender);
    abstract public void FixedUpdate(PlayerMovement sender);

}
