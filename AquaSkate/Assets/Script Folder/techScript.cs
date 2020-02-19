using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class techScript : MonoBehaviour
{
    public Text mask1Text, mask2Text;

    [SerializeField] bool isPlayer1;

    private void Start()
    {
        
    }

    public void OnHover(GameObject button)
    {

        if(button.name == "Mask 1")
        {
            Debug.Log("yeet");
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
        }

        else
        {
            isPlayer1 = false;

        }

        Debug.Log(isPlayer1);
    }


}
