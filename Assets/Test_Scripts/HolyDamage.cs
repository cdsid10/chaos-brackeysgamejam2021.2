using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class HolyDamage : MonoBehaviour
{
    private FameManager _fameManager;
    private SpawnManager _spawnManager;
    private PlayerMovement _playerMovement;
    private AudioManager _audioManager;

    [SerializeField] private GameObject particleHurt;
    
    // Start is called before the first frame update
    void Start()
    {
        _fameManager = FindObjectOfType<FameManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
        _spawnManager = FindObjectOfType<SpawnManager>();
        _audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.CompareTag("Player"))
        {
            _fameManager.SubFame();
            Instantiate(particleHurt, _playerMovement.transform.position, quaternion.identity);
            StartCoroutine(CollisionCD());
        }
    }

    IEnumerator CollisionCD()
    {
        _audioManager.PlayHit();
        _playerMovement.GetComponent<CircleCollider2D>().enabled = false;
        _playerMovement.GetComponent<SpriteRenderer>().color = Color.gray;
        _playerMovement.moveSpeed = 1.5f;
        yield return new WaitForSeconds(2f);
        _playerMovement.GetComponent<SpriteRenderer>().color = Color.white;
        _playerMovement.GetComponent<CircleCollider2D>().enabled = true;
        
        if (_spawnManager.huntersPerished >= 2)
        {
            _playerMovement.moveSpeed = 4f;
        }
        else if (_spawnManager.huntersPerished < 2)
        {
            _playerMovement.moveSpeed = 3f;
        }
    }
}
