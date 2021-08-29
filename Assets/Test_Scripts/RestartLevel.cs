using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartLevel : MonoBehaviour
{
    [SerializeField] private float timeRemaining = 2f;
    [SerializeField] private Image rKey;

    private void Start()
    {
        rKey.fillAmount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                rKey.fillAmount = timeRemaining;
                if (timeRemaining <= 0)
                {
                    SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
                }
            }
            
        }
        else if (Input.GetKeyUp(KeyCode.R))
        {
            timeRemaining = 2f;
            rKey.fillAmount = 0;
        }
    }
}