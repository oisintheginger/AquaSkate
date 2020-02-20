using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetString("player1", "Player 1");
        PlayerPrefs.SetString("player2", "Player 2");

        PlayerPrefs.SetInt("p1Level", 1);
        PlayerPrefs.SetInt("p2Level", 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
