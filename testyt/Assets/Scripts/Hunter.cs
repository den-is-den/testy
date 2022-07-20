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
    private Rigidbody2D rb;
    private Animator animator;
    SpriteRenderer sr;

    private bool SideTrigger = false;   //флаг на переключение (вправо/влево)
    private bool HiderFind = false;     //флаг на (увидел/не увидел) Hider

    void Start()
    {
        moveSpeed = startSpeed;
        hider = GameObject.FindGameObjectWithTag("Hider");
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
    }
    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        sr.flipX = SideTrigger == true ? true : false;

        if (HiderFind)
            moveSpeed = (2f) * startSpeed;
        else
            moveSpeed = startSpeed;
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