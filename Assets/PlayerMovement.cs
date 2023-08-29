using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public float dashSpeed;
    public Rigidbody2D rb;

    private Vector2 moveDirection;
    private bool doDash = false;
    
    void Update()
    {
        ProcessInputs();
    }
    void FixedUpdate()
    {
        Move();
    }
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

        bool canDash = true;

        if(canDash && Input.GetMouseButtonDown(1))
        {
            doDash = true;
            canDash = false;
        }
        if(Input.GetMouseButtonUp(1))
        {
            canDash = true;
        }
    }
    void Move()
    {

        rb.velocity = moveDirection * playerSpeed;
        if (doDash)
        {
            rb.velocity *= dashSpeed;
            doDash = false;  
        }
    }
}
