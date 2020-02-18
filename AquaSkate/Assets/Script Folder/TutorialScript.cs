using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialScript : MonoBehaviour
{

    [SerializeField] GameObject controlsImg, promptImg;
    [SerializeField] bool controlsOn;

    
    private void Update()
    {
        if(Input.GetButtonDown("Cancel"))
        {
            SceneManager.LoadScene("Tutorial");
        }

        if(Input.GetButtonDown("Select"))
        {
            /*controlsImg.GetComponent<RawImage>().enabled = true;
            promptImg.GetComponent<RawImage>().enabled = false;*/

            if (controlsOn == false)
            {

                controlsImg.GetComponent<RawImage>().enabled = true;
                promptImg.GetComponent<RawImage>().enabled = false;
                controlsOn = true;
            }

            else
            {
                controlsImg.GetComponent<RawImage>().enabled = false;
                promptImg.GetComponent<RawImage>().enabled = true;
                controlsOn = false;
            }

            //SceneManager.LoadScene("Tutorial");
        }

        if(Input.GetButtonDown("Pause"))
        {
            SceneManager.LoadScene("Menu");
        }
    }
}
