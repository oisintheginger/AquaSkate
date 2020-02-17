using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triggerbuttonScript : MonoBehaviour
{
    [SerializeField] string triggerAxis;
    [SerializeField] bool isGrounded, isBraking, isBoosted;
    [SerializeField] Transform groundTransform;

    float storedMaxSpeed;

    Material pMat;

    private void Start()
    {
        storedMaxSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMotion>().maxSpeed;
        pMat = GetComponent<Material>();
    }

    private void Update()
    {

        GroundCheck();


        if (Input.GetAxis(triggerAxis) > 0)
        {
            
            if (!isGrounded)
            {
                RaycastHit hit;
                if(Physics.Raycast(groundTransform.position, -transform.up, out hit, 3f))
                {
                    if (hit.collider.gameObject.tag == "Ground")
                    {
                        var pM = gameObject.GetComponent<playerMotion>();
                        var pR = gameObject.GetComponent<Rigidbody>();
                        
                        pM.maxSpeed *= 2;
                        pR.AddForce(gameObject.transform.forward * pM.boostForce, ForceMode.VelocityChange);
                        StartCoroutine(boostTime());
                    }
                }
                
                
            }
        }

        
    }

    IEnumerator boostTime()
    {

        yield return new WaitForSeconds(2f);
        Debug.Log("Got here");
        gameObject.GetComponent<playerMotion>().maxSpeed = storedMaxSpeed;


    }

    void GroundCheck()
    {
        RaycastHit rH;
        if (Physics.Raycast(groundTransform.position, -transform.up, out rH, 0.5f))
        {
            if (rH.collider.gameObject.tag == "Ground")
            {
                isGrounded = true;

            }


        }
        else
        {
            isGrounded = false;
        }
    }
}
