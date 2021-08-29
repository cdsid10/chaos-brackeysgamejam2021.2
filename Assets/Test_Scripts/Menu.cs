using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    [SerializeField] private int scenes, mScenes;
    
    public void Play()
    {
        SceneManager.LoadScene(scenes);
    }
    
    public void MenuScene()
    {
        SceneManager.LoadScene(mScenes);
    }

    public void Exit()
    {
        Application.Quit();
    }
}