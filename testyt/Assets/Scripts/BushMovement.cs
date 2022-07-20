using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    Vector2 movement;

    private Rigidbody2D rb;
    private Animator animator;
    SpriteRenderer sr;

    private bool SideTrigger = false; 
    private bool HideState = false;   
    private bool SpaceOrder = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetBool("HideBushTrigger", HideState);

        if (Input.GetKeyDown("d"))
            SideTrigger = true;
        else if (Input.GetKeyDown("a"))
            SideTrigger = false;

        if (Input.GetKeyDown("space") && !SpaceOrder)
        {
            HideState = true;
            SpaceOrder = true;
        }
        else if (Input.GetKeyDown("space") && SpaceOrder)
        {
            HideState = false;
            SpaceOrder = false;
        }
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        sr.flipX = SideTrigger == true ? true : false;

        if (HideState)
            moveSpeed = 1.5f;
        else
            moveSpeed = 5f;
    }
}
