using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinding : MonoBehaviour
{
    public Rigidbody myRb;
    [SerializeField] Transform startRail, endRail;

    [SerializeField] string jumpAxis;
    [SerializeField] float jumpForce, grindTimer, storedMaxSpeed; 

    playerMotion motionScript;


    public bool isGrinding = false, isLanding = false;

        

    private void Start()
    {
        myRb = GetComponent<Rigidbody>();
        storedMaxSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMotion>().maxSpeed;
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

            

            isLanding = false;
            isGrinding = true;

        }

        
        if (isGrinding)
        {
            motionScript.enabled = false;

            grindTimer += Time.deltaTime;

            transform.LookAt(new Vector3(endRail.position.x, transform.position.y, endRail.position.z));

            myRb.AddForce(transform.forward * 1f, ForceMode.Impulse);

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
            //myRb.velocity = Vector3.zero;
            isGrinding = false;
        }

        var pM = gameObject.GetComponent<playerMotion>();
        var pR = gameObject.GetComponent<Rigidbody>();

        pM.maxSpeed *= 2;
        pR.AddForce(gameObject.transform.forward * pM.boostForce, ForceMode.VelocityChange);
        StartCoroutine(boostTime());

        motionScript.enabled = true;
    }

    IEnumerator boostTime()
    {
        if (grindTimer >= 1.5f)
        {
            yield return new WaitForSeconds(3f);
        }

        if(grindTimer < 1.5f)
        {
            yield return new WaitForSeconds(1.5f);
        }
        
        Debug.Log("Got here");
        gameObject.GetComponent<playerMotion>().maxSpeed = storedMaxSpeed;


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
