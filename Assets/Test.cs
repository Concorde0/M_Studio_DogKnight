using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public Animator anim;
    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            Debug.Log("W");
            anim.SetBool("RunW",true);
            anim.SetBool("RunA", false);
            anim.SetBool("RunS", false);
            anim.SetBool("RunD", false);
            anim.SetBool("Idle", false);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            anim.SetBool("RunA", true);
            anim.SetBool("RunW", false);
            anim.SetBool("RunS", false);
            anim.SetBool("RunD", false);
            anim.SetBool("Idle", false);
        }
        else if (Input.GetKey(KeyCode.S))
        {
            anim.SetBool("RunS", true);
            anim.SetBool("RunW", false);
            anim.SetBool("RunA", false);
            anim.SetBool("RunD", false);
            anim.SetBool("Idle", false);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            anim.SetBool("RunD", true);
            anim.SetBool("RunW", false);
            anim.SetBool("RunA", false);
            anim.SetBool("RunS", false);
            anim.SetBool("Idle", false);
        }
        else
        {
            anim.SetBool("Idle", true);
        }
    }
}
