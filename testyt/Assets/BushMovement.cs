using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushMovement : MonoBehaviour
{
    private float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    private bool SideTrigger = false;
    private bool HideState = false;
    private bool SpaceOrder = false;
    SpriteRenderer sr;

    Vector2 movement;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);
        //animator.SetBool("SideTrigger", SideTrigger);
        animator.SetBool("HideBushTrigger", HideState);
        if (Input.GetKeyDown("d"))
        {
            SideTrigger = true;
        }
        else if (Input.GetKeyDown("a"))
        {
            SideTrigger = false;
        }
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
        /*if (movement.x > 0)
        {
            SideTrigger = true;
            Debug.Log("true");
        }
        else
        {
            SideTrigger = false;
            Debug.Log("false");
        } */
        if (HideState)
        {
            moveSpeed = 1.5f;
        }
        else
        {
            moveSpeed = 5f;
        }
    }
}
