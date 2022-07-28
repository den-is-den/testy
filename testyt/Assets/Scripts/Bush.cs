using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (Global.newBush)
            animator.SetBool("BushSwap", true);
        else
            animator.SetBool("BushSwap", false);
    }
}
