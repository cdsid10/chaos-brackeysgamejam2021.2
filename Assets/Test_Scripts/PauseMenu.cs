using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseText;

    public bool paused;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(Time.timeScale == 1)
            {
                Time.timeScale = 0;
                pauseText.SetActive(true);
                paused = true;
            } 
            else if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                pauseText.SetActive(false);
                paused = false;
            }
        }
    }
}