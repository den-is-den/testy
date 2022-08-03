using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Bush : MonoBehaviour
{
    private Animator animator;
    PhotonView view;
    public bool newBush = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        view = GetComponent<PhotonView>();
    }
    void Update()
    {
        if (view.IsMine)
        {
            if (newBush)
                animator.SetBool("BushSwap", true);
            else
                animator.SetBool("BushSwap", false);
        }
    }
}
