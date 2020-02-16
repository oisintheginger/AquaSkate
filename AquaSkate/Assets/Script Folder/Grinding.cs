using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinding : MonoBehaviour
{
    public Rigidbody myRb;
    [SerializeField] Transform startRail, endRail;

    [SerializeField] string jumpAxis;
    [SerializeField] float jumpForce; 

    playerMotion motionScript;


    public bool isGrinding = false, isLanding = false;

        

    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
        //startRail = GameObject.FindGameObjectWithTag("Rail").transform;
        motionScript = GetComponent<playerMotion>();
    }

    private void FixedUpdate()
    {
        if(isLanding)
        {
            myRb.velocity = Vector3.zero;
            float Dist = Vector3.Distance(transform.position, startRail.position);

            Debug.Log(Dist);
            myRb.MovePosition(new Vector3(startRail.position.x - Dist, startRail.position.y + 1.5f, startRail.position.z));

            transform.LookAt(endRail.position);

            isLanding = false;
            isGrinding = true;

        }

        
        if (isGrinding)
        {
            motionScript.enabled = false;
            
            myRb.AddForce(transform.forward * 5f, ForceMode.Impulse);

            if (Input.GetAxis(jumpAxis) > 0.9f)
            {

                myRb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
                isGrinding = false;
                EndGrind();
            }
        }


    }

    void EndGrind()
    {
        if(isGrinding)
        {
            myRb.velocity = Vector3.zero;
            isGrinding = false;
        }
        
        motionScript.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!isGrinding && myRb.velocity.y < 0)
        {
            if(other.gameObject.tag == "Rail")
            {
                isLanding = true;
                startRail = other.transform;
                endRail = other.transform.GetChild(0);
                
            }
        }

        if (other.gameObject.tag == "EndRail")
        {
            EndGrind();
        }


    }
}
