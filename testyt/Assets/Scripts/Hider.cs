using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hider : MonoBehaviour
{
    public  float startSpeed = 5f;      //��������� �������� 
    private float moveSpeed;            //��������
    Vector2 movement;                   //������ ���������

    private Rigidbody2D rb;
    private Animator animator;
    SpriteRenderer sr;

    private bool SideTrigger = false;   //���� �� ������������ (������/�����)
    private bool HideState = false;     //���� �� (���������/�� ���������) � ����

    void Start()
    {
        moveSpeed = startSpeed;
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
        
        if (Input.GetKeyDown("space"))
            HideState = !HideState;
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        sr.flipX = SideTrigger == true ? true : false;

        if (HideState)
            moveSpeed = (3f / 10) * startSpeed;
        else
            moveSpeed = startSpeed;
    }
}
