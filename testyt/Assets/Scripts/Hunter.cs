using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hunter : MonoBehaviour
{
    public float startSpeed = 5f;       //начальная скорость 
    private float moveSpeed;            //скорость
    Vector2 movement;                   //вектор координат

    public Camera cam;
    private GameObject hider;
    private GameObject bush;

    private Rigidbody2D rb;
    private Animator animator;
    SpriteRenderer sr;

    private bool SideTrigger = false;   //флаг на переключение (вправо/влево)
    private bool HiderFind = false;     //флаг на (увидел/не увидел) Hider
    private bool InBush = false;        //флаг на (находится/не находится) в кусте

    void Start()
    {
        moveSpeed = startSpeed;
        hider = GameObject.FindGameObjectWithTag("Hider");
        bush = GameObject.FindGameObjectWithTag("Bush");
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetFloat("MoSpeed", movement.sqrMagnitude);
        animator.SetBool("SpeedRun", HiderFind);

        if (Input.GetKeyDown("d"))
            SideTrigger = true;
        else if (Input.GetKeyDown("a"))
            SideTrigger = false;

        if (IsVisible(cam, hider))
            HiderFind = true;
        else
            HiderFind = false;

        if (HiderFind)
            moveSpeed = Mathf.Lerp(moveSpeed, startSpeed * (2f), 0.006f);
        else
            moveSpeed = Mathf.Lerp(moveSpeed, startSpeed, 0.006f);

    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        sr.flipX = SideTrigger == true ? true : false;


    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bush")
            InBush = true;    
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bush")
            InBush = false;
    }
    private bool IsVisible(Camera camera, GameObject target)
    {
        var planes = GeometryUtility.CalculateFrustumPlanes(camera);
        var point = target.transform.position;

        foreach (var plane in planes)
            if (plane.GetDistanceToPoint(point) < 0)
                return false;
        return true;
    }
}