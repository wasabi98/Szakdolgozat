using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    
    public Rigidbody2D rb;

    private Vector2 moveDirection;
    
    private bool canDash = true;
    private bool isDashing;
    public float dashPower;
    public float dashTime;
    public float dashCooldown;
    
    
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
        if (isDashing)
        {
            return;
        }
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
       
        moveDirection = new Vector2(moveX, moveY).normalized;
        
        if(Input.GetMouseButtonDown(1) && canDash)
        {
            StartCoroutine(DashCoroutine(moveDirection));
        }

       

        
    }

    IEnumerator DashCoroutine(Vector2 direction)
    {
        canDash = false;
        isDashing = true;
        

        rb.velocity = direction * dashPower;
        

        yield return new WaitForSeconds(dashTime);

        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);  

        canDash = true;
    }

    void Move()
    {
        if (!isDashing)
        {
            rb.velocity = moveDirection * playerSpeed;
        }
    }
}
