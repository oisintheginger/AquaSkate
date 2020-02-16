﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] bool isLastCheckpoint =false;
    [SerializeField] int checkPointValue;
    [SerializeField] int playerCount = 0;
    [SerializeField] float time = 0, storedMaxSpeed;

    private void Awake()
    {
        storedMaxSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMotion>().maxSpeed;   
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player"&& other.gameObject.GetComponent<playerMotion>().currentCheckPointTarget==checkPointValue-1)
        {
            playerCount++;
            if(isLastCheckpoint && playerCount==2)
            {
                SceneManager.LoadScene(0);
            }
            //boosting
            var pM = other.gameObject.GetComponent<playerMotion>();
            var pR = other.gameObject.GetComponent<Rigidbody>();
            pM.currentCheckPointTarget++;
            pM.maxSpeed *= 2;
            pR.AddForce(other.gameObject.transform.forward * pM.boostForce, ForceMode.VelocityChange);
            StartCoroutine(boostTime(other.gameObject));
        }
    }

    IEnumerator boostTime(GameObject other)
    {
        
        yield return new WaitForSeconds(5f);
        Debug.Log("Got here");
        other.gameObject.GetComponent<playerMotion>().maxSpeed = storedMaxSpeed;
        
        
    }
    void timer(float timeToWait, GameObject other)
    {
        time += Time.deltaTime;
        if(time>=timeToWait)
        {
            other.gameObject.GetComponent<playerMotion>().maxSpeed = storedMaxSpeed;
            time = 0;
        }
    }
}
