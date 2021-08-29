using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BgMusicLoop : MonoBehaviour
{
    private static BgMusicLoop instance = null;
    [SerializeField] private AudioSource _audioSource;
    
    private void Awake()
    {
        if (instance == null)
        { 
            instance = this;
            DontDestroyOnLoad(gameObject);
            return;
        }
        if (instance == this) return; 
        Destroy(gameObject);
    }

    void Start()
    {
        _audioSource = GetComponentInChildren<AudioSource>();
        _audioSource.Play();
    }
}