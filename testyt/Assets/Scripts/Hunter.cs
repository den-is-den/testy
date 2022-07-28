using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Hunter : MonoBehaviour
{
    public float startSpeed = 5f;
    PhotonView view;                    //начальная скорость 
    public float timeSpin = 5f;         //время поиска в кусте 
    private float moveSpeed;            //скорость
    Vector2 movement;                   //вектор координат
    public bool facingRight = false;

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
        view = GetComponent<PhotonView>();
        //sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (view.IsMine)
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            animator.SetFloat("MoSpeed", movement.sqrMagnitude);
            animator.SetBool("SpeedRun", HiderFind);

            /*if (Input.GetKeyDown("d"))
                SideTrigger = true;
            else if (Input.GetKeyDown("a"))
                SideTrigger = false;*/

            if (IsVisible(cam, hider))
                HiderFind = true;
            else
                HiderFind = false;

            if (HiderFind)
                moveSpeed = Mathf.Lerp(moveSpeed, startSpeed * (2f), 0.006f);
            else
                moveSpeed = Mathf.Lerp(moveSpeed, startSpeed, 0.006f);

            if (Input.GetKeyDown("space") && InBush)
            {
                //sr.sortingOrder = 1;
                rb.constraints = RigidbodyConstraints2D.FreezeAll;
                animator.SetBool("Seak", true);
                Invoke("SeakAnimEnd", timeSpin);
            }
        }
    }
    void FixedUpdate()
    {
        if (view.IsMine)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
            //sr.flipX = SideTrigger == true ? true : false;

            float h = Input.GetAxis("Horizontal");
            if (h > 0 && !facingRight)
                Flip();
            else if (h < 0 && facingRight)
                Flip();
        }
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

    private void SeakAnimEnd()
    {
        animator.SetBool("Seak", false);
        //sr.sortingOrder = 0;
        rb.constraints = RigidbodyConstraints2D.None;
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