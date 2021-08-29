using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip horn, uiSound, hit, add, sub, bells;
    
    // Start is called before the first frame update
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlayHorn()
    {
        _audioSource.volume = 0.45f;
        _audioSource.pitch = Random.Range(0.8f, 1);
        _audioSource.PlayOneShot(horn);
    }

    public void PlayUiSound()
    {
        _audioSource.volume = 0.03f;
        _audioSource.pitch = Random.Range(0.8f, 1);
        _audioSource.PlayOneShot(uiSound);
    }

    public void PlayHit()
    {
        _audioSource.volume = 0.2f;
        _audioSource.pitch = Random.Range(0.6f, 0.8f);
        _audioSource.PlayOneShot(hit);
    }

    public void Add()
    {
        _audioSource.volume = 0.1f;
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(add);
    }

    public void Sub()
    {
        _audioSource.volume = 0.15f;
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(sub);
    }

    public void PlayBells()
    {
        _audioSource.volume = 0.7f;
        _audioSource.pitch = Random.Range(0.8f, 1.2f);
        _audioSource.PlayOneShot(bells);
    }
}
