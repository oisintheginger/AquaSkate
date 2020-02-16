using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerScript : MonoBehaviour
{
    public bool IsPaused = false;
    [SerializeField] GameObject pauseMenu;
    [SerializeField] bool lockCursor = false;
    private void Awake()
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
    private void Update()
    {
        if(Input.GetButtonDown("Pause"))
        {
            if(IsPaused)
            {
                Play();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Play()
    {
        Time.timeScale = 1f;
        IsPaused = false;
        pauseMenu.SetActive(false);

    }
    public void Pause()
    {
        Time.timeScale = 0f;
        IsPaused = true;
        pauseMenu.SetActive(true);
    }
}
