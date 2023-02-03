using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class animationTrigger : MonoBehaviour {
    public Animator anim;
    void Start () {
        anim = GetComponent<Animator>();
    }
    void Update () {
        if (Input.GetKeyDown("1"))
        {
            anim.Play("startRotation");
        }
    }
};