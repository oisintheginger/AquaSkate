using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] bool isLastCheckpoint =false;
    [SerializeField] int checkPointValue;
    [SerializeField] int playerCount = 0;
    [SerializeField] int p1Level, p2Level;
    [SerializeField] float time = 0, storedMaxSpeed;
    [SerializeField] float boostMultiplier = 1.2f;
    

    private void Awake()
    {
        storedMaxSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMotion>().maxSpeed;
        p1Level = PlayerPrefs.GetInt("p1Level");
        p2Level = PlayerPrefs.GetInt("p2Level");
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player"&& other.gameObject.GetComponent<playerMotion>().currentCheckPointTarget==checkPointValue-1)
        {
            playerCount++;

            if(isLastCheckpoint && playerCount == 1)
            {
                PlayerPrefs.SetString("Winner", other.name);


                if(other.name == "Player 1")
                {
                    p1Level++;
                    PlayerPrefs.SetInt("p1Level", p1Level);
                }

                else
                {
                    p2Level++;
                    PlayerPrefs.SetInt("p2Level", p2Level);
                }
            }

            if(isLastCheckpoint && playerCount==2)
            {
                SceneManager.LoadScene("Menu");
            }
            //boosting
            var pM = other.gameObject.GetComponent<playerMotion>();
            var pR = other.gameObject.GetComponent<Rigidbody>();
            pM.currentCheckPointTarget++;
            pM.maxSpeed *= boostMultiplier;
            pR.AddForce(other.gameObject.transform.forward * pM.boostForce, ForceMode.VelocityChange);
            StartCoroutine(boostTime(other.gameObject));
        }
    }

    IEnumerator boostTime(GameObject other)
    {
        
        yield return new WaitForSeconds(time);
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
