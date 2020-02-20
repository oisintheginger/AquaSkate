using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class techScript : MonoBehaviour
{
    public Text mask1Text, mask2Text;

    string winner;

    [SerializeField] int p1Level, p2Level, maskCount;
    [SerializeField] bool isPlayer1;

    [SerializeField] GameObject Mask1, Mask2, p1Background, p2Background, Mask1Pic, Mask2Pic;

    private void Awake()
    {
        PlayerPrefs.SetInt("p1Level", 1);
        PlayerPrefs.SetInt("p2Level", 4);
    }

    private void Start()
    {
        p1Level = PlayerPrefs.GetInt("p1Level");
        p2Level = PlayerPrefs.GetInt("p2Level");
    }

    private void Update()
    {

        if(isPlayer1)
        {
            if (p1Level <= 0)
            {
                Mask1.SetActive(false);
                Mask2.SetActive(false);

            }

            if (p1Level >= 1 && p1Level < 4)
            {
                Mask1.SetActive(true);
                Mask2.SetActive(false);
            }

            if (p1Level >= 4)
            {
                Mask1.SetActive(true);
                Mask2.SetActive(true);
            }
        } else
        {
            if (p2Level <= 0)
            {
                Mask1.SetActive(false);
                Mask2.SetActive(false);
            }

            if (p2Level >= 1 && p2Level < 4)
            {
                Mask1.SetActive(true);
                Mask2.SetActive(false);
            }

            if (p2Level >= 4)
            {
                Mask1.SetActive(true);
                Mask2.SetActive(true);
            }
        }
    }

    public void OnHover(GameObject button)
    {

        if(button.name == "Mask 1")
        {
            //Debug.Log("yeet");
            mask1Text.enabled = true;
            mask2Text.enabled = false;
        }

        

        if (button.name == "Mask 2")
        {
            mask1Text.enabled = false;
            mask2Text.enabled = true;
        }
    }

    public void OnClick(GameObject button)
    {
        if(button.name == "Player 1")
        {
            isPlayer1 = true;
            p1Background.SetActive(true);
            p2Background.SetActive(false);
        }

        else
        {
            isPlayer1 = false;
            p1Background.SetActive(false);
            p2Background.SetActive(true);

        }

        if(button.name == "Mask 1")
        {
            PlayerPrefs.SetInt("p1Mask", 1);
        }

        if(button.name == "Mask 2")
        {
            PlayerPrefs.SetInt("p2Mask", 2);
        }

        Debug.Log(isPlayer1);

        if(button.name == "Return")
        {
            SceneManager.LoadScene("Menu");
        }
    }


}
