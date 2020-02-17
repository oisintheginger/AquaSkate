using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationControl : MonoBehaviour
{
    Animator myAnim;
    private void Awake()
    {
        myAnim = this.gameObject.GetComponent<Animator>();
    }
    private void Update()
    {
        
        var pM = this.transform.parent.gameObject.GetComponent<playerMotion>();
        Debug.Log(Input.GetAxis(pM.horizontalAxis));
        if(Input.GetButtonDown(pM.jumpAxis)&&pM.isGrounded)
        {
            myAnim.SetTrigger("Jump");
        }
        if(Input.GetAxis(pM.brakeAxis) > 0)
        {
            myAnim.SetBool("IsBraking", true);
        }
        else
        {
            myAnim.SetBool("IsBraking", false);
        }

        float turning = Input.GetAxisRaw(pM.horizontalAxis);
        myAnim.SetFloat("Speed", pM.xZPlaneSpeed / pM.maxSpeed);
        myAnim.SetFloat("Turn", turning);
        myAnim.SetBool("Grounded", pM.isGrounded);
    }
}
