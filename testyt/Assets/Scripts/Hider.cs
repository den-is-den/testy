using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Hider : MonoBehaviour
{
    public  float startSpeed = 5f;
    PhotonView view;                    //начальная скорость 
    private float moveSpeed;            //скорость
    Vector2 movement;                   //вектор координат

    private Rigidbody2D rb;
    private Animator animator;
    //SpriteRenderer sr;
    public bool facingRight = false;

    //private bool SideTrigger = false;   //флаг на переключение (вправо/влево)
    private bool HideState = false;     //флаг на (спрятался/не спрятался) в куст

    void Start()
    {
        moveSpeed = startSpeed;
        view = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
        if (view.Owner.IsLocal)
        {
            Camera.main.GetComponent<CameraFollow>().player = gameObject.transform;
        }
    }
    void Update()
    {
        if (view.IsMine)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("Speed", movement.sqrMagnitude);
            animator.SetBool("HideBushTrigger", HideState);

            /*if (Input.GetKeyDown("d"))
                SideTrigger = true;
            else if (Input.GetKeyDown("a"))
                SideTrigger = false;*/

            if (Input.GetKeyDown("space"))
                HideState = !HideState;
        }
    }
    void FixedUpdate()
    {
        if (view.IsMine)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            //sr.flipX = SideTrigger == true ? true : false;
            if (HideState)
                moveSpeed = (3f / 10) * startSpeed;
            else
                moveSpeed = startSpeed;

            float h = Input.GetAxis("Horizontal");
            if (h > 0 && !facingRight)
                Flip();
            else if (h < 0 && facingRight)
                Flip();
        }
    }
    void Flip()
    {
        if (view.IsMine)
        {
            facingRight = !facingRight;
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }
}
