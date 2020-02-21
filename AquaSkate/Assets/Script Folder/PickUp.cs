using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    public bool isGood, speedorSteering = false;
    [SerializeField] float timePeriod =5f;
    [SerializeField] AudioClip goodPickup, badPickup;
    AudioSource thisAS;
    private float storedSteering, storedMaxSpeed, storedTurning, storedTurningSpeed;
    [SerializeField] float steerAdjustment, speedAdjustment, badSpeedAdjustment, turningAdjustment, turnSpeedAdjustment;
    [SerializeField] Sprite badSteerSprite, goodSteerSprite, badSpeedSprite, goodSpeedSprite;
    // Start is called before the first frame update
    private void Awake()
    {
        storedSteering = GameObject.FindObjectOfType<playerMotion>().steeringScaler;
        storedMaxSpeed = GameObject.FindObjectOfType<playerMotion>().maxSpeed;
        storedTurning = GameObject.FindObjectOfType<playerMotion>().turnScaler;
        storedTurningSpeed = GameObject.FindObjectOfType<playerMotion>().turnSpeed;
        thisAS = this.gameObject.GetComponent<AudioSource>();

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player")
        {
            
            var pM = other.gameObject.GetComponent<playerMotion>();
            var rB = other.gameObject.GetComponent<Rigidbody>();
            pM.powerUpImage.gameObject.SetActive(true);
            if (pM.isPoweredUp == false)
            {
                if (isGood && speedorSteering)
                {
                    pM.isPoweredUp = true;
                    pM.powerUpImage.sprite = goodSpeedSprite;
                    pM.maxSpeed = speedAdjustment;
                    rB.AddForce(other.gameObject.transform.forward * pM.boostForce / 3, ForceMode.VelocityChange);
                    StartCoroutine(restoreTime(other.gameObject, true, timePeriod));
                    thisAS.PlayOneShot(goodPickup);
                }
                if (!isGood && speedorSteering)
                {
                    pM.isPoweredUp = true;
                    pM.powerUpImage.sprite = badSpeedSprite;
                    pM.maxSpeed = speedAdjustment;
                    StartCoroutine(restoreTime(other.gameObject, true, timePeriod));
                    thisAS.PlayOneShot(badPickup);
                }
                if (isGood && !speedorSteering)
                {
                    pM.powerUpImage.sprite = goodSteerSprite;
                    pM.isPoweredUp = true;
                    pM.steeringScaler = steerAdjustment;
                    pM.turnScaler = turningAdjustment;
                    pM.turnSpeed = turnSpeedAdjustment;
                    StartCoroutine(restoreTime(other.gameObject, false, timePeriod));
                    thisAS.PlayOneShot(goodPickup);
                }
                if (!isGood && !speedorSteering)
                {
                    pM.powerUpImage.sprite = badSteerSprite;
                    pM.isPoweredUp = true;
                    pM.steeringScaler = steerAdjustment;
                    pM.turnScaler = turningAdjustment;
                    pM.turnSpeed = turnSpeedAdjustment;
                    StartCoroutine(restoreTime(other.gameObject, false, timePeriod));
                    thisAS.PlayOneShot(badPickup);
                }
                this.gameObject.GetComponent<SphereCollider>().enabled = false;
                this.gameObject.GetComponentInChildren<MeshRenderer>().enabled = false;
            }
        }
    }
    IEnumerator restoreTime(GameObject other, bool speedorSteering, float timeToWait)
    {

        yield return new WaitForSeconds(timeToWait);
        other.gameObject.GetComponent<playerMotion>().isPoweredUp = false;
        other.gameObject.GetComponent<playerMotion>().powerUpImage.gameObject.SetActive(false);
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
