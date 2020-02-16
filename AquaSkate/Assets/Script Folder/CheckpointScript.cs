using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckpointScript : MonoBehaviour
{
    [SerializeField] bool isLastCheckpoint =false;
    [SerializeField] int checkPointValue;
    [SerializeField] int playerCount = 0;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Player"&& other.gameObject.GetComponent<playerMotion>().currentCheckPointTarget==checkPointValue-1)
        {
            playerCount++;
            if(isLastCheckpoint && playerCount==2)
            {
                SceneManager.LoadScene(0);
            }
            other.gameObject.GetComponent<playerMotion>().currentCheckPointTarget++;
        }
    }
}
