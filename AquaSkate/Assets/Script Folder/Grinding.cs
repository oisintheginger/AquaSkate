using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grinding : MonoBehaviour
{
    public Rigidbody myRb;
    [SerializeField] Transform startRail, endRail;

    [SerializeField] string jumpAxis;
    [SerializeField] float jumpForce, grindTimer, storedMaxSpeed;
    [SerializeField] float grindingSpeed, boostSpeed;

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

            //myRb.AddForce(transform.forward * 1f, ForceMode.Impulse);
            myRb.velocity = transform.forward * grindingSpeed;
            if (Input.GetAxis(jumpAxis) > 0.9f)
            {

                myRb.AddForce(transform.up * jumpForce, ForceMode.VelocityChange);
                isGrinding = false;
                myRb.useGravity = true;
                EndGrind();
            }
        }


    }

    void EndGrind()
    {
        if(isGrinding)
        {
            isGrinding = false;
        }

        var pM = gameObject.GetComponent<playerMotion>();
        var pR = gameObject.GetComponent<Rigidbody>();

        pM.maxSpeed *= 1.5f;
        pR.AddForce(gameObject.transform.forward * boostSpeed, ForceMode.VelocityChange);
        StartCoroutine(boostTime());
        myRb.useGravity = true;
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
        if(!isGrinding && myRb.velocity.y < -0.2f)
        {
            if(other.gameObject.tag == "Rail")
            {
                isLanding = true;
                startRail = other.transform;
                endRail = other.transform.GetChild(0);
                myRb.velocity = Vector3.zero;
                myRb.useGravity = false;
            }
        }

        if (other.gameObject.tag == "EndRail")
        {
            EndGrind();
        }


    }
}
