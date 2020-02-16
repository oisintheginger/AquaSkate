using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public bool isGood, speedorSteering = false;

    private float storedSteering, storedMaxSpeed, storedTurning, storedTurningSpeed;
    [SerializeField] float steerAdjustment, speedAdjustment, turningAdjustment, turnSpeedAdjustment;
    // Start is called before the first frame update
    private void Awake()
    {
        storedSteering = GameObject.FindObjectOfType<playerMotion>().steeringScaler;
        storedMaxSpeed = GameObject.FindObjectOfType<playerMotion>().maxSpeed;
        storedTurning = GameObject.FindObjectOfType<playerMotion>().turnScaler;
        storedTurningSpeed = GameObject.FindObjectOfType<playerMotion>().turnSpeed;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            var pM = other.gameObject.GetComponent<playerMotion>();
            var rB = other.gameObject.GetComponent<Rigidbody>();

            if(isGood && speedorSteering && pM.isPoweredUp==false)
            {
                pM.isPoweredUp = true;
                pM.maxSpeed = speedAdjustment;
                rB.AddForce(other.gameObject.transform.forward * pM.boostForce/3, ForceMode.VelocityChange);
                StartCoroutine(restoreTime(other.gameObject, true, 10f));
            }
            if(!isGood && speedorSteering && pM.isPoweredUp == false)
            {
                pM.isPoweredUp = true;
                pM.maxSpeed = speedAdjustment;
                StartCoroutine(restoreTime(other.gameObject, true, 10f));
            }
            if(isGood && !speedorSteering && pM.isPoweredUp == false)
            {
                pM.isPoweredUp = true;
                pM.steeringScaler = steerAdjustment;
                pM.turnScaler = turningAdjustment;
                pM.turnSpeed = turnSpeedAdjustment;
                StartCoroutine(restoreTime(other.gameObject, false, 10f));
            }
            if (!isGood && !speedorSteering && pM.isPoweredUp == false)
            {
                pM.isPoweredUp = true;
                pM.steeringScaler = steerAdjustment;
                pM.turnScaler = turningAdjustment;
                pM.turnSpeed = turnSpeedAdjustment;
                StartCoroutine(restoreTime(other.gameObject, false, 10f));
            }

        }
    }
    IEnumerator restoreTime(GameObject other, bool speedorSteering, float timeToWait)
    {

        yield return new WaitForSeconds(timeToWait);
        other.gameObject.GetComponent<playerMotion>().isPoweredUp = false;
        if (speedorSteering)
        {
            other.gameObject.GetComponent<playerMotion>().maxSpeed = storedMaxSpeed;
        }
        else if(!speedorSteering)
        {
            other.gameObject.GetComponent<playerMotion>().steeringScaler = storedSteering;
            other.gameObject.GetComponent<playerMotion>().turnScaler = storedTurning;
            other.gameObject.GetComponent<playerMotion>().turnSpeed = turnSpeedAdjustment;
        }

    }
}
