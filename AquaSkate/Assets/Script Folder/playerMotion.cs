using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMotion : MonoBehaviour
{
    [SerializeField] Quaternion sQ = Quaternion.identity;

    public int currentCheckPointTarget;
    public bool canPause = false, isPoweredUp= false;

    [SerializeField] string horizontalAxis, verticalAxis, jumpAxis, accelerateAxis, brakeAxis;
    [SerializeField] Image accelerometerBar;
    [SerializeField] Text checkPointUI;
    [SerializeField] int amountOfCheckpoints;

    Rigidbody pRB;
    [SerializeField] Vector3 appliedForce;
    public float maxSpeed, maxForce, boostForce, accelerationForce, brakeForce, xZPlaneSpeed, jumpForce, turnSpeed, turnScaler;
    [Range(0,5)] public float steeringScaler;
    [SerializeField] bool isGrounded;
    [SerializeField] Transform groundTransform, slopeTransform;

    [SerializeField] Vector3 rampNormal, rampHitPoint;

    //Testing Vectors for visualisation
    [SerializeField] Vector3 testVector, playerPlane, hitNormaltoPlane, targetDir;

    Ray groundCheckRay;
    Ray rampRay;
    private void Awake()
    {
        currentCheckPointTarget = 0;
        pRB = this.gameObject.GetComponent<Rigidbody>();
        pRB.drag = 0f;
        amountOfCheckpoints = GameObject.FindObjectsOfType<CheckpointScript>().Length;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
        SlopeCheck();
        groundCheckRay = new Ray(groundTransform.position, -transform.up);
        rampRay = new Ray(slopeTransform.position, transform.forward);
        
        Motion();
        checkPointUI.text = currentCheckPointTarget + "/" + amountOfCheckpoints;
        
    }
    
    void ScaleAccelerometer(float currentVelocity, float maxVelocity)
    {
        accelerometerBar.fillAmount = currentVelocity / maxVelocity;
    }

    void GroundCheck()
    {
        RaycastHit rH;
        if (Physics.Raycast(groundTransform.position, -transform.up, out rH, 0.5f))
        {
            if (rH.collider.gameObject.tag == "Ground")
            {
                 isGrounded =true;
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    void SlopeCheck()
    {
        RaycastHit rH;
        if (Physics.Raycast(groundCheckRay, out rH, 100f))
        {
            if (rH.collider.tag == "Ground")
            {
                float angle = Vector3.Angle(Vector3.up, rH.normal);
                Vector3 axis = Vector3.Cross(Vector3.up, rH.normal);
                sQ = Quaternion.AngleAxis(angle, axis);
                if(sQ.z>0||sQ.x>0)
                {
                    pRB.AddForce(transform.up * 0.5f, ForceMode.VelocityChange);
                }
            }
            
        }
    }

    void Motion()
    {
        pRB.drag = 0;
        
        var velocity = pRB.velocity;

        //turning
        float turning = Input.GetAxis(horizontalAxis);
        pRB.AddRelativeTorque(transform.up * turning * (turnSpeed*(maxSpeed/Mathf.Max(15f, velocity.magnitude*turnScaler))));


        xZPlaneSpeed = Mathf.Sqrt((velocity.x * velocity.x) + (velocity.z * velocity.z));

        Vector3 normalizedXZMovement = new Vector3(velocity.x / xZPlaneSpeed, velocity.y, velocity.z / xZPlaneSpeed);

        Vector3 xzSpeed = new Vector3(normalizedXZMovement.x * maxSpeed, velocity.y, normalizedXZMovement.z * maxSpeed);
       
        if (Input.GetButtonDown(accelerateAxis)&&isGrounded)
        {
            pRB.AddForce((transform.forward ) * accelerationForce *  Mathf.Max(0, Input.GetAxis(verticalAxis)), ForceMode.VelocityChange);

            /*
            pRB.AddForce((transform.up * Mathf.Abs(Mathf.Tan(sQ.x) * pRB.velocity.x * accelerationForce))
                        + (transform.up * Mathf.Abs(Mathf.Tan(sQ.z) * pRB.velocity.z * accelerationForce)), 
                        ForceMode.VelocityChange);
            */
        }
        if (xZPlaneSpeed >= maxSpeed)
        {
            pRB.velocity = xzSpeed;
        }

        if (Input.GetButton(brakeAxis)&&isGrounded)
        {
            pRB.AddForce(-pRB.velocity.normalized / Mathf.Max(0.1f, brakeForce));
        }
        if (Input.GetAxis(jumpAxis)>0.9f&&isGrounded)
        {
            
            pRB.AddForce(transform.up * jumpForce,ForceMode.VelocityChange);
        }

        pRB.velocity = Vector3.Lerp(pRB.velocity, transform.forward * xZPlaneSpeed, steeringScaler * Time.deltaTime);

        ScaleAccelerometer(xZPlaneSpeed, maxSpeed);
    }

    private void OnDrawGizmos()
    {
       
    }
}
