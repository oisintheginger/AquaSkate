using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class playerMotion : MonoBehaviour
{
    [SerializeField] Quaternion sQ = Quaternion.identity;

    public int currentCheckPointTarget;
    public bool canPause = false, isPoweredUp= false;

    public string horizontalAxis, verticalAxis, jumpAxis, accelerateAxis, brakeAxis;
    [SerializeField] Image accelerometerBar;
    public Image powerUpImage, winScreen;
    [SerializeField] Text checkPointUI;
    [SerializeField] int amountOfCheckpoints;

    Rigidbody pRB;
    [SerializeField] Vector3 appliedForce;
    public float maxSpeed, maxForce, boostForce, accelerationForce, brakeForce, xZPlaneSpeed, jumpForce, turnSpeed, turnScaler;
    [Range(0,5)] public float steeringScaler;
    public bool isGrounded;
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
        int aCP =0;
        for(int i = 0; i< GameObject.FindObjectsOfType<CheckpointScript>().Length; i++)
        {
            if(GameObject.FindObjectsOfType<CheckpointScript>()[i].checkPointValue > aCP)
            {
                aCP = GameObject.FindObjectsOfType<CheckpointScript>()[i].checkPointValue;
            }
        }
        amountOfCheckpoints = aCP;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        GroundCheck();
        SlopeCheck();
        groundCheckRay = new Ray(groundTransform.position, -transform.up);
        rampRay = new Ray(slopeTransform.position, transform.forward);
        this.gameObject.transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        Motion();
        checkPointUI.text = currentCheckPointTarget + "/" + amountOfCheckpoints;
        
    }
    
    void ScaleAccelerometer(float currentVelocity, float maxVelocity)
    {
        accelerometerBar.fillAmount = Mathf.Lerp(accelerometerBar.fillAmount,currentVelocity / maxVelocity,Time.deltaTime*2f);
    }

    void GroundCheck()
    {
        RaycastHit rH;
        if (Physics.Raycast(groundTransform.position, -transform.up, out rH, 0.3f))
        {
            if (rH.collider.gameObject.tag == "Ground"|| rH.collider.gameObject.tag=="Rail")
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
        if (Physics.Raycast(groundCheckRay, out rH, .1f))
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
            pRB.AddForce((transform.forward ) * accelerationForce * Input.GetAxis(verticalAxis)/* Mathf.Max(0, Input.GetAxis(verticalAxis))*/, ForceMode.VelocityChange);
            
        }
        if (xZPlaneSpeed >= maxSpeed)
        {
            pRB.velocity = xzSpeed;
        }

        if (Input.GetAxis(brakeAxis)>0&&isGrounded)
        {
            pRB.AddForce(-pRB.velocity.normalized / Mathf.Max(0.1f, brakeForce));
        }
        if (Input.GetAxis(jumpAxis)>0.9f&&isGrounded)
        {
            
            pRB.AddForce(transform.up * jumpForce,ForceMode.VelocityChange);
        }

        if (0.3f < xZPlaneSpeed / maxSpeed)
        {
            pRB.velocity = Vector3.Lerp(pRB.velocity, transform.forward * xZPlaneSpeed, steeringScaler * Time.deltaTime);
        }
        ScaleAccelerometer(xZPlaneSpeed, maxSpeed);
    }

    private void OnDrawGizmos()
    {
       
    }
}
