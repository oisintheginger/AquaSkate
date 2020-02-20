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
        myAnim.SetFloat("Speed", 2*(pM.xZPlaneSpeed / pM.maxSpeed));
        /*
        if (Input.GetAxis(pM.verticalAxis)>.5&& pM.xZPlaneSpeed>0.1f)
        {
            myAnim.SetFloat("Speed", 1);
            if(pM.xZPlaneSpeed > 40f)
            {
                myAnim.SetFloat("Speed", 2);
            }
        }
        else if(pM.xZPlaneSpeed < 0.1f)
        {
            myAnim.SetFloat("Speed", 0);
        }
        */
        myAnim.SetFloat("Turn", turning);
        myAnim.SetBool("Grounded", pM.isGrounded);
    }
}
/*
    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowLine : MonoBehaviour
{
public int CurrentWaypointID = 0;
public float speed;
private float reachDistance = 1f;
private Bezier LineRef;
Vector3 last_position;
Vector3 current_position;
// Start is called before the first frame update
void Start()
{
    LineRef = GameObject.Find("Bezier").GetComponent<Bezier>();
    //SetDestination(z_Waypoints[z_CurrentWaypoint].position);
    //gameObject.transform += LineRef.positions[CurrentWaypointID];
    transform.position = LineRef.positions[CurrentWaypointID];
}

// Update is called once per frame
void Update()
{
    Debug.Log(CurrentWaypointID);
    if (CurrentWaypointID <= 49)
    {
        if(CurrentWaypointID<10)
        {
            speed = 5;

        }
        else
        {
            speed = 30;
        }
        transform.position = LineRef.positions[CurrentWaypointID];
        if (Vector3.Distance(LineRef.positions[CurrentWaypointID], this.transform.position) < 1)
        {
            CurrentWaypointID = (CurrentWaypointID + 1);//% LineRef.positions.Length+1;
        }
    }
}

    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bezier : MonoBehaviour
{
    public Image crosshair;
    public LineRenderer linerenderer;
    public Transform point0, point1, point2;
    private int numPoints = 50;
    public Vector3[] positions = new Vector3[50];

    // Start is called before the first frame update
    void Start()
    {
        linerenderer.positionCount = numPoints;
        DrawLinearCurve();
    }

    // Update is called once per frame
    void Update()
    {
        ShootRaycast();
        if (crosshair.fillAmount == 1)
        {
            linerenderer.enabled = true;
            DrawLinearCurve();
        }
        else
        {
            linerenderer.enabled = false;
        }
    }
private void ShootRaycast()
    {
        RaycastHit hit;

        Debug.DrawRay(point0.transform.position, point0.transform.forward,  Color.blue, 1000);
        if (Physics.Raycast(point0.transform.position, point0.transform.forward, out hit, 10000) && hit.rigidbody != null)
        {
            Physics.Raycast(point0.transform.position, point0.transform.forward, out hit, 10000);
            // Physics.Raycast(point0.transform.position, point0.transform.forward, out hit, 10000);
            point2.position = hit.point + new Vector3(0, 0, 0.5f);
            //Debug.Log(hit.distance);
            point1.position = new Vector3((point0.position.x + point2.position.x) / 2 + 8, ((point0.position.y + point2.position.y) / 2) + 5, ((point0.position.z + point2.position.z) / 2) - 8);
            // point1.position =
            //Debug.Log(point1.position);
            //  Debug.Log(hit.transform.name);
        }
    }
private Vector3 CalculateLinerBezierPoint(float t, Vector3 p0, Vector3 p1)
    {
        return p0 + t * (p1 - p0);
    }

    private Vector3 CalculateQuadraticBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        Vector3 p = uu * p0;
        p += 2 * u * t * p1;
        p += tt * p2;
        return p;
    }
}
*/
