using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HolyDamage : MonoBehaviour
{
    private FameManager _fameManager;
    private PlayerMovement _playerMovement;
    
    // Start is called before the first frame update
    void Start()
    {
        _fameManager = FindObjectOfType<FameManager>();
        _playerMovement = FindObjectOfType<PlayerMovement>();
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
            StartCoroutine(CollisionCD());
        }
    }

    IEnumerator CollisionCD()
    {
        _playerMovement.GetComponent<CircleCollider2D>().enabled = false;
        _playerMovement.GetComponent<SpriteRenderer>().color = Color.gray;
        _playerMovement.moveSpeed = 1.5f;
        yield return new WaitForSeconds(2f);
        _playerMovement.GetComponent<SpriteRenderer>().color = Color.white;
        _playerMovement.GetComponent<CircleCollider2D>().enabled = true;
        _playerMovement.moveSpeed = 3f;
    }
}
